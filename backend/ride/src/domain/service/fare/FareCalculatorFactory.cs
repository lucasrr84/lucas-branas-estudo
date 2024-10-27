using System;

namespace ride.src.domain.service.fare;

public class FareCalculatorFactory
{
    public static IFareCalculator create(DateTime date)
    {
        if (date.Day == 1) return new SpecialDayFareCalculator();
        if (date.Hour > 22 || date.Hour < 6) return new OvernightFareCalculator();
        return new NormalFareCalculator();
    }
}
