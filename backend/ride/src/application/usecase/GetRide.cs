using ride.src.domain.dto;
using ride.src.domain.entity;
using ride.src.domain.service.distance;
using ride.src.infra.repository;

namespace ride.src.application.usecase;

public class GetRide
{
    private readonly IRideRepository _rideRepository;
    private readonly IPositionRepository _positionRepository;

    public GetRide(IRideRepository rideRepository, IPositionRepository positionRepository)
    {
        _rideRepository = rideRepository;
        _positionRepository = positionRepository;
    }

    public async Task<GetRideResponseDto> execute(string rideId)
    {
        var ride = await _rideRepository.getRideById(rideId);
        if (ride == null) throw new Exception("Ride not found");
        var positions = await _positionRepository.getPositionsByRideId(rideId);
        double distance = (ride.getStatus() == "completed") ? ride.getDistance() : DistanceCalculator.calculateByPositions(positions ?? new List<Position>());

        return new GetRideResponseDto
        {
            rideId = ride.getRideId(),
			passengerId = ride.getPassengerId(),
			fromLat = ride.getFrom().getLat(),
			fromLong = ride.getFrom().getLong(),
			toLat = ride.getTo().getLat(),
			toLong = ride.getTo().getLong(),
			status = ride.getStatus(),
			driverId = ride.getDriverId(),
			positions = positions,
			distance = distance,
			fare = ride.getFare()
        };
    }
}
