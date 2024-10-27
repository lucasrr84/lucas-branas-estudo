using System;
using ride.src.domain.service.fare;

namespace ride.src.domain.service.fare;

public class OvernightFareCalculator : IFareCalculator
{
    public double calculate(double distance)
    {
        return distance * 3.9;
    }
}
