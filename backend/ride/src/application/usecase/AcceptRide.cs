using System;
using ride.src.domain.dto;
using ride.src.infra.gateway;
using ride.src.infra.repository;

namespace ride.src.application.usecase;

public class AcceptRide
{
    private readonly AccountGateway _accountGateway;
    private readonly IRideRepository _rideRepository;

    public AcceptRide(AccountGateway accountGateway, IRideRepository rideRepository)
    {
        _accountGateway = accountGateway;
        _rideRepository = rideRepository;
    }

    public async Task execute(AcceptRideInputDto input)
    {
        var account = await _accountGateway?.getAccountById(input.driverId);
        if (account == null) throw new Exception("Account does not exist");
        if (!account.isDriver) throw new Exception("Account must be a driver");

        var ride = await _rideRepository?.getRideById(input.rideId);
        if (ride == null) throw new Exception("Ride does not exist");

        ride.accept(input.driverId);
        await _rideRepository.updateRide(ride);
    }
}
