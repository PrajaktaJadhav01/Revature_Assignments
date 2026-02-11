// Program Name: TryCatchDemo

using System;

class TryCatchDemo
{
    static void Main()
    {
        try
        {
            int x = 10;
            int y = 0;
            Console.WriteLine(x / y);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero");
        }
    }
}
