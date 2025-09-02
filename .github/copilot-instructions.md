# ReachOutVeevaPromoMats
Windows Service application that integrates with Veeva Vault promotional materials API and ReachOut platform. The service polls Veeva for document updates or processes dropped CSV files and forwards the data to ReachOut for synchronization.

**ALWAYS reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Working Effectively

### Build Requirements - CRITICAL LIMITATION
**This application CANNOT be built on Linux.** The solution requires Windows with .NET Framework 4.8 SDK due to:
- Main project targets .NET Framework 4.8 (not supported on Linux)
- Mixed architecture with .NET Standard 2.0 models library
- Missing .NET Framework 4.8 reference assemblies on Linux

### What DOES work on Linux:
- Package restoration: `mono nuget.exe restore ReachOutVeevaPromoMats.sln` -- takes 3 seconds (cached) to 45 seconds (first time). NEVER CANCEL.
- Building Models library only: `cd ReachOutAuth.Models && dotnet build` -- takes 1-2 seconds.
- Code analysis, exploration, and editing

### Windows Build Commands (for reference):
- Restore packages: `nuget restore ReachOutVeevaPromoMats.sln`
- Build solution: `msbuild ReachOutVeevaPromoMats.sln /p:Configuration=Debug` -- estimated 30-60 seconds. NEVER CANCEL.
- Build release: `msbuild ReachOutVeevaPromoMats.sln /p:Configuration=Release`

## Validation Scenarios

**Since building requires Windows, validation on Linux is limited to:**

### Configuration Validation
- Review `App.config` for correct settings:
  - `veevaapienabled` should be `false` for development
  - `dropfileenabled` can be `true` for testing file processing
  - `debug` should be `true` to prevent actual API calls during development
  - Drop file directory should exist: `D:\Development\ReachOut\VeevaDrop` (Windows path)

#### Example validation commands:
```bash
# Check key configuration values
grep -n "veevaapienabled\|dropfileenabled\|debug" App.config

# Verify logging configuration exists
ls -la NLog.config

# Ensure required directories exist in project
ls -la API/ Models/ Configurations/
```

### Code Quality Validation
- Ensure all `using` statements are properly referenced
- Verify configuration keys in `TenantConfiguration.cs` match `App.config`
- Check that logging is properly configured in `NLog.config`
- Validate API endpoints and authentication settings

### Manual Testing Scenarios (Windows Only)
When building on Windows, test these scenarios:

#### Interactive Console Mode:
1. Run `ReachOutVeevaPromoMats.exe` from command line
2. Verify it runs `BrokerService.doWork()` once and exits
3. Check log files are created in `./logs/` directory
4. Confirm no exceptions in logs

#### Service Mode Testing:
1. Install as Windows Service: `sc create ReachOutVeevaPromoMats binPath= "C:\path\to\ReachOutVeevaPromoMats.exe"`
2. Start service: `sc start ReachOutVeevaPromoMats`
3. Verify service starts without errors
4. Check logs for proper timer initialization
5. Stop service: `sc stop ReachOutVeevaPromoMats`

#### File Drop Testing:
1. Enable file drop: set `dropfileenabled="true"` in App.config
2. Create drop directory if it doesn't exist
3. Place test CSV file in drop directory
4. Run application and verify file is processed and archived
5. Check ReachOut API receives the data (if not in debug mode)

## Repository Structure

### Key Projects:
- **ReachOutVeevaPromoMats** (.NET Framework 4.8): Main Windows Service application
- **ReachOutAuth.Models** (.NET Standard 2.0): Shared models and data contracts

### Important Files:
- `Program.cs`: Entry point - detects interactive vs service mode
- `BrokerService.cs`: Windows Service implementation with timers
- `BusinessLogic.cs`: Core business logic for API and file processing
- `Configurations/TenantConfiguration.cs`: Configuration management
- `App.config`: Application configuration (connection strings, API settings, file paths)
- `NLog.config`: Logging configuration
- `packages.config`: NuGet package dependencies

### Key Directories:
- `API/`: Veeva API integration classes
- `Models/`: API response models and authentication
- `Lib/`: Third-party libraries
- `ReachOutAuth.Models/`: Shared models project

## Common Tasks

### Package Management:
- Download NuGet: `wget https://dist.nuget.org/win-x86-commandline/latest/nuget.exe`
- Restore packages: `mono nuget.exe restore ReachOutVeevaPromoMats.sln` -- takes 3 seconds (cached) to 45 seconds (first time). NEVER CANCEL.

### Development Workflow:
1. Always work in Debug mode with `debug="true"` in App.config
2. Make code changes using any text editor
3. If on Windows: Build and test interactively first, then as service
4. If on Linux: Validate configuration and code quality only
5. Check logs in `./logs/` directory for any issues

### Configuration Management:
- Application settings are in `App.config`
- Tenant-specific settings handled in `TenantConfiguration.cs`
- Always validate configuration keys exist and have correct types
- Debug mode prevents actual API calls to external services

### Security Notes:
- Contains API credentials in `App.config` (in production, use secure configuration)
- Uses Bearer token authentication with ReachOut platform
- Basic authentication with Veeva API

## Known Issues and Limitations:
- Cannot build on Linux due to .NET Framework 4.8 dependency
- Some NuGet packages have security vulnerabilities (RestSharp, System.Text.Json) - documented for future updates
- Mono xbuild is deprecated and doesn't support .NET Framework 4.8
- Mixed framework targeting (4.8 + Standard 2.0) causes compatibility issues with older tooling

## Testing:
- No automated unit tests currently exist in the repository
- Testing is manual through interactive console mode or Windows Service installation
- Validation focuses on configuration correctness and log file analysis