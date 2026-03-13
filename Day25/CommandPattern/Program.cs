using System;

class Program
{
    static void Main(string[] args)
    {
        Light light = new Light();

        // Command
        ICommand lightOnCommand = new LightOnCommand(light);

        // Invoker
        RemoteControl remoteControl = new RemoteControl(lightOnCommand);

        // Execute command
        remoteControl.PressButton();
    }
}

// Receiver
public class Light
{
    public void On()
    {
        Console.WriteLine("Light is on");
    }

    public void Off()
    {
        Console.WriteLine("Light is off");
    }
}

// Command Interface
public interface ICommand
{
    void Execute();
}

// Concrete Command
public class LightOnCommand : ICommand
{
    private readonly Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.On();
    }
}

// Invoker
public class RemoteControl
{
    private ICommand _command;

    public RemoteControl(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }
}

// Extra class
public class AlexaRemote
{
    public void VoiceCommands()
    {
        Console.WriteLine("Voice activated remote control");
    }
}