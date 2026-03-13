using System;

// Using Command Pattern
ICommand createCustomerCommand = new CreateCustomerCommand();
createCustomerCommand.Execute();

// Direct Method Call (violates Open/Closed principle)
CreateCustomer();

void CreateCustomer()
{
    Console.WriteLine("Creating customer directly");
}

// Command Interface
public interface ICommand
{
    void Execute();
}

// Concrete Command
public class CreateCustomerCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Create Customer");
    }
}