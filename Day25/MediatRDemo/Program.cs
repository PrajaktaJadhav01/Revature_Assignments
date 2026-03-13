using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

services.AddLogging();

// Register MediatR
services.AddMediatR(typeof(Program));

// Register services
services.AddScoped<App>();

var provider = services.BuildServiceProvider();

var app = provider.GetService<App>();

app.Run();


// ---------------- APP ----------------

public class App
{
    private readonly IMediator _mediator;

    public App(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void Run()
    {
        _mediator.Send(new CreateCustomerCommand
        {
            Id = 1,
            Name = "Prajakta",
            Email = "prajakta@gmail.com"
        });
    }
}


// ---------------- CUSTOMER MODEL ----------------

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}


// ---------------- COMMAND ----------------

public class CreateCustomerCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}


// ---------------- HANDLER ----------------

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Unit>
{
    public Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Customer Created: {request.Name} ({request.Email})");
        return Task.FromResult(Unit.Value);
    }
}