using System;
using ride.src.domain.entity;

namespace ride.src.infra.repository;

public class RideRepositoryMemory : IRideRepository
{
    private readonly List<Ride> _rides;

    public RideRepositoryMemory()
    {
        _rides = new List<Ride>();
    }

    public async Task<Ride> getRideById(string rideId)
    {
        return await Task.FromResult(_rides.FirstOrDefault(ride => ride.getRideId() == rideId));
    }

    public async Task<bool> hasActivateRideByPassengerId(string passengerId)
    {
        return await Task.FromResult(_rides.Any(ride => ride.getPassengerId() == passengerId && ride.getStatus() != "completed" && ride.getStatus() != "cancelled"));
    }

    public async Task saveRide(Ride ride)
    {
        await Task.Run(() => _rides.Add(ride));
    }

    public async Task updateRide(Ride ride)
    {
        var rideToRemove = await Task.FromResult(_rides.FirstOrDefault(r => r.getRideId() == ride.getRideId()));
        if (rideToRemove != null)
        {
            _rides.Remove(rideToRemove);
            _rides.Add(ride);
        }

        /*
        Logger.getInstance().debug("updateRide", ride);
		await this.connection?.query("update ccca.ride set status = $1, driver_id = $2, distance = $3, fare = $4 where ride_id = $5", [ride.getStatus(), ride.getDriverId(), ride.getDistance(), ride.getFare(), ride.getRideId()]);
        */
    }
}
