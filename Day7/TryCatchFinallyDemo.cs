// Program Name: TryCatchFinallyDemo

using System;

class TryCatchFinallyDemo
{
    static void Main()
    {
        try
        {
            int num = int.Parse("abc");
        }
        catch
        {
            Console.WriteLine("Error occurred");
        }
        finally
        {
            Console.WriteLine("Finally block executed");
        }
    }
}
