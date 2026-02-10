public class Animal
{
    public virtual void Speak()
    {
        System.Console.WriteLine("Animal sound");
    }
}

public class Dog : Animal
{
    public override void Speak()
    {
        System.Console.WriteLine("Dog says: Woof");
    }
}

public class Cat : Animal
{
    public override void Speak()
    {
        System.Console.WriteLine("Cat says: Meow");
    }
}
