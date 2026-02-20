using System;

class Program
{
    static int Add(int a, int b)
    {
        return a + b;
    }

    static void Main(string[] args)
    {
        int passedTests = 0;
        int totalTests = 4;

        int result1 = Add(2, 3);
        Console.WriteLine("2 + 3 = " + result1);
        if (result1 == 5)
            passedTests++;

        int result2 = Add(10, 20);
        Console.WriteLine("10 + 20 = " + result2);
        if (result2 == 30)
            passedTests++;

        int result3 = Add(-5, 5);
        Console.WriteLine("-5 + 5 = " + result3);
        if (result3 == 0)
            passedTests++;

        int result4 = Add(100, 200);
        Console.WriteLine("100 + 200 = " + result4);
        if (result4 == 300)
            passedTests++;

        if (passedTests == totalTests)
            Console.WriteLine("Test Passed");
        else
            Console.WriteLine("Test Failed");
    }
}