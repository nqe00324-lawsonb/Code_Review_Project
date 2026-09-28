using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BadCodeExamples;

/// <summary>
/// This class demonstrates INTENTIONAL performance and design anti-patterns.
/// </summary>
public class PerformanceAndDesignAntiPatterns
{
    // BAD: String concatenation in loop (performance issue)
    public string BuildStringBadly(string[] items)
    {
        string result = "";
        foreach (var item in items)
        {
            result += item + ", "; // BAD: Creates new string each iteration
        }
        return result;
    }

    // BAD: Inefficient LINQ usage
    public bool ContainsValue(List<int> numbers, int value)
    {
        return numbers.Count(x => x == value) > 0; // BAD: Iterates entire list
    }

    // BAD: Excessive boxing/unboxing
    public void BoxingIssues()
    {
        var list = new List<object>(); // Non-generic equivalent, causes boxing
        list.Add(5);      // Boxing
        int value = (int)list[0]; // Unboxing
    }

    // BAD: Creating objects unnecessarily
    public void UnnecessaryObjectCreation()
    {
        for (int i = 0; i < 10000000; i++)
        {
            var obj = new object(); // Creates unnecessary objects
        }
    }

    // BAD: Recursive without base case safety
    public int RecursionWithoutSafety(int n)
    {
        // BAD: No max recursion depth check - StackOverflowException risk
        return n <= 0 ? 0 : n + RecursionWithoutSafety(n - 1);
    }

    // BAD: Infinite loop risk
    public void PotentialInfiniteLoop(int[] data)
    {
        int i = 0;
        while (i < data.Length)
        {
            Console.WriteLine(data[i]);
            // BAD: i is never incremented - infinite loop!
        }
    }

    // BAD: Hardcoded values scattered throughout
    public decimal CalculateTotalPrice(decimal itemPrice, int quantity)
    {
        decimal tax = 0.08m; // BAD: Hardcoded tax rate
        decimal shipping = 10m; // BAD: Hardcoded shipping
        return (itemPrice * quantity * tax) + shipping;
    }

    // BAD: No input validation
    public int Divide(int a, int b)
    {
        return a / b; // BAD: b could be 0 -> DivideByZeroException
    }

    // BAD: Mutable static field (thread-unsafe, hard to test)
    private static List<string> _globalCache = new List<string>();

    public void AddToCache(string item)
    {
        _globalCache.Add(item); // BAD: Mutable static state
    }

    // BAD: Direct dependency on concrete class (violates DIP)
    public class ReportGenerator
    {
        private SqlDatabase _database = new SqlDatabase(); // BAD: Hard dependency

        public void GenerateReport()
        {
            var data = _database.GetData();
        }
    }

    public class SqlDatabase
    {
        public List<string> GetData()
        {
            return new List<string> { "data" };
        }
    }

    // BAD: Primitive type obsession
    public void RegisterUser(string firstName, string lastName, string email, 
                            string phone, string address, string city, string state, string zip)
    {
        // BAD: Too many primitive parameters - should be an object
        Console.WriteLine($"Registered: {firstName} {lastName}");
    }

    // BAD: Dead code
    public int Calculate(int x, int y)
    {
        int result = x + y;

        // This code is never executed
        int neverUsed = x * y, alsoNeverUsed = x - y;

        return result;
    }

    // BAD: Switch statement without default case
    public string ProcessStatus(int status)
    {
        switch (status)
        {
            case 1:
                return "Active";
            case 2:
                return "Inactive";
            // BAD: No default case - returns null implicitly
        }
        return null;
    }

    // BAD: Magic numbers
    public bool IsAdult(int age)
    {
        return age >= 18; // What is 18? Why 18?
    }

    public decimal ApplyDiscount(decimal price, int customerTier)
    {
        if (customerTier == 1) return price * 0.90m;
        if (customerTier == 2) return price * 0.80m;
        if (customerTier == 3) return price * 0.70m;
        return price; // BAD: Magic numbers, unclear logic
    }

    // BAD: Inconsistent state
    public class BankAccount
    {
        private decimal _balance;
        private decimal _overdraftLimit;

        public void Deposit(decimal amount)
        {
            _balance += amount; // No validation
            Console.WriteLine($"Deposited: {amount}");
        }

        public void Withdraw(decimal amount)
        {
            _balance -= amount; // BAD: No validation, can go into invalid state
        }
    }

    public void DemonstrateBadPatterns()
    {
        Console.WriteLine("--- Performance and Design Anti-Patterns ---");

        var stringArray = new string[] { "a", "b", "c", "d", "e" };
        Console.WriteLine($"String concat: {BuildStringBadly(stringArray)}");

        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        Console.WriteLine($"Contains 3: {ContainsValue(numbers, 3)}");

        BoxingIssues();
        Console.WriteLine("Boxing/Unboxing performed");

        Console.WriteLine($"Calculation: {Divide(10, 2)}");

        var account = new BankAccount();
        account.Deposit(100);
        account.Withdraw(150); // Account now in invalid state
    }
}
