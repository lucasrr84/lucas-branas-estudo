using ride.src.domain.entity;

namespace ride.src.domain.vo;

public class AcceptedStatus : IRideStatus
{
    private readonly Ride _ride;

    public string value { get; set; }

    public AcceptedStatus(Ride ride)
    {
        _ride = ride;
        value = "accepted";
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
        _ride.setStatus(new InProgressStatus(_ride));
    }

    public void finish()
    {
        throw new Exception("Invalid status");
    }
}
