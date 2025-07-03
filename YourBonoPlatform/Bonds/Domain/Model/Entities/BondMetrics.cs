namespace YourBonoPlatform.Bonds.Domain.Model.Entities;

public class BondMetrics
{
    public BondMetrics(int id, int bondId, double maxPrice, double duration, double convexity, double modifiedDuration, double tcea, double trea)
    {
        Id = id;
        BondId = bondId;
        MaxPrice = maxPrice;
        Duration = duration;
        Convexity = convexity;
        ModifiedDuration = modifiedDuration;
        Tcea = tcea;
        Trea = trea;
    }

    public void Update(BondMetrics updatedMetrics)
    {
        Duration = updatedMetrics.Duration;
        Convexity = updatedMetrics.Convexity;
        ModifiedDuration = updatedMetrics.ModifiedDuration;
        Tcea = updatedMetrics.Tcea;
        Trea = updatedMetrics.Trea;
    }
    
    public int Id { get; private set; }
    public int BondId { get; private set; }
    public double MaxPrice { get; private set; }
    public double Duration { get; private set; }
    public double Convexity { get; private set; }
    public double ModifiedDuration { get; private set; }
    public double Tcea { get; private set; }
    public double Trea { get; private set; }
}