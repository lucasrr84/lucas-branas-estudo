using System;

namespace ride.src.domain.dto;

public class AcceptRideInputDto
{
    public string rideId { get; set; } = "";
    public string driverId { get; set; } = "";
}
