#!/usr/bin/env pwsh

# Bad Code API Test Script
# This script tests the integrated bad code API endpoints

param(
	[string]$BaseUrl = "http://localhost:5220",
	[switch]$SkipCertCheck = $true
)

function Test-Endpoint {
	param(
		[string]$EndpointPath,
		[string]$Method = "GET",
		[object]$Body = $null,
		[string]$Description = ""
	)

	$fullUrl = "$BaseUrl$EndpointPath"

	Write-Host ""
	Write-Host "========================================" -ForegroundColor Cyan
	Write-Host "Testing: $Description" -ForegroundColor Yellow
	Write-Host "URL: $fullUrl" -ForegroundColor Gray
	Write-Host "Method: $Method" -ForegroundColor Gray

	try {
		$params = @{
			Uri    = $fullUrl
			Method = $Method
		}

		if ($SkipCertCheck) {
			$params["SkipCertificateCheck"] = $true
		}

		if ($Body) {
			$params["Body"] = $Body | ConvertTo-Json
			$params["ContentType"] = "application/json"
		}

		$response = Invoke-WebRequest @params

		Write-Host "Status: $($response.StatusCode) - $($response.StatusDescription)" -ForegroundColor Green

		if ($response.Content) {
			$content = $response.Content | ConvertFrom-Json
			Write-Host "Response:" -ForegroundColor Green
			Write-Host ($content | ConvertTo-Json -Depth 3 | Out-String) -ForegroundColor White
		}
	}
	catch {
		Write-Host "Status: Error" -ForegroundColor Red
		Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red

		if ($_.Exception.Response) {
			$statusCode = [int]$_.Exception.Response.StatusCode
			$statusDesc = $_.Exception.Response.StatusDescription
			Write-Host "HTTP Status: $statusCode - $statusDesc" -ForegroundColor Red
		}
	}
}

Write-Host ""
Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Magenta
Write-Host "║  Bad Code API Integration Test Suite                   ║" -ForegroundColor Magenta
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Magenta
Write-Host ""
Write-Host "Base URL: $BaseUrl" -ForegroundColor Cyan
Write-Host ""
Write-Host "Note: Make sure the BadCode API is running before executing these tests" -ForegroundColor Yellow
Write-Host ""

# Get available endpoints
Test-Endpoint -EndpointPath "/weatherforecast/demo-endpoints" `
	-Description "Get All Available Bad Code Demonstration Endpoints"

# Test category demonstrations
Test-Endpoint -EndpointPath "/weatherforecast/security-demo" `
	-Description "Security Vulnerabilities Demo"

Test-Endpoint -EndpointPath "/weatherforecast/resource-demo" `
	-Description "Resource Management Issues Demo"

Test-Endpoint -EndpointPath "/weatherforecast/naming-demo" `
	-Description "Poor Naming Conventions Demo"

Test-Endpoint -EndpointPath "/weatherforecast/exception-demo" `
	-Description "Exception Handling Issues Demo"

Test-Endpoint -EndpointPath "/weatherforecast/null-demo" `
	-Description "Null Reference Issues Demo"

Test-Endpoint -EndpointPath "/weatherforecast/performance-demo" `
	-Description "Performance and Design Anti-Patterns Demo"

# Test specific bad code patterns
Test-Endpoint -EndpointPath "/weatherforecast/get-user/1" `
	-Description "SQL Injection Test (Normal ID)"

Test-Endpoint -EndpointPath "/weatherforecast/get-user/1%20OR%201%3D1" `
	-Description "SQL Injection Test (SQL Injection Attempt)"

Test-Endpoint -EndpointPath "/weatherforecast/process-input" `
	-Method "POST" `
	-Body @{ input = "admin:admin@example.com" } `
	-Description "Input Processing without Validation"

Test-Endpoint -EndpointPath "/weatherforecast/build-string" `
	-Method "POST" `
	-Body @("item1", "item2", "item3", "item4", "item5") `
	-Description "String Concatenation Performance Test"

Test-Endpoint -EndpointPath "/weatherforecast/divide/10/2" `
	-Description "Division Test (Valid Input)"

Test-Endpoint -EndpointPath "/weatherforecast/divide/10/0" `
	-Description "Division Test (Division by Zero - Error Expected)"

Write-Host ""
Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Magenta
Write-Host "║  Test Suite Completed                                  ║" -ForegroundColor Magenta
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Magenta
Write-Host ""
