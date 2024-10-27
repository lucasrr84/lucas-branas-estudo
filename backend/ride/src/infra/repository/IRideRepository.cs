using System;
using ride.src.domain.entity;

namespace ride.src.infra.repository;

public interface IRideRepository
{
    Task saveRide(Ride ride);
    Task<Ride> getRideById (string rideId);
    Task updateRide(Ride ride);
    Task<bool> hasActivateRideByPassengerId(string passengerId); 
}
