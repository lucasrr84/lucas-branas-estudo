using ride.src.domain.events;
using ride.src.domain.service.distance;
using ride.src.domain.service.fare;
using ride.src.domain.vo;
using ride.src.infra.mediator;

namespace ride.src.domain.entity;

public class Ride : Mediator
{
    private readonly UUID _rideId;
    private readonly UUID _passengerId;
    private UUID _driverId;
    private readonly Coord _from;
    private readonly Coord _to;
    private IRideStatus _status;
    private readonly DateTime _date;
    private double _distance;
    private double _fare;

    public Ride(string rideId, string passengerId, double fromLat, double fromLong, double toLat, double toLong, string status, DateTime date, string driverId = "", double distance = 0, double fare = 0)
    {
        _rideId = new UUID(rideId);
        _passengerId = new UUID(passengerId);
        if (String.IsNullOrEmpty(driverId)) _driverId = new UUID(driverId);
        _from = new Coord(fromLat, fromLong);
        _to = new Coord(toLat, toLong);
        _status = RideStatusFactory.create(status, this);
        _date = date;
        _distance = distance;
        _fare = fare;
    }

    public static Ride create(string passengerId, double fromLat, double fromLong, double toLat, double toLong)
    {
        var uuid = UUID.create();
        var status = "requested";
        var date = new DateTime();
        var driverId = "";
        var distance = 0;
        var fare = 0;
        return new Ride(uuid.getValue(), passengerId, fromLat, fromLong, toLat, toLong, status, date, driverId, distance, fare);
    }

    public string getRideId()
    {
        return _rideId.getValue();
    }

    public string getPassengerId()
    {
        return _passengerId.getValue();
    }

    public string getDriverId()
    {
        return _driverId.getValue();
    }

    public Coord getFrom()
    {
        return _from;
    }

    public Coord getTo()
    {
        return _to;
    }

    public string getStatus()
    {
        return _status.value;
    }

    public void setStatus(IRideStatus status)
    {
        _status = status;
    }

    public void accept(string driverId)
    {
        _status.accept();
        _driverId = new UUID(driverId);
    }

    public void start()
    {
        _status.start();
    }

    public async void finish(List<Position> positions)
    {
        _distance = 0;
        _fare = 0;

        for (int index = 0; index < positions.Count(); index++)
        {
            var position = positions[index];
            var nextPosition = index + 1 < positions.Count() ? positions[index + 1] : null;
    
            if (nextPosition == null) continue;

            double distance = DistanceCalculator.calculate(position.Coord, nextPosition.Coord);
            _distance += distance;
            _fare += FareCalculatorFactory.create(position.Date).calculate(distance);
        }
        _status.finish();
        var evt = new RideCompletedEvent(_rideId.getValue(), _fare);
        await notify(RideCompletedEvent.eventName, evt);
    }

    public DateTime getDate()
    {
        return _date;
    }

    public double getDistance()
    {
        return _distance;
    }

    public double getFare()
    {
        return _fare;
    }
}
