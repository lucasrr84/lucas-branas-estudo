using ride.src.domain.dto;
using ride.src.infra.repository;

namespace ride.src.application.usecase;

public class StartRide
{
    private readonly IRideRepository _rideRepository;

    public StartRide(IRideRepository rideRepository)
    {
        _rideRepository = rideRepository;
    }

    public async Task execute(StartRideInputDto input)
    {
        var ride = await _rideRepository.getRideById(input.rideId);
        if (ride == null) throw new Exception();
        ride.start();
        await _rideRepository.updateRide(ride);
    }
}
