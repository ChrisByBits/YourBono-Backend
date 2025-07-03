namespace YourBonoPlatform.Bonds.Domain.Model.Commands;

public record CreateBondCommand(
    int UserId,
    string Name,
    double NominalValue,
    double MarketValue,
    int Duration,
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