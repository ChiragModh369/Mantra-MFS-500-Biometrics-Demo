# C# Service - .NET Version Fix

## Issue

The C# service won't run due to .NET version mismatch. You have:

- .NET 7.0.20 ✓ (installed)
- .NET 10.0.1 ✓ (installed)

But we're getting runtime errors.

## Quick Solution

**Option 1: Install .NET 8.0 SDK (Recommended)**

This is the most stable LTS version:

1. Download: https://dotnet.microsoft.com/download/dotnet/8.0
2. Install the SDK (not just runtime)
3. Update project back to .NET 8.0

**Option 2: Use .NET 7.0 (Already have it)**

The project is currently set to `net10.0`. Let me set it back to `net7.0`:

1. Update `BiometricService.csproj` to use `net7.0`
2. Clean and rebuild
3. Run

## Commands to Try

### Clean and Rebuild

```bash
cd BiometricService
dotnet clean
dotnet build
dotnet run
```

### If Still Fails

```bash
# Check installed versions
dotnet --list-sdks
dotnet --list-runtimes

# Install .NET 8.0 SDK from link above
# Then change BiometricService.csproj back to net8.0
```

## Temporary Workaround

**If you can't install .NET 8.0 right now**, you can:

### Skip C# Service for Now

The Node.js API will work without the C# service, but verification will use simple template matching (not production-ready).

### Use it Later

Once you have the Mantra SDK and proper .NET version:

1. Install .NET 8.0 SDK
2. Place SDK DLLs in `BiometricService/SDK/`
3. Integrate SDK code
4. Run both services

---

## For Now

The **Node.js service on port 3000** is working fine. You can:

- ✅ Register users
- ✅ Enroll fingerprints (captures will work from browser to device directly)
- ⚠️ Verify fingerprints (will use simple matching, not SDK)

Once .NET 8.0 is installed and C# service is running, you'll get professional SDK-based matching!
