using account.src.domain.vo;
using ride.src.domain.entity;

namespace ride.src.infra.repository;

public class PositionRepositoryMemory : IPositionRepository
{
    private readonly List<Position> _positions;

    public PositionRepositoryMemory()
    {
        _positions = new List<Position>();
    }

    public async Task<List<Position>> getPositionsByRideId(string rideId)
    {
        var positionsData = await Task.FromResult(_positions.Where(position => position.getRideId().getValue() == rideId).OrderBy(position => position.Date).ToList());
        var positions = new List<Position>();
        foreach (var positionData in positionsData)
        {
            await Task.Run(() => positions.Add(positionData));
        }

        return positions;
    }

    public async Task savePosition(Position position)
    {
        await Task.Run(() => _positions.Add(position));
    }
}
