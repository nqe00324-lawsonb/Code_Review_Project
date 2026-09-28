# Bad Code Integration Guide

## Overview
The **BadCodeExamples** project has been integrated into the **BadCode** web API controller. This allows you to easily test your code review plugin against intentional bad code patterns through HTTP endpoints.

## Architecture

### Projects
1. **BadCode** - ASP.NET Core Web API project
2. **BadCodeExamples** - Console library containing bad code classes

### Integration Point
- **WeatherForecastController** - Enhanced with multiple bad code demonstration endpoints

## Available Endpoints

### 1. Demonstration Overview
**GET** `/weatherforecast/demo-endpoints`
- Returns a list of all available bad code demonstration endpoints
- Useful for discovering what tests are available

### 2. Category Demonstrations (General)

#### Security Vulnerabilities
**GET** `/weatherforecast/security-demo`
- Demonstrates:
  - SQL Injection vulnerabilities
  - Hardcoded credentials/passwords
  - Weak encryption (ROT13)
  - No input validation
  - Information disclosure through exceptions

#### Resource Management Issues
**GET** `/weatherforecast/resource-demo`
- Demonstrates:
  - Undisposed streams and file handles
  - Memory leaks from static collections
  - Event handler subscriptions not unsubscribed
  - Timers not stopped
  - Missing using statements for IDisposable

#### Poor Naming Conventions
**GET** `/weatherforecast/naming-demo`
- Demonstrates:
  - Single-letter variable names
  - Unclear method names (DoStuff, CalculateStuff)
  - God methods doing too much
  - Inconsistent naming conventions
  - Magic numbers without explanation
  - Variable shadowing

#### Exception Handling Issues
**GET** `/weatherforecast/exception-demo`
- Demonstrates:
  - Swallowed exceptions silently
  - Catching Exception base class too broadly
  - Using exceptions for control flow
  - Missing null checks
  - Returning default values silently on error

#### Null Reference Issues
**GET** `/weatherforecast/null-demo`
- Demonstrates:
  - Missing null checks
  - Deep property access without validation
  - Unsafe null-conditional operators
  - Assuming non-null return values
  - Returning null implicitly

#### Performance and Design Anti-Patterns
**GET** `/weatherforecast/performance-demo`
- Demonstrates:
  - String concatenation in loops
  - Inefficient LINQ usage
  - Boxing/unboxing overhead
  - Unnecessary object creation
  - Hardcoded values scattered
  - No input validation
  - Thread-unsafe mutable static fields
  - Direct dependencies on concrete classes
  - Dead code
  - Missing switch default cases

### 3. Specific Bad Code Tests

#### SQL Injection Test
**GET** `/weatherforecast/get-user/{userId}`
- Tests SQL injection vulnerability
- Example: `GET /weatherforecast/get-user/1 OR 1=1`
- Demonstrates: Unvalidated string concatenation in SQL queries

#### Input Validation Test
**POST** `/weatherforecast/process-input`
- Tests input processing without validation
- Request body: `{ "input": "value" }`
- Demonstrates: Missing input validation and parsing errors

#### String Concatenation Performance Test
**POST** `/weatherforecast/build-string`
- Tests inefficient string concatenation in loops
- Request body: `["item1", "item2", "item3", "item4", "item5"]`
- Demonstrates: Performance issues from string concatenation

#### Division Without Validation Test
**GET** `/weatherforecast/divide/{a}/{b}`
- Tests division without input validation
- Example: `GET /weatherforecast/divide/10/2` (success)
- Example: `GET /weatherforecast/divide/10/0` (error)
- Demonstrates: Missing null/zero checks

## Running the Application

### Prerequisites
- .NET 10.0 SDK installed
- Visual Studio Community 2026 or VS Code

### Run via Command Line
```powershell
cd D:\Projects\AIAgentCodereview\SampleProject\Code_Review_Project\BadCode
dotnet run --project BadCode/BadCode.csproj
```

### Run via Visual Studio
1. Open the solution file: `BadCode.slnx`
2. Set **BadCode** as the startup project
3. Press **F5** or click **Run**

