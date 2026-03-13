CustomerService customerService = new CustomerService();

// Create command
var addCustomerCommand = new AddCustomerCommand(customerService);

// Invoker
var controller = new Controller(addCustomerCommand);

// Execute command
controller.PressButton();


// Receiver (Actual business logic)
public class CustomerService
{
    public void AddCustomer()
    {
        Console.WriteLine("Customer added successfully.");
    }

    public void DeleteCustomer()
    {
        Console.WriteLine("Customer deleted successfully.");
    }
}


// Command Interface
public interface ICommand
{
    void Execute();
}


// Concrete Command
public class AddCustomerCommand : ICommand
{
    private readonly CustomerService _service;

    public AddCustomerCommand(CustomerService service)
    {
        _service = service;
    }

    public void Execute()
    {
        _service.AddCustomer();
    }
}


// Invoker
public class Controller
{
    private ICommand _command;

    public Controller(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }
}