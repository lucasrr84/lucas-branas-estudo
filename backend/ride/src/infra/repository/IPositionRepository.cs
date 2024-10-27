using System;
using ride.src.domain.entity;

namespace ride.src.infra.repository;

public interface IPositionRepository
{
    Task savePosition(Position position);
	Task<List<Position>> getPositionsByRideId (string rideId);
}
