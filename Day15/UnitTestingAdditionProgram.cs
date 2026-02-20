using System;

class Program
{
    static void Main()
    {
        AddFunctionShouldReturn30ForInputs10And20();
        AddFunctionShouldReturn40ForInputs20And20();
        AddFunctionShouldReturn50ForInputs25And25();
    }

    static void AddFunctionShouldReturn30ForInputs10And20()
    {
        // Arrange
        var x = 10;
        var y = 20;
        var expectedResult = 30;

        // Act
        var actualResult = Add(x, y);

        // Assert
        Console.WriteLine($"Actual Result: {actualResult}, Expected Result: {expectedResult}");

        if (actualResult == expectedResult)
            Console.WriteLine("Test Passed");
        else
            Console.WriteLine("Test Failed");

        Console.WriteLine();
    }

    static void AddFunctionShouldReturn40ForInputs20And20()
    {
        var expectedResult = 40;
        var actualResult = Add(20, 20);

        Console.WriteLine($"Actual Result: {actualResult}, Expected Result: {expectedResult}");

        if (actualResult == expectedResult)
            Console.WriteLine("Test Passed");
        else
            Console.WriteLine("Test Failed");

        Console.WriteLine();
    }

    static void AddFunctionShouldReturn50ForInputs25And25()
    {
        var expectedResult = 50;
        var actualResult = Add(25, 25);

        Console.WriteLine($"Actual Result: {actualResult}, Expected Result: {expectedResult}");

        if (actualResult == expectedResult)
            Console.WriteLine("Test Passed");
        else
            Console.WriteLine("Test Failed");

        Console.WriteLine();
    }

    static int Add(int a, int b)
    {
        return a + b;
    }
}