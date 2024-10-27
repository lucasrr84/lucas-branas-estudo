using System;
using ride.src.domain.dto;
using ride.src.domain.events;
using ride.src.infra.queue;
using ride.src.infra.repository;

namespace ride.src.application.usecase;

public class FinishRide
{
    private readonly IRideRepository _rideRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IQueue _queue;

    public FinishRide(IRideRepository rideRepository, IPositionRepository positionRepository, IQueue queue)
    {
        _rideRepository = rideRepository;
        _positionRepository = positionRepository;
        _queue = queue;
    }

    public async Task execute(FinishRideInputdto input)
    {
        var ride = await _rideRepository.getRideById(input.rideId);
        if (ride == null) throw new Exception("Ride does not exist");

        //fazer aqui a queue
        ride.register(RideCompletedEvent.eventName, async (object evt) => {
            await _rideRepository.updateRide(ride);
            await _queue.publish("rideCompleted", evt);
        });

        var positions = await _positionRepository.getPositionsByRideId(input.rideId);
        ride.finish(positions);
    }
}
