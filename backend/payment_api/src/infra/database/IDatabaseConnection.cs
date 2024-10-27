using System;

namespace src.infra.database;

public interface IDatabaseConnection
{
    Task<object> query(string statement, object param);
    Task close();
}
