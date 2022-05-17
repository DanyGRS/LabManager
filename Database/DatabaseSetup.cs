using Microsoft.Data.Sqlite;

namespace LabManager.Database;

class DatabaseSetup
{
    private DatabaseConfig databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
        CreateComputerTable();
    }

    public void CreateComputerTable()
    {
        var connection =  new SqliteConnection("Data Source=database_test.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Computers(
            id int not null primary key,
            ram varchar(100) not null,
            processor varchar(100) not null
            );

            CREATE TABLE IF NOT EXISTS Labs(
            id int not null primary key,
            number int not null,
            name varchar(100) not null,
            block int not null
            );
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
}