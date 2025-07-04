using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;

namespace YourBonoPlatform.Bonds.Domain.Model.Aggregates;

public class Bond
{
    public Bond()
    {
        UserId = 0;
        Name = string.Empty;
        NominalValue = 0.0;
        MarketValue = 0.0;
        Duration = 0;
        Frequency = 0;
        InterestRateTypeId = 0;
        InterestRate = 0.0;
        Capitalization = 0;
        DiscountRate = 0.0;
        EmissionDate = DateTime.MinValue;
        GracePeriodTypeId = 0;
        GracePeriodDuration = 0;
        CurrencyTypeId = 0;
        PrimeRate = 0.0;
        StructuredRate = 0.0;
        PlacementRate = 0.0;
        FloatingRate = 0.0;
        CavaliRate = 0.0;
    }
    public Bond(CreateBondCommand command)
    {
        UserId = command.UserId;
        Name = command.Name;
        NominalValue = command.NominalValue;
        MarketValue = command.MarketValue;
        Duration = command.Duration;
        Frequency = command.Frequency;
        InterestRateTypeId = command.InterestRateTypeId;
        InterestRate = command.InterestRate;
        Capitalization = command.Capitalization;
        DiscountRate = command.DiscountRate;
        EmissionDate = command.EmissionDate;
        GracePeriodTypeId = command.GracePeriodTypeId;
        GracePeriodDuration = command.GracePeriodDuration;
        CurrencyTypeId = command.CurrencyTypeId;
        PrimeRate = command.PrimeRate;
        StructuredRate = command.StructuredRate;
        PlacementRate = command.PlacementRate;
        FloatingRate = command.FloatingRate;
        CavaliRate = command.CavaliRate;
    }

    public void Update(UpdateBondCommand command)
    {
        Name = command.Name;
        NominalValue = command.NominalValue;
        MarketValue = command.MarketValue;
        Duration = command.Duration;
        Frequency = command.Frequency;
        InterestRateTypeId = command.InterestRateTypeId;
        InterestRate = command.InterestRate;
        Capitalization = command.Capitalization;
        DiscountRate = command.DiscountRate;
        EmissionDate = command.EmissionDate;
        GracePeriodTypeId = command.GracePeriodTypeId;
        GracePeriodDuration = command.GracePeriodDuration;
        CurrencyTypeId = command.CurrencyTypeId; 
        PrimeRate = command.PrimeRate;
        StructuredRate = command.StructuredRate;
        PlacementRate = command.PlacementRate;
        FloatingRate = command.FloatingRate;
        CavaliRate = command.CavaliRate;
    }
    
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public double NominalValue { get; private set; }
    public double MarketValue { get; private set; }
    public int Duration { get; private set; }
    public int Frequency { get; private set; }
    public int InterestRateTypeId { get; private set; }
    public double InterestRate { get; private set; }
    public int Capitalization { get; private set; }
    public double DiscountRate { get; private set; }
    public DateTime EmissionDate { get; private set; }
    public int GracePeriodTypeId { get; private set; }
    public int GracePeriodDuration { get; private set; }
    public int CurrencyTypeId { get; private set; }
    public double PrimeRate { get; private set; }
    public double StructuredRate { get; private set; }
    public double PlacementRate { get; private set; }
    public double FloatingRate { get; private set; }
    public double CavaliRate { get; private set; }
}