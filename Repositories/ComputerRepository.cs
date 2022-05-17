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

    public List<Computer> GetAll()
    {
        var connection =  new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers";

        var reader = command.ExecuteReader();

        var computers = new List<Computer>();

        while(reader.Read())
        {
            computers.Add(new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
        }
        connection.Close();

        return computers;
    }

}