using System;
using ride.src.domain.entity;

namespace ride.src.domain.dto;

public class GetRideResponseDto
{
    public string rideId { get; set; } = "";
    public string passengerId { get; set; } = "";
    public double fromLat { get; set; } = 0;
    public double fromLong { get; set; } = 0;
    public double toLat { get; set; } = 0;
    public double toLong { get; set; } = 0;
    public string status { get; set; } = "";
    public string driverId { get; set; } = "";
    public List<Position> positions { get; set; } = new List<Position>();
    public double distance { get; set; } = 0;
    public double fare { get; set; } = 0;
}
