using System;

// Create button using factory method
IButton button = CreateButton("MacOS");

Console.WriteLine($"Button Name: {button.Name}");
button.Click();


// Factory Method
IButton CreateButton(string os)
{
    return os switch
    {
        "Windows" => new WindowsButton(),
        "MacOS" => new MacOSButton(),
        _ => throw new ArgumentException("Invalid OS")
    };
}


// Button Interface
interface IButton
{
    string Name { get; set; }
    void Click();
}


// Windows Button
class WindowsButton : IButton
{
    public string Name { get; set; } = "Windows Button";

    public void Click()
    {
        Console.WriteLine("Windows Button Clicked");
    }
}


// MacOS Button
class MacOSButton : IButton
{
    public string Name { get; set; } = "MacOS Button";

    public void Click()
    {
        Console.WriteLine("MacOS Button Clicked");
    }
}