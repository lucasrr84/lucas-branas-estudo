using System;

namespace ride.src.domain.vo;

public class Coord
{
    private readonly double _lat;
    private readonly double _long;

    public Coord(double lat, double longi)
    {
        if (lat < -90 || lat > 90) throw new Exception("Invalid latitude");
        if (longi < -180 || longi > 180) throw new Exception("Invalid longitude");

        _lat = lat;
        _long = longi;
    }

    public double getLat()
    {
        return _lat;
    }

    public double getLong()
    {
        return _long;
    }
}
