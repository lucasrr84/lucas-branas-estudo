using ride.src.domain.entity;

namespace ride.src.domain.vo;

public class CompletedStatus : IRideStatus
{
    private readonly Ride _ride;

    public string value { get; set; }

    public CompletedStatus(Ride ride)
    {
        _ride = ride;
        value = "completed";
    }

    public void request()
    {
        throw new Exception("Invalid status");
    }

    public void accept()
    {
        throw new Exception("Invalid status");
    }

    public void start()
    {
        throw new Exception("Invalid status");
    }

    public void finish()
    {
        throw new Exception("Invalid status");
    }
}
