using Microsoft.Data.Sqlite;

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

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        connection =  new SqliteConnection("Data Source=database_test.db");
        connection.Open();
        command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers";

        var reader = command.ExecuteReader();

        Console.WriteLine("Computer List");
        while(reader.Read())
        {
            Console.WriteLine(
                "{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        }

        reader.Close();
        connection.Close();
    }
}

if(modelAction == "New")
{
    connection =  new SqliteConnection("Data Source=database_test.db");
    connection.Open();

    int id = Convert.ToInt32(args[2]);
    string ram = args[3];
    string processador = args[4];

    command = connection.CreateCommand();
    command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);";
    command.Parameters.AddWithValue("$id", id);
    command.Parameters.AddWithValue("$ram", ram);
    command.Parameters.AddWithValue("$processor", processador);
    
    command.ExecuteNonQuery();
    connection.Close();
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        connection =  new SqliteConnection("Data Source=database_test.db");
        connection.Open();
        command = connection.CreateCommand();
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
    connection =  new SqliteConnection("Data Source=database_test.db");
    connection.Open();

    int id = Convert.ToInt32(args[2]);
    int numero = Convert.ToInt32(args[3]);
    string nome = args[4];
    string bloco = args[5];

    command = connection.CreateCommand();
    command.CommandText = "INSERT INTO Computers VALUES($id, $number, $name, $block)";
    command.Parameters.AddWithValue("$id", id);
    command.Parameters.AddWithValue("$number", numero);
    command.Parameters.AddWithValue("$name", nome);
    command.Parameters.AddWithValue("$block", bloco);

    command.ExecuteNonQuery();
    connection.Close();
}