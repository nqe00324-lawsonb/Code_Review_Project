@echo off
REM Bad Code API Test Script - Windows Batch Version
REM Tests the integrated bad code API endpoints

setlocal enabledelayedexpansion

set "BASE_URL=http://localhost:5220"
if not "%1"=="" set "BASE_URL=%1"

echo.
echo ╔════════════════════════════════════════════════════════╗
echo ║  Bad Code API Integration Test Suite - Windows         ║
echo ╚════════════════════════════════════════════════════════╝
echo.
echo Base URL: %BASE_URL%
echo.
echo Note: Make sure the BadCode API is running before executing these tests
echo       Install curl if not available: https://curl.se/download.html
echo.

REM Test 1: Get all endpoints
echo ========================================
echo Testing: Get All Available Endpoints
echo ========================================
curl -s "%BASE_URL%/weatherforecast/demo-endpoints" | findstr /c:"endpoints" && echo. || echo FAILED
echo.

REM Test 2: Security demo
echo ========================================
echo Testing: Security Vulnerabilities Demo
echo ========================================
curl -s "%BASE_URL%/weatherforecast/security-demo" | findstr /c:"security" && echo. || echo FAILED
echo.

REM Test 3: Resource demo
echo ========================================
echo Testing: Resource Management Issues Demo
echo ========================================
curl -s "%BASE_URL%/weatherforecast/resource-demo" | findstr /c:"resource" && echo. || echo FAILED
echo.

REM Test 4: Naming demo
echo ========================================
echo Testing: Poor Naming Conventions Demo
echo ========================================
curl -s "%BASE_URL%/weatherforecast/naming-demo" | findstr /c:"naming" && echo. || echo FAILED
echo.

REM Test 5: Exception demo
echo ========================================
echo Testing: Exception Handling Issues Demo
echo ========================================
curl -s "%BASE_URL%/weatherforecast/exception-demo" | findstr /c:"exception" && echo. || echo FAILED
echo.

REM Test 6: Null demo
echo ========================================
echo Testing: Null Reference Issues Demo
echo ========================================
curl -s "%BASE_URL%/weatherforecast/null-demo" | findstr /c:"null" && echo. || echo FAILED
echo.

REM Test 7: Performance demo
echo ========================================
echo Testing: Performance and Design Anti-Patterns Demo
echo ========================================
curl -s "%BASE_URL%/weatherforecast/performance-demo" | findstr /c:"performance" && echo. || echo FAILED
echo.

REM Test 8: SQL injection normal
echo ========================================
echo Testing: SQL Injection Test - Normal ID
echo ========================================
curl -s "%BASE_URL%/weatherforecast/get-user/1" | findstr /c:"User lookup" && echo. || echo FAILED
echo.

REM Test 9: SQL injection attack
echo ========================================
echo Testing: SQL Injection Test - SQL Attack Attempt
echo ========================================
curl -s "%BASE_URL%/weatherforecast/get-user/1%%20OR%%201%%3D1" | findstr /c:"User lookup" && echo. || echo FAILED
echo.

REM Test 10: Division valid
echo ========================================
echo Testing: Division Test - Valid Input
echo ========================================
curl -s "%BASE_URL%/weatherforecast/divide/10/2" | findstr /c:"result" && echo. || echo FAILED
echo.

REM Test 11: Division by zero
echo ========================================
echo Testing: Division Test - Division by Zero (Error Expected)
echo ========================================
curl -s "%BASE_URL%/weatherforecast/divide/10/0" | findstr /c:"Division" && echo. || echo FAILED
echo.

echo ╔════════════════════════════════════════════════════════╗
echo ║  Test Suite Completed                                  ║
echo ╚════════════════════════════════════════════════════════╝
echo.
pause
