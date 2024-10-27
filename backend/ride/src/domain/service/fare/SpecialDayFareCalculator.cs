using System;
using ride.src.domain.service.fare;

namespace ride.src.domain.service.fare;

public class SpecialDayFareCalculator : IFareCalculator
{
    public double calculate(double distance)
    {
        return distance * 1;
    }
}