### Access the API
- Default URL: `https://localhost:7030` (or `http://localhost:5220`)
- Swagger UI: `https://localhost:7030/swagger`

## Testing with a Tool

### Using PowerShell
```powershell
# Test security demo
$response = Invoke-WebRequest -Uri "https://localhost:7030/weatherforecast/security-demo" -SkipCertificateCheck
$response.Content | ConvertFrom-Json | ConvertTo-Json

# Test SQL injection
$response = Invoke-WebRequest -Uri "https://localhost:7030/weatherforecast/get-user/1 OR 1=1" -SkipCertificateCheck
$response.Content | ConvertFrom-Json | ConvertTo-Json

# Test division by zero
$response = Invoke-WebRequest -Uri "https://localhost:7030/weatherforecast/divide/10/0" -SkipCertificateCheck
$response.Content | ConvertFrom-Json | ConvertTo-Json
```

### Using curl
```bash
# Test security demo
curl -k https://localhost:7030/weatherforecast/security-demo

# Test SQL injection
curl -k https://localhost:7030/weatherforecast/get-user/1\ OR\ 1=1

# Test division by zero
curl -k https://localhost:7030/weatherforecast/divide/10/0

# Test input validation
curl -k -X POST https://localhost:7030/weatherforecast/process-input \
  -H "Content-Type: application/json" \
  -d '{"input":"admin:admin@example.com"}'

# Test string concatenation
curl -k -X POST https://localhost:7030/weatherforecast/build-string \
  -H "Content-Type: application/json" \
  -d '["item1","item2","item3","item4","item5"]'
```

### Using Postman
1. Create a new collection
2. Add requests for each endpoint
3. Save and run against the local API

## Code Review Plugin Testing Strategy

Your plugin should be able to detect:

### Security Issues
- [ ] SQL injection vulnerabilities
- [ ] Hardcoded credentials
- [ ] Weak cryptography
- [ ] Information disclosure

### Resource Management
- [ ] Undisposed resources
- [ ] Memory leaks
- [ ] Event handler leaks
- [ ] Missing using statements

### Code Quality
- [ ] Poor naming conventions
- [ ] God methods
- [ ] Dead code
- [ ] Magic numbers

### Error Handling
- [ ] Swallowed exceptions
- [ ] Broad exception catches
- [ ] Control flow with exceptions
- [ ] Missing null checks

### Performance
- [ ] Inefficient string operations
- [ ] Inefficient LINQ queries
- [ ] Boxing/unboxing
- [ ] Unnecessary allocations

## Integration Details

### File Modifications

#### WeatherForecastController.cs
- Added using statement for BadCodeExamples
- Added 6 category demonstration endpoints
- Added 4 specific bad code test endpoints
- Added demo-endpoints discovery endpoint
- Added ProcessInputRequest DTO class

#### BadCode.csproj
- Added ProjectReference to BadCodeExamples

### Compilation Results
- BadCodeExamples: Successfully compiles with 42 warnings
- BadCode: Successfully compiles (no new warnings introduced)

## Monitoring and Analysis

When your code review plugin analyzes these endpoints, it should:

1. **Identify Patterns** - Spot common anti-patterns
2. **Report Issues** - List specific code quality problems
3. **Suggest Fixes** - Provide remediation guidance
4. **Rate Severity** - Classify issues by impact level

### Expected Analysis Results

| Category | Issue Count | Severity |
|----------|------------|----------|
| Security | 5+ | Critical/High |
| Resource Management | 5+ | High |
| Naming/Style | 12+ | Medium/Low |
| Exception Handling | 8+ | High/Medium |
| Null References | 10+ | High |
| Performance/Design | 11+ | Medium/High |
| **Total** | **50+** | **Mixed** |

## Cleanup Notes

- These bad code examples are intentionally written poorly
- Do NOT use these patterns in production code
- The project is for testing purposes only
- All code includes comments explaining why it's bad

## Next Steps

1. Run your code review plugin against specific endpoints
2. Verify it detects the documented issues
3. Compare results with expected analysis patterns
4. Iterate on plugin detection logic
5. Test with additional bad code scenarios as needed

---

**Created**: 2024
**Purpose**: Code Review Plugin Testing
**Status**: Ready for Testing
