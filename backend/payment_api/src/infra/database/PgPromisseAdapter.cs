using System;

namespace src.infra.database;

public class PgPromisseAdapter : IDatabaseConnection
{
    private readonly object _connection;

    public PgPromisseAdapter()
    {
        _connection = "postgres://postgres:123456@localhost:5434/app";
    }
    
    public Task<object> query(string statement, object param)
    {
        throw new NotImplementedException();
    }

    public async Task close()
    {
        throw new NotImplementedException();
    }
}
