// Program Name: CombinedExceptionDemo

using System;
using System.IO;

class CombinedExceptionDemo
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Program Started");

            CheckArray();      // IndexOutOfRangeException
            CheckNull();       // NullReferenceException
            CheckDivision();   // DivideByZeroException
            CheckFile();       // FileNotFoundException
            AcceptPayment(500, 200); // Custom Exception
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Divide By Zero Error: " + ex.Message);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("Array Index Error: " + ex.Message);
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine("Null Reference Error: " + ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("File Error: " + ex.Message);
        }
        catch (NotEnoughBalanceException ex)
        {
            Console.WriteLine("Payment Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("General Error: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed.");
        }

        Console.WriteLine("Program Ended");
    }

    // ------------------- Exception Methods -------------------

    static void CheckArray()
    {
        int[] arr = { 1, 2, 3 };
        Console.WriteLine(arr[5]);   // IndexOutOfRangeException
    }

    static void CheckNull()
    {
        string name = null;
        Console.WriteLine(name.ToUpper()); // NullReferenceException
    }

    static void CheckDivision()
    {
        int a = 10;
        int b = 0;
        Console.WriteLine(a / b); // DivideByZeroException
    }

    static void CheckFile()
    {
        string text = File.ReadAllText("abc.txt"); // FileNotFoundException
        Console.WriteLine(text);
    }

    static void AcceptPayment(decimal amount, decimal balance)
    {
        if (amount > balance)
        {
            throw new NotEnoughBalanceException("Not enough balance.");
        }

        Console.WriteLine("Payment Successful");
    }
}

// ------------------- Custom Exception -------------------

class BankException : ApplicationException
{
    public BankException(string message) : base(message) { }
}

class NotEnoughBalanceException : BankException
{
    public NotEnoughBalanceException(string message) : base(message) { }
}
