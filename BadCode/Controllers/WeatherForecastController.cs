using Microsoft.AspNetCore.Mvc;
using BadCodeExamples;

namespace BadCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        // Original endpoint
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // ======================
        // BAD CODE DEMONSTRATION ENDPOINTS
        // ======================

        /// <summary>
        /// Demonstrates Security Vulnerabilities
        /// Tests SQL injection, hardcoded credentials, weak encryption, and information disclosure
        /// </summary>
        [HttpGet("security-demo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SecurityDemo()
        {
            try
            {
                var securityDemo = new SecurityVulnerabilities();
                securityDemo.DemonstrateBadPatterns();

                return Ok(new
                {
                    message = "Security vulnerabilities demonstrated",
                    issues = new[]
                    {
                        "SQL Injection vulnerability present",
                        "Hardcoded credentials",
                        "Weak encryption (ROT13)",
                        "No input validation",
                        "Information disclosure through exceptions"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Demonstrates Resource Management Issues
        /// Tests unclosed streams, memory leaks, and event handler issues
        /// </summary>
        [HttpGet("resource-demo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ResourceManagementDemo()
        {
            try
            {
                var resourceDemo = new ResourceManagementIssues();
                resourceDemo.DemonstrateBadPatterns();

                return Ok(new
                {
                    message = "Resource management issues demonstrated",
                    issues = new[]
                    {
                        "Undisposed streams and file handles",
                        "Memory leaks from static collections",
                        "Event handler subscriptions not unsubscribed",
                        "Timers not stopped",
                        "Missing using statements"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Demonstrates Poor Naming Conventions
        /// Tests unclear variable names, god methods, and inconsistent naming
        /// </summary>
        [HttpGet("naming-demo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PoorNamingDemo()
        {
            try
            {
                var namingDemo = new PoorNamingDemo();
                namingDemo.TestMethod1();

                return Ok(new
                {
                    message = "Poor naming conventions demonstrated",
                    issues = new[]
                    {
                        "Single-letter variable names (x, y, z, a, b, c)",
                        "Unclear method names (DoStuff, CalculateStuff)",
                        "God methods doing too much",
                        "Inconsistent naming conventions",
                        "Magic numbers without explanation",
                        "Variable shadowing"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Demonstrates Exception Handling Issues
        /// Tests swallowed exceptions, broad catches, and control flow misuse
        /// </summary>
        [HttpGet("exception-demo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ExceptionHandlingDemo()
        {
            try
            {
                var exceptionDemo = new ExceptionHandlingIssues();
                exceptionDemo.DemonstrateBadPatterns();

                return Ok(new
                {
                    message = "Exception handling issues demonstrated",
                    issues = new[]
                    {
                        "Swallowed exceptions silently",
                        "Catching Exception base class too broadly",
                        "Using exceptions for control flow",
                        "Not checking for null before operations",
                        "Returning default values silently on error",
                        "Losing original exception context"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Demonstrates Null Reference Issues
        /// Tests missing null checks and unsafe property access
        /// </summary>
        [HttpGet("null-demo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult NullReferenceDemo()
        {
            try
            {
                var nullDemo = new NullReferenceIssues();
                nullDemo.DemonstrateBadPatterns();

                return Ok(new
                {
                    message = "Null reference issues demonstrated",
                    issues = new[]
                    {
                        "Missing null checks",
                        "Deep property access without validation",
                        "Unsafe null-conditional operators",
                        "Assuming non-null return values",
                        "Returning null implicitly",
                        "Object initialization with null properties"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Demonstrates Performance and Design Anti-Patterns
        /// Tests string concatenation, inefficient LINQ, boxing, and design issues
        /// </summary>
        [HttpGet("performance-demo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PerformanceDemo()
        {
            try
            {
                var perfDemo = new PerformanceAndDesignAntiPatterns();
                perfDemo.DemonstrateBadPatterns();

                return Ok(new
                {
                    message = "Performance and design anti-patterns demonstrated",
                    issues = new[]
                    {
                        "String concatenation in loops",
                        "Inefficient LINQ usage",
                        "Boxing/unboxing overhead",
                        "Unnecessary object creation",
                        "Hardcoded values scattered",
                        "No input validation",
                        "Mutable static fields (thread-unsafe)",
                        "Direct dependencies on concrete classes",
                        "Magic numbers",
                        "Dead code",
                        "Missing switch default cases"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Test specific bad code - SQL Injection vulnerable endpoint
        /// </summary>
        [HttpGet("get-user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUserWithSQLInjection(string userId)
        {
            try
            {
                var security = new SecurityVulnerabilities();
                security.GetUserData(userId);

                return Ok(new
                {
                    message = "User lookup attempted",
                    warning = "This endpoint demonstrates SQL injection vulnerability!",
                    userId = userId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Test specific bad code - Process user input without validation
        /// </summary>
        [HttpPost("process-input")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ProcessUserInputBadly([FromBody] ProcessInputRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request?.Input))
                {
                    return BadRequest(new { error = "Input is required" });
                }

                var security = new SecurityVulnerabilities();
                security.ProcessUserInput(request.Input);

                return Ok(new
                {
                    message = "Input processed",
                    warning = "This endpoint demonstrates lack of input validation!",
                    inputLength = request.Input.Length
                });
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest(new { error = $"Input format error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Test specific bad code - String concatenation performance issue
        /// </summary>
        [HttpPost("build-string")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuildStringBadly([FromBody] string[] items)
        {
            try
            {
                if (items == null || items.Length == 0)
                {
                    return BadRequest(new { error = "Items array is required" });
                }

                var perfDemo = new PerformanceAndDesignAntiPatterns();
                var result = perfDemo.BuildStringBadly(items);

                return Ok(new
                {
                    message = "String built (using bad concatenation pattern)",
                    result = result,
                    itemCount = items.Length,
                    warning = "This uses inefficient string concatenation in a loop!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Test specific bad code - Division without validation
        /// </summary>
        [HttpGet("divide/{a}/{b}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DivideBadly(int a, int b)
        {
            try
            {
                var perfDemo = new PerformanceAndDesignAntiPatterns();
                var result = perfDemo.Divide(a, b);

                return Ok(new
                {
                    message = "Division successful",
                    a = a,
                    b = b,
                    result = result
                });
            }
            catch (DivideByZeroException ex)
            {
                return BadRequest(new { error = $"Division error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all available bad code demonstration endpoints
        /// </summary>
        [HttpGet("demo-endpoints")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetDemoEndpoints()
        {
            return Ok(new
            {
                description = "Available Bad Code Demonstration Endpoints",
                endpoints = new object[]
                {
                    new { method = "GET", path = "/weatherforecast/security-demo", description = "Demonstrates SQL injection, hardcoded credentials, weak encryption, information disclosure" },
                    new { method = "GET", path = "/weatherforecast/resource-demo", description = "Demonstrates unclosed streams, memory leaks, event handler issues" },
                    new { method = "GET", path = "/weatherforecast/naming-demo", description = "Demonstrates poor naming conventions, god methods" },
                    new { method = "GET", path = "/weatherforecast/exception-demo", description = "Demonstrates exception handling anti-patterns" },
                    new { method = "GET", path = "/weatherforecast/null-demo", description = "Demonstrates null reference issues" },
                    new { method = "GET", path = "/weatherforecast/performance-demo", description = "Demonstrates performance and design anti-patterns" },
                    new { method = "GET", path = "/weatherforecast/get-user/{userId}", description = "SQL Injection vulnerable endpoint (Test with: get-user/1 OR 1=1)" },
                    new { method = "POST", path = "/weatherforecast/process-input", description = "Input processing without validation (JSON body: {\"input\": \"value\"})" },
                    new { method = "POST", path = "/weatherforecast/build-string", description = "Inefficient string concatenation (JSON body: [\"item1\", \"item2\"])" },
                    new { method = "GET", path = "/weatherforecast/divide/{a}/{b}", description = "Division without validation (Try with b=0 for error)" }
                }
            });
        }
    }

    public class ProcessInputRequest
    {
        public string? Input { get; set; }
    }
}
