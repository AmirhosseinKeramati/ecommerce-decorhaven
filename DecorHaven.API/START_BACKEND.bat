@echo off
echo ========================================
echo   Decor Haven Backend Startup Script
echo ========================================
echo.

echo [1/3] Checking .NET installation...
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK not found!
    echo Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)
echo OK: .NET SDK installed

echo.
echo [2/3] Navigating to API folder...
cd DecorHaven.API
if %errorlevel% neq 0 (
    echo ERROR: DecorHaven.API folder not found!
    pause
    exit /b 1
)
echo OK: In DecorHaven.API folder

echo.
echo [3/3] Starting backend server...
echo.
echo ========================================
echo   Backend will start at:
echo   - HTTP:  http://localhost:5000
echo   - HTTPS: https://localhost:5001
echo   - Swagger UI at root URL
echo ========================================
echo.
echo Press Ctrl+C to stop the server
echo.

dotnet run

pause

