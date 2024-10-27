using ride.src.domain.dto;
using ride.src.domain.entity;
using ride.src.infra.gateway;
using ride.src.infra.repository;

namespace ride.src.application.usecase;

public class RequestRide
{
    private readonly AccountGateway _accountGateway;
    private readonly IRideRepository _rideRepository;

    public RequestRide(AccountGateway accountGateway, IRideRepository rideRepository)
    {
        _accountGateway = accountGateway;
        _rideRepository = rideRepository;
    }

    public async Task<RequestRideOutputDto> execute(RequestRideInputDto input)
    {
        var account = await _accountGateway.getAccountById(input.passengerId);
        if (account == null) throw new Exception("Account does not exist");
        if (!account.isPassenger) throw new Exception("Account must be from a passenger");
        
        var passengerHasActiveRide = await _rideRepository.hasActivateRideByPassengerId(input.passengerId);
        if (passengerHasActiveRide) throw new Exception("Passenger already have an active ride");
        
        var ride = Ride.create(input.passengerId, input.fromLat, input.fromLong, input.toLat, input.toLong);
        await _rideRepository.saveRide(ride);
        
        return new RequestRideOutputDto
        {
            rideId = ride.getRideId()
        };
    }
}
