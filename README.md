# BadCode Project - Code Review Plugin Testing Environment

## 🎯 Project Overview

This project provides a comprehensive testing environment for code review plugins. It includes intentional bad code examples integrated into an ASP.NET Core Web API, allowing you to test plugin detection capabilities through HTTP endpoints.

## 📁 Project Structure

```
BadCode/
├── BadCode/                          # Main Web API project
│   ├── Controllers/
│   │   └── WeatherForecastController.cs   # Enhanced with bad code endpoints
│   ├── Properties/
│   ├── appsettings.json
│   ├── Program.cs
│   ├── WeatherForecast.cs
│   └── BadCode.csproj               # References BadCodeExamples
│
├── BadCodeExamples/                 # Bad code examples library
│   ├── SecurityVulnerabilities.cs   # SQL injection, hardcoded creds, weak crypto
│   ├── ResourceManagementIssues.cs  # Memory leaks, undisposed resources
│   ├── PoorNamingDemo.cs            # Poor naming, god methods
│   ├── ExceptionHandlingIssues.cs   # Swallowed exceptions, broad catches
│   ├── NullReferenceIssues.cs       # Missing null checks
│   ├── PerformanceAndDesignAntiPatterns.cs  # Performance issues, design flaws
│   ├── Program.cs
│   └── BadCodeExamples.csproj
│
├── BadCode.slnx                     # Solution file
├── INTEGRATION_GUIDE.md             # Detailed integration documentation
├── README.md                        # This file
└── test-endpoints.ps1              # PowerShell test script

```

## 🚀 Quick Start

### Prerequisites
- **.NET 10.0 SDK** installed
- **Visual Studio Community 2026** (or VS Code)
- **PowerShell 5.0+** for running test scripts

### Step 1: Build the Solution
```powershell
cd D:\Projects\AIAgentCodereview\SampleProject\Code_Review_Project\BadCode
dotnet build BadCode.slnx
```

### Step 2: Run the API
```powershell
dotnet run --project BadCode/BadCode.csproj
```

The API will start at:
- **HTTP**: `http://localhost:5220`
- **HTTPS**: `https://localhost:7030`

### Step 3: Test the Endpoints
```powershell
# Run the test script
.\test-endpoints.ps1
```

Or access the Swagger UI: `https://localhost:7030/swagger`

## 📊 Bad Code Categories

### 1. **Security Vulnerabilities** (5+ issues)
- SQL Injection attacks
- Hardcoded credentials
- Weak encryption algorithms
- No input validation
- Information disclosure

**Endpoint**: `GET /weatherforecast/security-demo`

### 2. **Resource Management** (5+ issues)
- Undisposed streams
- Memory leaks
- Event handler leaks
- Unterminated timers
- Missing using statements

**Endpoint**: `GET /weatherforecast/resource-demo`

### 3. **Poor Naming Conventions** (12+ issues)
- Single-letter variable names
- Unclear method names
- God methods
- Inconsistent naming
- Magic numbers
- Variable shadowing

**Endpoint**: `GET /weatherforecast/naming-demo`

### 4. **Exception Handling Issues** (8+ issues)
- Swallowed exceptions
- Overly broad exception catches
- Exceptions used for control flow
- Missing null checks
- Silent failures

**Endpoint**: `GET /weatherforecast/exception-demo`

### 5. **Null Reference Problems** (10+ issues)
- Missing null checks
- Deep property access
- Unsafe operators
- Implicit null returns
- No validation

**Endpoint**: `GET /weatherforecast/null-demo`

### 6. **Performance & Design Anti-Patterns** (11+ issues)
- String concatenation in loops
- Inefficient LINQ
- Boxing/unboxing
- Hardcoded values
- Thread-unsafe statics
- Dead code

**Endpoint**: `GET /weatherforecast/performance-demo`

## 🧪 Testing Your Code Review Plugin

### Test Endpoints Available

| Endpoint | Method | Purpose | Example |
|----------|--------|---------|---------|
| `/demo-endpoints` | GET | List all endpoints | - |
| `/security-demo` | GET | Test security detection | - |
| `/resource-demo` | GET | Test resource leaks | - |
| `/naming-demo` | GET | Test naming issues | - |
| `/exception-demo` | GET | Test exception handling | - |
| `/null-demo` | GET | Test null references | - |
| `/performance-demo` | GET | Test performance issues | - |
| `/get-user/{id}` | GET | SQL injection test | `/get-user/1 OR 1=1` |
| `/process-input` | POST | Input validation test | `{"input":"data"}` |
| `/build-string` | POST | String concat test | `["a","b","c"]` |
| `/divide/{a}/{b}` | GET | Division validation test | `/divide/10/0` |

### Sample cURL Commands

