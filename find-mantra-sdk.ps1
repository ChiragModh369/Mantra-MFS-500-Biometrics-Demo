# Mantra SDK DLL Finder Script
# This script searches your system for Mantra SDK DLLs

Write-Host "=================================================" -ForegroundColor Cyan
Write-Host "  Mantra SDK DLL Finder" -ForegroundColor Cyan
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "Searching for Mantra SDK DLLs..." -ForegroundColor Yellow
Write-Host ""

# Common installation paths
$searchPaths = @(
    "C:\Program Files\Mantra",
    "C:\Program Files (x86)\Mantra",
    "C:\Program Files",
    "C:\Program Files (x86)"
)

$foundDlls = @()

foreach ($path in $searchPaths) {
    if (Test-Path $path) {
        Write-Host "Searching in: $path" -ForegroundColor Gray
        
        # Search for Mantra DLLs
        $dlls = Get-ChildItem -Path $path -Recurse -Filter "Mantra*.dll" -ErrorAction SilentlyContinue
        $dlls += Get-ChildItem -Path $path -Recurse -Filter "MFS*.dll" -ErrorAction SilentlyContinue
        
        foreach ($dll in $dlls) {
            $foundDlls += $dll
            Write-Host "  ✓ Found: $($dll.FullName)" -ForegroundColor Green
        }
    }
}

Write-Host ""
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host "  RESULTS" -ForegroundColor Cyan
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host ""

if ($foundDlls.Count -gt 0) {
    Write-Host "Found $($foundDlls.Count) SDK DLL(s):" -ForegroundColor Green
    Write-Host ""
    
    foreach ($dll in $foundDlls) {
        Write-Host "Name:     $($dll.Name)" -ForegroundColor Cyan
        Write-Host "Path:     $($dll.DirectoryName)" -ForegroundColor Gray
        Write-Host "Size:     $([math]::Round($dll.Length / 1KB, 2)) KB" -ForegroundColor Gray
        Write-Host "Modified: $($dll.LastWriteTime)" -ForegroundColor Gray
        Write-Host ""
    }
    
    Write-Host "=================================================" -ForegroundColor Cyan
    Write-Host "  NEXT STEPS" -ForegroundColor Cyan
    Write-Host "=================================================" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "1. Create SDK folder:" -ForegroundColor Yellow
    Write-Host "   mkdir 'd:\Node\Test Biometrics Demo MFS 500\BiometricService\SDK'" -ForegroundColor White
    Write-Host ""
    Write-Host "2. Copy the DLLs (example for first found DLL):" -ForegroundColor Yellow
    if ($foundDlls.Count -gt 0) {
        Write-Host "   Copy-Item '$($foundDlls[0].DirectoryName)\*.dll' -Destination 'd:\Node\Test Biometrics Demo MFS 500\BiometricService\SDK\'" -ForegroundColor White
    }
    Write-Host ""
    Write-Host "3. Restart the C# service" -ForegroundColor Yellow
    Write-Host ""
    
} else {
    Write-Host "❌ No Mantra SDK DLLs found!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Possible reasons:" -ForegroundColor Yellow
    Write-Host "  1. SDK is installed in a non-standard location" -ForegroundColor Gray
    Write-Host "  2. SDK was not installed with the device driver" -ForegroundColor Gray
    Write-Host "  3. Using a different SDK package" -ForegroundColor Gray
    Write-Host ""
    Write-Host "Suggestions:" -ForegroundColor Yellow
    Write-Host "  1. Check your device purchase documentation" -ForegroundColor Gray
    Write-Host "  2. Contact Mantra Softech support" -ForegroundColor Gray
    Write-Host "  3. Check the MorFin Auth Test application installation folder" -ForegroundColor Gray
    Write-Host ""
    
    # Try to find the MorFin Auth Test application
    Write-Host "Looking for MorFin Auth Test application..." -ForegroundColor Yellow
    $morfin = Get-Process -Name "*MorFin*" -ErrorAction SilentlyContinue
    if ($morfin) {
        Write-Host "  ✓ Found MorFin running. Check its installation folder." -ForegroundColor Green
        foreach ($proc in $morfin) {
            try {
                Write-Host "  Location: $($proc.Path)" -ForegroundColor Cyan
            } catch {}
        }
    }
}

Write-Host ""
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host "Press any key to exit..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
