using ride.src.domain.entity;
using ride.src.domain.vo;

namespace ride.src.domain.service.distance;

public class DistanceCalculator
{
    public static double calculate(Coord from, Coord to)
    {
        var earthRadius = 6371;
		var degreesToRadians = Math.PI / 180;
		var deltaLat = (to.getLat() - from.getLat()) * degreesToRadians;
		var deltaLon = (to.getLong() - from.getLong()) * degreesToRadians;
		var a =
			Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
			Math.Cos(from.getLat() * degreesToRadians) *
			Math.Cos(to.getLat() * degreesToRadians) *
			Math.Sin(deltaLon / 2) *
			Math.Sin(deltaLon / 2);
		var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
		var distance = earthRadius * c;
		return Math.Round(distance);
    }

    public static double calculateByPositions(List<Position> positions)
    {
        double distance = 0;
        
        for (int index = 0; index < positions.Count; index++)
        {
            var position = positions[index];
            var nextPosition = index + 1 < positions.Count ? positions[index + 1] : null;

            if (nextPosition == null) continue;

            distance += DistanceCalculator.calculate(position.Coord, nextPosition.Coord);
        }

        return distance;
    }
}
