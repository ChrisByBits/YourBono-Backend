namespace YourBonoPlatform.Bonds.Interfaces.REST.Resources;

public record BondMetricsResource(
    int Id,
    int BondId,
    double MaxPrice,
    double Duration,
    double Convexity,
    double ModifiedDuration,
    double Tcea,
    double Trea
    );