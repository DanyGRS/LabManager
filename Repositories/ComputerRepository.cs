using Dapper;
using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public IEnumerable<Computer> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var computers = connection.Query<Computer>("SELECT * FROM Computers");

        return computers;
    }

    public Computer Save(Computer computer)
    {
        using var connection =  new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)", computer);

        return computer;

    }

    public void Delete(int id)
    {
        using var connection =  new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        
        connection.Execute("DELETE FROM Computers WHERE Id = @Id", new{Id = id});
      
    }


    public Computer GetById(int id)
    {
        using var connection =  new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE Id = @Id", new {Id = id});

        return computer;
    }

    public Computer Update(Computer computer)
    {
        using var connection =  new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Computers SET Ram = @Ram, Processor = @Processor WHERE Id = @Id", computer);
        
        return computer;
    }

    public bool existsById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var resultado = connection.ExecuteScalar<bool>("SELECT count(id) FROM Computers WHERE Id = @Id", new{Id = id});

        return resultado;
    }
}