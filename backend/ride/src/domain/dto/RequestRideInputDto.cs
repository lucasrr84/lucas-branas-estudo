using System;

namespace ride.src.domain.dto;

public class RequestRideInputDto
{
    public string passengerId { get; set; } = "";
    public double fromLat { get; set; } = 0;
    public double fromLong { get; set; } = 0;
    public double toLat { get; set; } = 0;
    public double toLong { get; set; } = 0;
}
