using System;

namespace ride.src.domain.service.fare;

public interface IFareCalculator
{
    double calculate(double distance);
}
