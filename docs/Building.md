# Building & Publishing Guide

## Prerequisites
- Windows 10 or Windows 11 PC / Laptop
- .NET 8.0 SDK or higher
- Visual Studio 2022+ with WPF workload

## Build Commands
```bash
# Restore NuGet dependencies
dotnet restore

# Build Release binary
dotnet build -c Release

# Execute application
dotnet run --project src/CollegeIdManagement/CollegeIdManagement.csproj

# Run Unit Tests
dotnet test
```

## Creating Installer (Inno Setup)
1. Open `installer/CollegeIdManagement.iss` in Inno Setup Compiler.
2. Compile setup script to generate `College_ID_Management_Setup.exe`.
