
namespace ride.src.domain.events;

public class RideCompletedEvent
{
    public static string eventName = "rideCompleted";
    public string rideId { get; }
    public double amount { get; }

    public RideCompletedEvent(string rideId, double amount)
    {
        this.rideId = rideId;
        this.amount = amount;
    }

    public override string ToString()
    {
        return $"{this.rideId} - {this.amount}";
    }
}
