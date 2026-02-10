using System;
using System.Diagnostics;
using System.Text;

class Program
{
    static void Main()
    {
        int iterations = 100000;

        // String Concatenation
        Stopwatch sw1 = Stopwatch.StartNew();
        string result1 = "";

        for (int i = 0; i < iterations; i++)
        {
            result1 += "Hello";
        }

        sw1.Stop();
        Console.WriteLine($"Concatenation Time: {sw1.ElapsedMilliseconds} ms");


        // StringBuilder
        Stopwatch sw2 = Stopwatch.StartNew();
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            sb.Append("Hello");
        }

        string result2 = sb.ToString();
        sw2.Stop();
        Console.WriteLine($"StringBuilder Time: {sw2.ElapsedMilliseconds} ms");
    }
}
