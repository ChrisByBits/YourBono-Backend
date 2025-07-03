using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;
using YourBonoPlatform.Bonds.Domain.Services;

namespace YourBonoPlatform.Bonds.Application.Internal
{
    public class BondValuationService : IBondValuationService
    {
        private const int MathPrecision = 10;
        private const MidpointRounding Rounding = MidpointRounding.AwayFromZero;

        public async Task<IEnumerable<CashFlowItem>> CalculateCashFlows(Bond bond)
        {
            var totalPeriods = bond.Duration * bond.Frequency;
            var TEA = GetTEA(bond);
            var periodInterestRate = GetPeriodicInterestRate(TEA, bond.Frequency);
            var periodDiscountRate = GetPeriodDiscountRate(bond.DiscountRate / 100, bond.Frequency);

            List<CashFlowItem> cashFlows = [];
            double balance = bond.NominalValue;
            DateTime currentDate = bond.EmissionDate;

            var initialExpensesIssuer = (bond.StructuredRate + bond.PlacementRate + bond.FloatingRate + bond.CavaliRate) / 100 * bond.MarketValue;
            var initialExpensesBondHolder = initialExpensesIssuer;

            var issuerCashFlow = bond.MarketValue - initialExpensesIssuer;
            var bondHolderCashFlow = bond.MarketValue + initialExpensesBondHolder;

            cashFlows.Add(new CashFlowItem(0,  bond.Id, 0, currentDate, false, 0, 0, 0, bond.NominalValue, 0, issuerCashFlow, -bondHolderCashFlow, 0, 0, 0));

            for (int period = 1; period <= totalPeriods; period++)
            {
                currentDate = currentDate.AddDays(360 / bond.Frequency);
                double amortization = 0;
                double interest = balance * periodInterestRate;

                if (period == totalPeriods)
                {
                    interest = balance * (periodInterestRate + bond.PrimeRate / 100);
                }

                bool isGrace = period <= bond.GracePeriodDuration;
                if (!isGrace)
                {
                    double amortizableAmount = cashFlows[bond.GracePeriodDuration].FinalBalance;
                    amortization = amortizableAmount / (totalPeriods - bond.GracePeriodDuration);
                }

                double totalPayment = 0;
                double finalBalance = 0;

                if (isGrace)
                {
                    if (bond.GracePeriodTypeId == (int)EGracePeriodTypes.Total)
                    {
                        totalPayment = 0;
                        finalBalance = balance + interest;
                        bondHolderCashFlow = 0;
                    }
                    else if (bond.GracePeriodTypeId == (int)EGracePeriodTypes.Partial)
                    {
                        totalPayment = interest;
                        finalBalance = balance + interest;
                        bondHolderCashFlow = totalPayment;
                    }
                }
                else
                {
                    totalPayment = interest + amortization;
                    finalBalance = balance - amortization;
                    bondHolderCashFlow = totalPayment;
                }

                finalBalance = Math.Abs(finalBalance) < 0.00001 ? 0 : Math.Round(finalBalance, MathPrecision, Rounding);
                issuerCashFlow = -bondHolderCashFlow;

                double presentValue = GetPresentValue(bondHolderCashFlow, periodDiscountRate, period);
                double presentValueTimesPeriod = presentValue * period;
                double convexityFactor = presentValueTimesPeriod * (period + 1);

                cashFlows.Add(new CashFlowItem(
                    0, bond.Id, period, currentDate, isGrace,
                    Math.Round(balance, 2),
                    Math.Round(interest, 2),
                    Math.Round(amortization, 2),
                    Math.Round(finalBalance, 2),
                    Math.Round(totalPayment, 2),
                    Math.Round(issuerCashFlow, 2),
                    Math.Round(bondHolderCashFlow, 2),
                    Math.Round(presentValue, 2),
                    Math.Round(presentValueTimesPeriod, 2),
                    Math.Round(convexityFactor, 2)));

                balance = finalBalance;
            }

            return await Task.FromResult(cashFlows);
        }

