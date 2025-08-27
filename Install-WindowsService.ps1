# Windows Service Installation Script for ReachOut Veeva PromoMats
# Run this script as Administrator

param(
    [Parameter(Mandatory=$true)]
    [string]$InstallPath,
    
    [Parameter(Mandatory=$false)]
    [string]$ServiceName = "ReachOutVeevaPromoMats",
    
    [Parameter(Mandatory=$false)]
    [string]$DisplayName = "ReachOut Veeva PromoMats Service",
    
    [Parameter(Mandatory=$false)]
    [ValidateSet("install", "uninstall", "start", "stop", "status")]
    [string]$Action = "install"
)

$ErrorActionPreference = "Stop"

function Test-Administrator {
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
    $principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
    return $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

function Install-Service {
    param($Path, $Name, $Display)
    
    Write-Host "Installing service '$Name' from path '$Path'..." -ForegroundColor Green
    
    if (!(Test-Path $Path)) {
        throw "Executable not found at: $Path"
    }
    
    # Check if service already exists
    $existingService = Get-Service -Name $Name -ErrorAction SilentlyContinue
    if ($existingService) {
        Write-Host "Service '$Name' already exists. Stopping and removing..." -ForegroundColor Yellow
        Stop-Service -Name $Name -Force -ErrorAction SilentlyContinue
        & sc.exe delete $Name
        Start-Sleep -Seconds 2
    }
    
    # Create the service
    & sc.exe create $Name binPath="$Path" start=auto displayName="$Display"
    
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to create service. Exit code: $LASTEXITCODE"
    }
    
    # Set description
    & sc.exe description $Name "Service to sync Veeva promotional materials with ReachOut platform"
    
    Write-Host "Service '$Name' installed successfully!" -ForegroundColor Green
    Write-Host "You can start it with: Start-Service '$Name'" -ForegroundColor Gray
}

function Uninstall-Service {
    param($Name)
    
    Write-Host "Uninstalling service '$Name'..." -ForegroundColor Yellow
    
    $service = Get-Service -Name $Name -ErrorAction SilentlyContinue
    if (!$service) {
        Write-Host "Service '$Name' not found." -ForegroundColor Red
        return
    }
    
    # Stop the service if running
    if ($service.Status -eq "Running") {
        Write-Host "Stopping service..." -ForegroundColor Yellow
        Stop-Service -Name $Name -Force
    }
    
    # Remove the service
    & sc.exe delete $Name
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Service '$Name' uninstalled successfully!" -ForegroundColor Green
    } else {
        Write-Host "Failed to uninstall service. Exit code: $LASTEXITCODE" -ForegroundColor Red
    }
}

function Start-ServiceHelper {
    param($Name)
    
    Write-Host "Starting service '$Name'..." -ForegroundColor Green
    Start-Service -Name $Name
    
    $service = Get-Service -Name $Name
    Write-Host "Service status: $($service.Status)" -ForegroundColor Gray
}

function Stop-ServiceHelper {
    param($Name)
    
    Write-Host "Stopping service '$Name'..." -ForegroundColor Yellow
    Stop-Service -Name $Name -Force
    
    $service = Get-Service -Name $Name
    Write-Host "Service status: $($service.Status)" -ForegroundColor Gray
}

function Get-ServiceStatus {
    param($Name)
    
    $service = Get-Service -Name $Name -ErrorAction SilentlyContinue
    if ($service) {
        Write-Host "Service '$Name' status: $($service.Status)" -ForegroundColor Gray
        Write-Host "Start type: $($service.StartType)" -ForegroundColor Gray
    } else {
        Write-Host "Service '$Name' not found." -ForegroundColor Red
    }
}

# Main script
try {
    if (!(Test-Administrator)) {
        throw "This script must be run as Administrator"
    }
    
    switch ($Action.ToLower()) {
        "install" {
            if (!$InstallPath) {
                throw "InstallPath parameter is required for installation"
            }
            Install-Service -Path $InstallPath -Name $ServiceName -Display $DisplayName
        }
        "uninstall" {
            Uninstall-Service -Name $ServiceName
        }
        "start" {
            Start-ServiceHelper -Name $ServiceName
        }
        "stop" {
            Stop-ServiceHelper -Name $ServiceName
        }
        "status" {
            Get-ServiceStatus -Name $ServiceName
        }
        default {
            throw "Unknown action: $Action"
        }
    }
}
catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host "Operation completed successfully!" -ForegroundColor Green