using System;

namespace ride.src.infra.database;

public interface IDataConnection
{
    object query(string statement, object param);
    void close();
}