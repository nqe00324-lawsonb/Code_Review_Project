using System;
using System.Data.SqlClient;

namespace BadCodeExamples;

/// <summary>
/// This class demonstrates INTENTIONAL security vulnerabilities for testing purposes.
/// DO NOT use these patterns in production code!
/// </summary>
public class SecurityVulnerabilities
{
    // BAD: Hardcoded credentials
    private const string HardcodedPassword = "Admin123!";
    private string hardcodedConnectionString = "Server=myserver;Database=mydb;User=sa;Password=SuperSecurePassword123";

    // BAD: SQL Injection vulnerability
    public void GetUserData(string userId)
    {
        string query = "SELECT * FROM Users WHERE UserId = " + userId; // Direct string concatenation - SQL injection!
        Console.WriteLine($"Executing query: {query}");
        // ExecuteQuery(query);
    }

    // BAD: Weak encryption
    public string EncryptUserData(string data)
    {
        // Using ROT13 is not encryption!
        return RotateString(data, 13);
    }

    private string RotateString(string input, int shift)
    {
        var result = "";
        foreach (char c in input)
        {
            if (c >= 'a' && c <= 'z')
            {
                result += (char)('a' + (c - 'a' + shift) % 26);
            }
            else if (c >= 'A' && c <= 'Z')
            {
                result += (char)('A' + (c - 'A' + shift) % 26);
            }
            else
            {
                result += c;
            }
        }
        return result;
    }

    // BAD: No input validation
    public void ProcessUserInput(string userInput)
    {
        Console.WriteLine("User input: " + userInput);
        // Using directly without validation or sanitization
        var data = userInput.Split(":");
        string username = data[0]; // Can throw IndexOutOfRangeException
        string email = data[1];
    }

    // BAD: Information disclosure through exceptions
    public void LoginUser(string username, string password)
    {
        try
        {
            // Simulate database lookup
            throw new Exception("User not found in database");
        }
        catch (Exception ex)
        {
            // BAD: Exposing detailed error message
            Console.WriteLine($"Login failed: {ex.Message}"); // Reveals database structure
        }
    }

    public void DemonstrateBadPatterns()
    {
        Console.WriteLine("--- Security Vulnerabilities ---");
        GetUserData("1 OR 1=1"); // SQL injection attempt
        Console.WriteLine("Encrypted data (weak): " + EncryptUserData("SecretInfo"));
        ProcessUserInput("admin:admin@example.com");
        LoginUser("testuser", "password");
    }
}