```bash
# Get all endpoints
curl -k https://localhost:7030/weatherforecast/demo-endpoints

# Test security vulnerabilities
curl -k https://localhost:7030/weatherforecast/security-demo

# Test SQL injection
curl -k "https://localhost:7030/weatherforecast/get-user/1%20OR%201%3D1"

# Test input validation
curl -k -X POST https://localhost:7030/weatherforecast/process-input \
  -H "Content-Type: application/json" \
  -d '{"input":"admin:admin@example.com"}'

# Test exception handling
curl -k https://localhost:7030/weatherforecast/exception-demo

# Test null references
curl -k https://localhost:7030/weatherforecast/null-demo

# Test division by zero
curl -k https://localhost:7030/weatherforecast/divide/10/0
```

### PowerShell Test Script

The included `test-endpoints.ps1` script automatically tests all endpoints:

```powershell
# Run with defaults
.\test-endpoints.ps1

# Run with custom base URL
.\test-endpoints.ps1 -BaseUrl "http://localhost:5220"

# Run without certificate check skip
.\test-endpoints.ps1 -SkipCertCheck:$false
```

## 🔍 What Your Plugin Should Detect

### Compiler Warnings
The BadCodeExamples project generates **42 compiler warnings** including:
- CS0169: Field never used
- CS0219: Variable assigned but never used
- CS8618: Non-nullable field not initialized
- CS8603: Possible null reference return
- CS0414: Field assigned but value never used

### Code Quality Issues

Your plugin should identify:

✅ **Security Issues**
- Unvalidated string concatenation in SQL queries
- Hardcoded secrets and credentials
- Weak cryptographic implementations

✅ **Resource Leaks**
- Streams opened without disposal
- Collections accumulating without cleanup
- Event subscriptions without unsubscribe

✅ **Code Style**
- Single-letter variable names
- Methods performing multiple responsibilities
- Inconsistent naming patterns

✅ **Error Handling**
- Exception handlers that swallow errors
- Missing null validation
- Exceptions used for control flow

✅ **Performance Issues**
- String concatenation in loops
- Boxing operations
- Inefficient collection operations

✅ **Design Flaws**
- Hardcoded magic numbers
- Missing input validation
- Thread-unsafe static state

## 📈 Expected Analysis Results

When your plugin analyzes this code, expect:

| Metric | Expected Count |
|--------|-----------------|
| Total Issues | 50+ |
| Critical/High Severity | 20+ |
| Medium Severity | 20+ |
| Low Severity | 10+ |
| Compiler Warnings | 42 |

## 🛠️ Development Workflow

1. **Run the API** - Start the BadCode service
2. **Execute Tests** - Run test-endpoints.ps1 or use Swagger UI
3. **Analyze Results** - Run your code review plugin
4. **Verify Detection** - Compare plugin results with expected issues
5. **Iterate** - Refine your plugin's detection logic

## 📝 Important Notes

⚠️ **WARNING**: This code is intentionally bad!
- Do NOT use any patterns from these files in production
- All code is marked with comments explaining why it's bad
- The project is purely for testing purposes

## 🔧 Customization

### Adding More Bad Code Examples

1. Create a new class file in `BadCodeExamples/`
2. Implement bad code patterns with comments
3. Add a demonstration method
4. Reference it in `Program.cs` and controller

### Adding More Endpoints

1. Add action methods to `WeatherForecastController.cs`
2. Instantiate your bad code class
3. Call its demonstration method
4. Return descriptive JSON responses

## 🐛 Troubleshooting

### Port Already in Use
If port 5220 or 7030 is already in use:
```powershell
# Kill process using port
Get-Process | Where-Object {$_.Handles -eq 5220} | Stop-Process -Force
```

### Certificate Issues
When running over HTTPS locally:
```powershell
dotnet dev-certs https --trust
```

### Build Fails
Ensure .NET 10.0 is installed:
```powershell
dotnet --version
```

## 📚 Additional Resources

- **INTEGRATION_GUIDE.md** - Detailed integration documentation
- **test-endpoints.ps1** - Automated test script
- **Individual bad code files** - Well-commented examples

## 📞 Support

For issues or questions:
1. Check INTEGRATION_GUIDE.md for detailed documentation
2. Review compiler warnings in build output
3. Examine the bad code files' comments
4. Test endpoints individually using provided scripts

## ✨ Key Features

✅ **Comprehensive Bad Code Examples** - 50+ detectable issues
✅ **Web API Integration** - Test through HTTP endpoints  
✅ **Multiple Categories** - Security, performance, style, etc.
✅ **Easy Testing** - Automated test scripts provided
✅ **Well Documented** - Comments explaining every issue
✅ **.NET 10 Ready** - Modern C# patterns with intentional flaws

---

**Project Type**: .NET 10 Web API + Library
**Purpose**: Code Review Plugin Testing
**Status**: Ready for Testing
**Last Updated**: 2024
