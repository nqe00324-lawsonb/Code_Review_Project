using System;
using BadCodeExamples;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Bad Code Examples for Code Review Plugin Testing ===\n");

        // Test various bad code patterns
        var securityDemo = new SecurityVulnerabilities();
        securityDemo.DemonstrateBadPatterns();

        var resourceDemo = new ResourceManagementIssues();
        resourceDemo.DemonstrateBadPatterns();

        var namingDemo = new PoorNamingDemo();
        namingDemo.TestMethod1();

        var exceptionDemo = new ExceptionHandlingIssues();
        exceptionDemo.DemonstrateBadPatterns();

        var nullDemo = new NullReferenceIssues();
        nullDemo.DemonstrateBadPatterns();

        Console.WriteLine("\n=== All bad code examples executed ===");
    }
}
