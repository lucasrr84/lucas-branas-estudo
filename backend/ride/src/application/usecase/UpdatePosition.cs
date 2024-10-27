using System;
using ride.src.domain.dto;
using ride.src.domain.entity;
using ride.src.infra.repository;

namespace ride.src.application.usecase;

public class UpdatePosition
{
    private readonly IRideRepository _rideRepository;
    private readonly IPositionRepository _positionRepository;

    public UpdatePosition(IRideRepository rideRepository, IPositionRepository positionRepository)
    {
        _rideRepository = rideRepository;
        _positionRepository = positionRepository;
    }

    public async Task execute(UpdatePositionInputDto input)
    {
        var ride = await _rideRepository.getRideById(input.rideId);
        if (ride == null) throw new Exception("Ride not found");
        var position = Position.create(input.rideId, input.lat, input.longi, input.date);
        await _positionRepository.savePosition(position);
    }
}