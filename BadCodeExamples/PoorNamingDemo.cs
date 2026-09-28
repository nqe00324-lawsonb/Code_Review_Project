using System;

namespace BadCodeExamples;

/// <summary>
/// This class demonstrates INTENTIONAL poor naming conventions and code structure.
/// </summary>
public class PoorNamingDemo
{
    // BAD: Single-letter and unclear variable names
    private int x, y, z;
    private string a, b, c;
    private double[] arr1, arr2, arr3;

    // BAD: Poor method naming
    public void DoStuff()
    {
        int result = CalculateStuff(5, 10);
        Console.WriteLine($"Result: {result}");
    }

    private int CalculateStuff(int val1, int val2)
    {
        // BAD: Unclear what this calculates
        return val1 * val2 + val1 - val2;
    }

    // BAD: Method that does too much (God Method)
    public void ProcessData(string filepath)
    {
        // Read file
        byte[] data = new byte[1024];

        // Process data
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = (byte)(data[i] % 256);
        }

        // Validate data
        if (data.Length == 0)
        {
            throw new Exception("File is empty");
        }

        // Serialize data
        string serialized = System.Text.Json.JsonSerializer.Serialize(data);

        // Save to database
        Console.WriteLine($"Saved: {serialized.Substring(0, Math.Min(50, serialized.Length))}");

        // Send to API
        Console.WriteLine("Sent to API");

        // Log activity
        Console.WriteLine($"Processed file: {filepath}");
    }

    // BAD: Parameter naming
    public int Calculate(int a, int b, int c, int d)
    {
        return a + b * c - d / 2;
    }

    // BAD: Boolean naming
    public bool IsOk(string input)
    {
        return input.Length > 0;
    }

    public bool IsValid(string data)
    {
        // What does this validate? No comments, unclear purpose
        return data.Contains("@");
    }

    // BAD: Magic numbers without explanation
    public decimal ComputePrice(decimal basePrice, int quantity)
    {
        return basePrice * quantity * 1.15m + 50m - (quantity > 100 ? 10m : 0m) * 0.95m;
    }

    // BAD: Inconsistent naming conventions
    public void TestMethod1()
    {
        int MyVariable = 5;
        string myOtherVariable = "test";
        double MY_THIRD_VAR = 3.14;

        ProcessData("temp.txt");
    }

    // BAD: Poor class design with mixed responsibilities
    public class DataProcessor
    {
        public void P1(string s) { } // Meaningless abbreviation
        public void P2(int x) { }    // Meaningless abbreviation
        public void P3() { }         // Meaningless abbreviation
    }

    // BAD: Confusing variable shadowing
    public void ShadowingExample()
    {
        int value = 10;
        {
            int innerValue = 20; // Changed to show shadowing is still bad pattern
            // int value = 20; // BAD: Would shadow outer variable
            Console.WriteLine(innerValue); // Which value?
        }
    }
}
