// Program Name: NullReferenceExceptionDemo

using System;

class NullReferenceExceptionDemo
{
    static void Main()
    {
        string name = null;

        Console.WriteLine(name.ToUpper());
    }
}
