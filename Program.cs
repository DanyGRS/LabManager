﻿using Microsoft.Data.Sqlite;

var connection =  new SqliteConnection("Data Source=database_test.db");
connection.Open();

var command = connection.CreateCommand();
command.CommandText = @"
    CREATE TABLE IF NOT EXISTS Computers(
    id int not null primary key,
    ram varchar(100) not null,
    processor varchar(100) not null
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