        public async Task<BondMetrics?> CalculateBondMetrics(Bond bond, IEnumerable<CashFlowItem> cashFlows)
        {
            double periodDiscountRate = GetPeriodDiscountRate(bond.DiscountRate / 100, bond.Frequency);
            double maxBondPrice = GetMaxBondPrice(cashFlows.Select(c => c.PresentCashFlow).ToList());
            double duration = GetDuration(cashFlows.Select(c => c.PresentCashFlowTimesPeriod).ToList(), maxBondPrice);
            double modifiedDuration = duration / (1 + periodDiscountRate);
            double convexity = GetConvexity(cashFlows.Select(c => c.ConvexityFactor).ToList(), maxBondPrice, periodDiscountRate);
            double tcea = GetTCEA(cashFlows, bond.Frequency);
            double trea = GetTREA(cashFlows, bond.Frequency);

            return await Task.FromResult(new BondMetrics(0, bond.Id, maxBondPrice, duration, convexity, modifiedDuration, tcea, trea));
        }

        private double GetTEA(Bond bond)
        {
            if (bond.InterestRateTypeId == (int)EInterestTypes.Effective)
                return bond.InterestRate / 100;

            double tnp = bond.InterestRate / 100;
            int m = 360 / bond.Capitalization;
            return (double)Math.Pow((double)(1 + tnp / m), m) - 1;
        }

        private double GetPeriodicInterestRate(double tea, int frequency)
        {
            double baseVal = (double)(1 + tea);
            double exponent = 1.0 / frequency;
            return (double)(Math.Pow(baseVal, exponent) - 1);
        }

        private double GetPeriodDiscountRate(double discountRate, int frequency)
        {
            double baseVal = (double)(1 + discountRate);
            double exponent = 1.0 / frequency;
            return (double)(Math.Pow(baseVal, exponent) - 1);
        }

        private double GetPresentValue(double value, double rate, int period)
        {
            return value / (double)Math.Pow((double)(1 + rate), period);
        }

        private double GetMaxBondPrice(List<double> presentCashFlows)
        {
            return presentCashFlows.Skip(1).Sum();
        }

        private double GetDuration(List<double> flowsTimesPeriod, double price)
        {
            return flowsTimesPeriod.Skip(1).Sum() / price;
        }

        private double GetConvexity(List<double> convexityFactors, double price, double rate)
        {
            double sum = convexityFactors.Skip(1).Sum();
            double denom = price * (double)Math.Pow((double)(1 + rate), 2);
            return sum / denom;
        }

        private double GetTREA(IEnumerable<CashFlowItem> cashFlows, int frequency)
        {
            var flows = cashFlows.Select(c => c.BondHolderCashFlow).ToList();
            double trep = CalculateIRR(flows);
            return (double)Math.Pow((double)(1 + trep), frequency) - 1;
        }

        private double GetTCEA(IEnumerable<CashFlowItem> cashFlows, int frequency)
        {
            var flows = cashFlows.Select(c => c.IssuerCashFlow).ToList();
            double tcep = CalculateIRR(flows);
            return (double)Math.Pow((double)(1 + tcep), frequency) - 1;
        }

        private double CalculateIRR(List<double> cashFlows)
        {
            const double tol = 1e-10;
            const int maxIter = 1000;
            double guess = 0.1;

            for (int iter = 0; iter < maxIter; iter++)
            {
                double f = 0, df = 0;
                for (int t = 0; t < cashFlows.Count; t++)
                {
                    f += (double)cashFlows[t] / Math.Pow(1 + guess, t);
                    df -= t * (double)cashFlows[t] / Math.Pow(1 + guess, t + 1);
                }

                double newGuess = guess - f / df;
                if (Math.Abs(newGuess - guess) < tol)
                    return (double)newGuess;

                guess = newGuess;
            }

            throw new Exception("IRR did not converge");
        }
    }
}
