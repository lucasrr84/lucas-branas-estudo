using ride.src.domain.entity;

namespace ride.src.domain.vo;

public class InProgressStatus : IRideStatus
{
    private readonly Ride _ride;

    public string value { get; set; }

    public InProgressStatus(Ride ride)
    {
        _ride = ride;
        value = "in_progress";
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
        _ride.setStatus(new CompletedStatus(_ride));
    }
}
