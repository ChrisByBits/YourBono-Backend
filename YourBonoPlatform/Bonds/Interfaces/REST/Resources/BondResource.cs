namespace YourBonoPlatform.Bonds.Interfaces.REST.Resources;

public record BondResource(
    int Id,
    int UserId,
    string Name,
    double NominalValue,
    double MarketValue,
    double Duration,
    int Frequency,
    int InterestRateTypeId,
    double InterestRate,
    int Capitalization,
    double DiscountRate,
    DateTime EmissionDate,
    int GracePeriodTypeId,
    int GracePeriodDuration,
    int CurrencyTypeId,
    double PrimeRate,
    double StructuredRate,
    double PlacementRate,
    double FloatingRate,
    double CavaliRate
    );