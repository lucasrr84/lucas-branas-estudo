using System;
using ride.src.domain.entity;

namespace ride.src.domain.vo;

public class RideStatusFactory
{
    public static IRideStatus create(string status, Ride ride)
    {
        if (status == "requested") return new RequestedStatus(ride);
        if (status == "accepted") return new AcceptedStatus(ride);
        if (status == "in_progress") return new InProgressStatus(ride);
        if (status == "completed") return new CompletedStatus(ride);
        throw new Exception("Invalid status");
    }

}
