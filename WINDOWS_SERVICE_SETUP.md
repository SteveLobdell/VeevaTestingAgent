# Windows Service Installation Instructions

## Prerequisites
- .NET 8.0 Runtime installed on the target Windows machine
- Administrative privileges for service installation

## Installation Steps

### 1. Build and Publish the Application
```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

### 2. Create Windows Service
Use PowerShell as Administrator:

```powershell
# Create the service
sc create "ReachOutVeevaPromoMats" binPath="C:\Path\To\Your\Published\ReachOutVeevaPromoMats.exe" start=auto displayName="ReachOut Veeva PromoMats Service"

# Set service description
sc description "ReachOutVeevaPromoMats" "Service to sync Veeva promotional materials with ReachOut platform"

# Start the service
sc start "ReachOutVeevaPromoMats"
```

### 3. Alternative: Use dotnet commands (requires .NET 8.0 on target machine)
```powershell
# Install as service
dotnet run --configuration Release --environment Production install

# Start service
net start "ReachOutVeevaPromoMats"
```

## Configuration

### appsettings.json
Update the `appsettings.json` file with your environment-specific settings:

```json
{
  "ReachOutSettings": {
    "InstanceID": "YourInstance",
    "AbleAuthUrl": "https://your-reachout-server.com",
    "AbleAuthUser": "your-api-user@domain.com",
    "AbleAuthSecret": "your-secret-key",
    "ReachOutClientId": "YourClientId"
  },
  "VeevaAPI": {
    "Enabled": true,
    "Url": "https://your-veeva-vault.veevavault.com",
    "User": "your-veeva-user",
    "Password": "your-veeva-password"
  },
  "DropFile": {
    "Enabled": true,
    "Directory": "C:\\Data\\VeevaDrop"
  }
}
```

### NLog.config
The application uses NLog for logging. Update `NLog.config` as needed for your logging requirements.

## Service Management

### Start/Stop Service
```powershell
# Start
net start "ReachOutVeevaPromoMats"

# Stop  
net stop "ReachOutVeevaPromoMats"

# Check status
sc query "ReachOutVeevaPromoMats"
```

### Remove Service
```powershell
# Stop first
net stop "ReachOutVeevaPromoMats"

# Remove
sc delete "ReachOutVeevaPromoMats"
```

## Testing in Interactive Mode

For testing and debugging, you can run the application interactively:

```bash
dotnet run
```

This will execute the work once and exit, allowing you to verify functionality before installing as a service.

## Troubleshooting

### Check Service Status
```powershell
Get-Service "ReachOutVeevaPromoMats"
```

### View Event Logs
Check Windows Event Viewer under:
- Windows Logs > Application
- Applications and Services Logs

### Check NLog Files
By default, logs are written to the application directory. Check for any .log files.

## Upgrade Process

1. Stop the service
2. Replace the application files
3. Update configuration if needed
4. Start the service

```powershell
net stop "ReachOutVeevaPromoMats"
# Replace files
net start "ReachOutVeevaPromoMats"
```