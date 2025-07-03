namespace YourBonoPlatform.Bonds.Domain.Model.Entities;

public class CashFlowItem
{
    
    public CashFlowItem(int id, int bondId, int period, DateTime paymentDate, bool isGracePeriod,
        double initialBalance, double interest, double amortization, double finalBalance, double totalPayment,
        double issuerCashFlow, double bondHolderCashFlow, double presentCashFlow,
        double presentCashFlowTimesPeriod, double convexityFactor)
    {
        Id = id;
        BondId = bondId;
        Period = period;
        PaymentDate = paymentDate;
        IsGracePeriod = isGracePeriod;
        InitialBalance = initialBalance;
        Interest = interest;
        Amortization = amortization;
        FinalBalance = finalBalance;
        TotalPayment = totalPayment;
        IssuerCashFlow = issuerCashFlow;
        BondHolderCashFlow = bondHolderCashFlow;
        PresentCashFlow = presentCashFlow;
        PresentCashFlowTimesPeriod = presentCashFlowTimesPeriod;
        ConvexityFactor = convexityFactor;
    }
    
    public int Id { get; private set; }
    public int BondId { get; private set; }
    public int Period { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public bool IsGracePeriod { get; private set; }
    public double InitialBalance { get; private set; }
    public double Interest { get; private set; }
    public double Amortization { get; private set; }
    public double FinalBalance { get; private set; }
    public double TotalPayment { get; private set; }
    public double IssuerCashFlow { get; private set; }
    public double BondHolderCashFlow { get; private set; }
    public double PresentCashFlow { get; private set; }
    public double PresentCashFlowTimesPeriod { get; private set; }
    public double ConvexityFactor { get; private set; }

}