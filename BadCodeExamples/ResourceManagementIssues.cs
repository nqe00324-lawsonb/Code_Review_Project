using System;
using System.IO;

namespace BadCodeExamples;

/// <summary>
/// This class demonstrates INTENTIONAL resource management issues for testing purposes.
/// </summary>
public class ResourceManagementIssues
{
    // BAD: Stream not disposed
    public string ReadFileWithoutDisposing(string filePath)
    {
        FileStream fs = new FileStream(filePath, FileMode.Open); // BAD: Never disposed
        StreamReader reader = new StreamReader(fs); // BAD: Never disposed
        return reader.ReadToEnd();
    }

    // BAD: Connection not closed
    public void AccessDatabaseBadly()
    {
        var connection = new object(); // Simulating a DB connection
        // BAD: No try-catch, no finally to close connection
        // connection.Open();
        // connection.ExecuteQuery("SELECT * FROM Users");
        // connection is never closed
    }

    // BAD: Memory leak with large collections
    private static List<byte[]> _largeMemoryList = new List<byte[]>();

    public void CauseMemoryLeak()
    {
        // BAD: Continuously adding large objects to static collection
        for (int i = 0; i < 1000000; i++)
        {
            _largeMemoryList.Add(new byte[1024 * 1024]); // 1MB each - never cleared
        }
    }

    // BAD: Event handlers not unsubscribed
    private Button? _button;
    private int _eventSubscriptionCount = 0;

    public void SubscribeToButtonClick()
    {
        _button = new Button();
        _button.Click += ButtonOnClick; // BAD: Never unsubscribed - memory leak
        _eventSubscriptionCount++;
    }

    private void ButtonOnClick()
    {
        Console.WriteLine("Button clicked");
    }

    // BAD: Timer not stopped
    private System.Timers.Timer? _timer;

    public void StartTimerBadly()
    {
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += (sender, args) => Console.WriteLine("Timer tick"); // BAD: Never stopped
        _timer.Start();
    }

    // BAD: No using statement for IDisposable
    public void ProcessFileWithoutUsing(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        string content = reader.ReadToEnd();
        // BAD: No Dispose() call
    }

    public void DemonstrateBadPatterns()
    {
        Console.WriteLine("--- Resource Management Issues ---");
        Console.WriteLine("(Demonstrating resource leaks - actual file operations skipped for safety)");

        // Note: Commented out to prevent actual file system operations
        // ReadFileWithoutDisposing("nonexistent.txt");

        SubscribeToButtonClick();
        SubscribeToButtonClick(); // Multiple subscriptions
        SubscribeToButtonClick();

        Console.WriteLine($"Event handlers subscribed: {_eventSubscriptionCount}");

        // StartTimerBadly(); // Starts timer that never stops
    }
}

// Simple mock Button class for demonstration
public class Button
{
    public event Action? Click;

    public void RaiseClick()
    {
        Click?.Invoke();
    }
}
