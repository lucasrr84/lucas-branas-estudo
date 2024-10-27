using System;

namespace ride.src.domain.dto;

public class UpdatePositionInputDto
{
    public string rideId { get; set; } = "";
    public double lat { get; set; } = 0;
    public double longi { get; set; } = 0;
    public DateTime date { get; set; } = new DateTime();
}
