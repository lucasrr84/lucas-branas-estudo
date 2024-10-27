using System;
using ride.src.domain.service.fare;

namespace ride.src.domain.service.fare;

public class NormalFareCalculator : IFareCalculator
{
    public double calculate(double distance)
    {
        return distance * 2.1;
    }
}
