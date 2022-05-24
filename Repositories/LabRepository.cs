using Dapper;
using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;


namespace LabManager.Repositories;

class LabRepository
{
    private DatabaseConfig databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public List<Lab> GetAll()
    {
        using var connection =  new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        return connection.Query<Lab>("SELECT * FROM Labs;").ToList();
    }

    public Lab Save(Lab lab)
    {
        using var connection =  new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("INSERT INTO labs VALUES(@id, @number, @name, @block);", lab);
        return lab;
    
    }
}