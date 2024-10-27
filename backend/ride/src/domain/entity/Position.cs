using ride.src.domain.vo;

namespace ride.src.domain.entity;

public class Position
{
    private readonly UUID _positionId;
    private readonly UUID _rideId;
    public Coord Coord { get; private set; }
    public DateTime Date {get; private set; }

    public Position(string positionId, string rideId, double lat, double longi, DateTime date)
    {
        _positionId = new UUID(positionId);
        _rideId = new UUID(rideId);
        Coord = new Coord(lat, longi);
        Date = date;
    }

    public static Position create(string rideId, double lat, double longi, DateTime date = new DateTime())
    {
        var positionId = UUID.create().getValue();
        return new Position(positionId, rideId, lat, longi, date);
    }

    public void setCoord(double lat, double longi)
    {
        Coord = new Coord(lat, longi);
    }

    public UUID getRideId()
    {
        return _rideId;
    }
}
