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

if(modelAction == "Delete")
{

    int id = Convert.ToInt32(args[2]);

    computerRepository.Delete(id);
    Console.WriteLine($"Computador com id {id} foi deletado!");
    
}


if(modelAction == "Show")
{
    int id = Convert.ToInt32(args[2]);

    if(computerRepository.existsById(id))
    {
        var computer = computerRepository.GetById(id);
        Console.WriteLine("{0},{1},{2}", computer.Id, computer.Ram, computer.Processor);
    } 
    else 
    {
        Console.WriteLine($"Computador com id {id} não existe");
    }
    //ExecuteScalar
} 

if(modelAction == "Update")
{
    int id = Convert.ToInt32(args[2]);
    string ram = args[3];
    string processador = args[4];

    var computer = new Computer(id, ram, processador);
    computerRepository.Update(computer);   
}