// Program Name: FileNotFoundExceptionDemo

using System;
using System.IO;

class FileNotFoundExceptionDemo
{
    static void Main()
    {
        string text = File.ReadAllText("sample.txt");
        Console.WriteLine(text);
    }
}
