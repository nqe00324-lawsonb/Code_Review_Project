using System;
using System.Collections.Generic;

namespace BadCodeExamples;

/// <summary>
/// This class demonstrates INTENTIONAL null reference and undefined behavior issues.
/// </summary>
public class NullReferenceIssues
{
    // BAD: No null checks
    public int GetLength(string text)
    {
        return text.Length; // BAD: text could be null -> NullReferenceException
    }

    // BAD: Deep property access without null checks
    public class Person
    {
        public string? Name { get; set; }
        public Address? Address { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
    }

    public string GetPersonCity(Person? person)
    {
        // BAD: Multiple potential null references
        return person.Address.City; // Can throw NullReferenceException multiple times
    }

    // BAD: Using null coalescing but still unsafe
    public void ProcessUser(User? user)
    {
        string name = user?.Name ?? "";
        // If we reach here, we might still have null issues
        Console.WriteLine(name.ToUpper()); // Could still be problematic
    }

    public class User
    {
        public string? Name { get; set; }
        public List<Role>? Roles { get; set; }
    }

    public class Role
    {
        public string? RoleName { get; set; }
    }

    // BAD: Assuming collections are never empty
    public string GetFirstRole(User? user)
    {
        // BAD: No checks for null user, null Roles, or empty Roles
        return user.Roles[0].RoleName;
    }

    // BAD: Not checking for null in loop
    public void ProcessUsers(List<User>? users)
    {
        foreach (var user in users) // BAD: users could be null
        {
            Console.WriteLine(user.Name?.Length ?? 0);
        }
    }

    // BAD: Returning null implicitly
    public User GetUserById(int id)
    {
        if (id == 0)
        {
            return null; // BAD: Returning null without documentation
        }
        return new User { Name = "Test" };
    }

    // BAD: Not checking return value for null
    public void UseGetUserById(int userId)
    {
        var user = GetUserById(userId);
        Console.WriteLine($"User: {user.Name}"); // BAD: user could be null
    }

    // BAD: Object initialization without setting required properties
    public class Product
    {
        public string? Name { get; set; } // Will be null if not set
        public decimal Price { get; set; }
    }

    public void CreateProduct()
    {
        var product = new Product { Price = 100 }; // BAD: Name is null
        Console.WriteLine($"Product: {product.Name}"); // null!
    }

    // BAD: Null-conditional operator misuse
    public void ProcessNullableData(string? data)
    {
        var result = data?.ToUpper(); // result is string? (nullable)
        Console.WriteLine($"Result length: {result.Length}"); // BAD: result could be null
    }

    // BAD: Assuming Parse/TryParse success
    public int ParseNumber(string? input)
    {
        // BAD: No check if input is null
        return int.Parse(input); // Null or invalid format -> Exception
    }

    // BAD: Access after potential null assignment
    public class Configuration
    {
        public string? GetValue(string key)
        {
            return null;
        }
    }

    public void GetConfigSafely()
    {
        var config = new Configuration();
        string? value = config.GetValue("key");
        string upperValue = value.ToUpper(); // BAD: value could be null
    }

    // BAD: Null comparisons that don't help
    public void RiskyComparison(string? text)
    {
        if (text != null)
        {
            Console.WriteLine(text);
        }
        else
        {
            Console.WriteLine(text.Length); // BAD: Will definitely throw NullReferenceException
        }
    }

    // BAD: Not using null-coalescing in assignment
    public string GetValueBadly(string? value1, string? value2, string? value3)
    {
        string result = value1;
        if (result == null)
            result = value2;
        if (result == null)
            result = value3;
        // BAD: result could still be null
        return result.ToUpper();
    }

    public void DemonstrateBadPatterns()
    {
        Console.WriteLine("--- Null Reference Issues ---");

        try
        {
            Console.WriteLine($"String length: {GetLength(null!)}");
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"Caught: {ex.GetType().Name}");
        }

        var person = new Person { Name = "John" }; // Address is null
        try
        {
            Console.WriteLine($"City: {GetPersonCity(person)}");
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"Caught: {ex.GetType().Name}");
        }

        var user = new User { Name = "Alice" }; // Roles is null
        try
        {
            Console.WriteLine($"First role: {GetFirstRole(user)}");
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"Caught: {ex.GetType().Name}");
        }

        try
        {
            ProcessNullableData(null);
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"Caught: {ex.GetType().Name}");
        }

        try
        {
            var num = ParseNumber(null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught: {ex.GetType().Name}");
        }
    }
}
