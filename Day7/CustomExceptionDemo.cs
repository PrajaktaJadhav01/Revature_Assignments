// Program Name: CustomExceptionDemo

using System;

class CustomExceptionDemo
{
    static void Main()
    {
        try
        {
            CheckBalance(500, 200);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void CheckBalance(int withdraw, int balance)
    {
        if (withdraw > balance)
            throw new Exception("Insufficient Balance");
    }
}
