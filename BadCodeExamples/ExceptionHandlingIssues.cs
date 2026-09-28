using System;

namespace BadCodeExamples;

/// <summary>
/// This class demonstrates INTENTIONAL poor exception handling patterns.
/// </summary>
public class ExceptionHandlingIssues
{
    // BAD: Swallowing exceptions silently
    public string GetConfigValue(string key)
    {
        try
        {
            // Simulate config read that might fail
            throw new System.IO.FileNotFoundException("Config file not found");
        }
        catch (Exception) // BAD: Catching too broadly
        {
            // Silent failure - no logging, no indication something went wrong
            return null;
        }
    }

    // BAD: Catching and re-throwing without adding value
    public void FunctionWithUselessCatch()
    {
        try
        {
            DoSomethingRisky();
        }
        catch (Exception ex)
        {
            // BAD: Catching and immediately re-throwing is pointless
            throw;
        }
    }

    private void DoSomethingRisky()
    {
        throw new InvalidOperationException("Something went wrong");
    }

    // BAD: Catching Exception base class (too broad)
    public void ProcessData()
    {
        try
        {
            // Some code
            int result = 10 / int.Parse("0"); // Will throw DivideByZeroException
        }
        catch (Exception ex) // BAD: Too broad!
        {
            Console.WriteLine("Error: " + ex.ToString()); // Generic error handling
        }
    }

    // BAD: Using exceptions for control flow
    public bool IsValidNumber(string input)
    {
        try
        {
            int.Parse(input);
            return true;
        }
        catch // BAD: Exceptions should not be used for control flow
        {
            return false;
        }
    }

    // BAD: Not disposing resources in catch block
    public void FileOperationWithBadExceptionHandling(string filePath)
    {
        var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
        try
        {
            // Read from stream
        }
        catch (Exception ex)
        {
            // BAD: Stream is never closed if exception occurs
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // BAD: Catching and losing original exception
    public void LoseExceptionContext()
    {
        try
        {
            throw new ApplicationException("Original error");
        }
        catch (ApplicationException ex)
        {
            // BAD: Creating new exception without InnerException loses stack trace
            throw new Exception("Something went wrong");
        }
    }

    // BAD: Multiple catch blocks with same handling
    public void DuplicateCatchHandling()
    {
        try
        {
            // Some operation
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            // Similar handling
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            // Same handling as above - code duplication
        }
    }

    // BAD: Not checking for null before operations
    public int ProcessList(int[]? items)
    {
        // BAD: No null check - will throw NullReferenceException
        return items.Length; // items could be null
    }

    // BAD: Swallowing exception and returning default value silently
    public decimal GetPrice(string productId)
    {
        try
        {
            return LookupPrice(productId);
        }
        catch
        {
            return 0; // BAD: Silently returns 0, caller won't know about error
        }
    }

    private decimal LookupPrice(string productId)
    {
        throw new InvalidOperationException("Product not found");
    }

    public void DemonstrateBadPatterns()
    {
        Console.WriteLine("--- Exception Handling Issues ---");

        var config = GetConfigValue("SomeKey");
        Console.WriteLine($"Config value: {config ?? "null (swallowed exception)"}");

        FunctionWithUselessCatch();

        ProcessData();

        Console.WriteLine($"IsValidNumber('abc'): {IsValidNumber("abc")}");

        var price = GetPrice("INVALID_ID");
        Console.WriteLine($"Price (silent failure): {price}");
    }
}
