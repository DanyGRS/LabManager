using LabManager.Database;
using LabManager.Models;
using LabManager.Repositories;
using Microsoft.Data.Sqlite;

    
var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

//routing
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }
    }
}

if(modelAction == "New")
{
    int id = Convert.ToInt32(args[2]);
    string ram = args[3];
    string processador = args[4];

    var computer = new Computer(id,  ram, processador); 
    computerRepository.Save(computer);
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        var connection =  new SqliteConnection("Data Source=database_test.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Labs";

        var reader = command.ExecuteReader();

        Console.WriteLine("Lab List");
        while(reader.Read())
        {
            Console.WriteLine(
                "{0}, {1}, {2}, {3}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
        }

        reader.Close();
        connection.Close();
    }
}

if(modelAction == "New")
{
    var connection =  new SqliteConnection("Data Source=database_test.db");
    connection.Open();

    int id = Convert.ToInt32(args[2]);
    int numero = Convert.ToInt32(args[3]);
    string nome = args[4];
    string bloco = args[5];

    var command = connection.CreateCommand();
    command.CommandText = "INSERT INTO Computers VALUES($id, $number, $name, $block)";
    command.Parameters.AddWithValue("$id", id);
    command.Parameters.AddWithValue("$number", numero);
    command.Parameters.AddWithValue("$name", nome);
    command.Parameters.AddWithValue("$block", bloco);

    command.ExecuteNonQuery();
    connection.Close();
}