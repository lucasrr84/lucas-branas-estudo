using ride.src.domain.entity;

namespace ride.src.domain.vo;

public class RequestedStatus : IRideStatus
{
    private readonly Ride _ride;

    public string value { get; set; }

    public RequestedStatus(Ride ride)
    {
        _ride = ride;
        value = "requested";
    }

    public void request()
    {
        throw new Exception("Invalid status");
    }

    public void accept()
    {
        _ride.setStatus(new AcceptedStatus(_ride));
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
