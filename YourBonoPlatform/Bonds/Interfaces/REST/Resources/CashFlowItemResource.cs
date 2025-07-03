namespace YourBonoPlatform.Bonds.Interfaces.REST.Resources;

public record CashFlowItemResource(
    int Id,
    int BondId,
    int Period,
    DateTime PaymentDate,
    bool IsGracePeriod,
    double InitialBalance,
    double Interest,
    double Amortization,
    double FinalBalance,
    double TotalPayment,
    double IssuerCashFlow,
    double BondHolderCashFlow,
    double PresentCashFlow,
    double PresentCashFlowTimesPeriod,
    double ConvexityFactor
    );