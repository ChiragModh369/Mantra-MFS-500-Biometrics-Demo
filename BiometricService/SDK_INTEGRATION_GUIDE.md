# C# Fingerprint Service - SDK Integration Guide

## Overview

This guide will help you integrate the Mantra SDK into the C# Fingerprint Service once you provide the SDK DLLs.

---

## Current Architecture

```
Browser (HTML+JS)
       ↓ HTTP
Node.js API (Express)
       ↓ HTTP
C# Web API (ASP.NET Core) ← YOU ARE HERE
       ↓ Mantra SDK
MANTRA MFS500 Device
```

---

## Step 1: Add Mantra SDK DLLs

1. Obtain the Mantra C# SDK DLLs from MANTRA
2. Create a folder: `BiometricService/SDK/`
3. Copy all SDK DLLs to this folder
4. Add references to `BiometricService.csproj`:

```xml
<ItemGroup>
  <Reference Include="MantraAPI">
    <HintPath>SDK\MantraAPI.dll</HintPath>
  </Reference>
  <!-- Add other SDK DLLs as needed -->
</ItemGroup>
```

---

## Step 2: Update MantraFingerprintService.cs

Open [BiometricService/Services/MantraFingerprintService.cs](file:///d:/Node/Test%20Biometrics%20Demo%20MFS%20500/BiometricService/Services/MantraFingerprintService.cs)

### Example SDK Integration (Typical Structure)

```csharp
using MantraAPI; // Replace with actual SDK namespace

public class MantraFingerprintService : IFingerprintService
{
    private readonly ILogger<MantraFingerprintService> _logger;
    private readonly MFS100 _device; // Replace with actual device class

    public MantraFingerprintService(ILogger<MantraFingerprintService> logger)
    {
        _logger = logger;
        _device = new MFS100(); // Initialize device
        _device.InitializeDevice(); // Or whatever initialization method exists
    }

    public async Task<CaptureResponse> CaptureFingerprint(CaptureRequest request)
    {
        try
        {
            // Example capture (adjust based on actual SDK):
            var result = _device.CaptureFingerprint(
                timeout: request.Timeout,
                quality: request.Quality,
                format: request.TemplateFormat
            );

            if (result.ErrorCode == 0) // Success
            {
                return new CaptureResponse
                {
                    Success = true,
                    TemplateData = result.IsoTemplate, // or AnsiTemplate
                    QualityScore = result.Quality,
                    BitmapData = result.BitmapData,
                    ErrorCode = 0
                };
            }
            else
            {
                return new CaptureResponse
                {
                    Success = false,
                    ErrorMessage = result.ErrorDescription,
                    ErrorCode = result.ErrorCode
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Capture failed");
            return new CaptureResponse
            {
                Success = false,
                ErrorMessage = ex.Message,
                ErrorCode = -1
            };
        }
    }

    public async Task<VerifyResponse> VerifyFingerprint(VerifyRequest request)
    {
        try
        {
            // Example matching (adjust based on actual SDK):
            var matchResult = _device.MatchFingerprints(
                template1: request.CapturedTemplate,
                template2: request.EnrolledTemplate
            );

            return new VerifyResponse
            {
                Success = true,
                IsMatch = matchResult.Score >= 80, // Adjust threshold
                MatchScore = matchResult.Score
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Verification failed");
            return new VerifyResponse
            {
                Success = false,
                IsMatch = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<DeviceInfoResponse> GetDeviceInfo()
    {
        try
        {
            var info = _device.GetDeviceInfo();

            return new DeviceInfoResponse
            {
                Success = true,
                DeviceName = info.DeviceName,
                SerialNumber = info.SerialNumber,
                Make = "MANTRA",
                Model = "MFS500",
                IsConnected = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get device info");
            return new DeviceInfoResponse
            {
                Success = false,
                ErrorMessage = ex.Message,
                IsConnected = false
            };
        }
    }

    public async Task<bool> IsDeviceReady()
    {
        try
        {
            return _device.IsConnected(); // Or equivalent method
        }
        catch
        {
            return false;
        }
    }
}
```

---

## Step 3: Common SDK Methods to Look For

Most biometric SDKs have these common methods (names may vary):

### Device Operations

- `Initialize()` / `InitializeDevice()`
- `IsConnected()` / `CheckConnection()`
- `GetDeviceInfo()` / `GetInfo()`
- `Dispose()` / `Uninitialize()`

### Capture Operations

- `Capture()` / `CaptureFingerprint()` / `GetFingerprint()`
- Parameters usually include: timeout, quality, template format

### Matching Operations

- `Match()` / `VerifyFingerprint()` / `MatchTemplates()`
- `Compare()` / `Identify()`
- Returns: score, isMatch, threshold

### Template Operations

- `GetISOTemplate()` / `GetANSITemplate()`
- `ConvertTemplate()`
- `ExtractMinutiae()`

---

## Step 4: Test the C# Service

### 4.1 Build the Project

```bash
cd BiometricService
dotnet build
```

### 4.2 Run the Service

```bash
dotnet run
```

Service will start on `http://localhost:5000`

### 4.3 Test Endpoints

**Health Check:**

```bash
curl http://localhost:5000/api/biometric/health
```

**Capture (with device connected):**

```bash
curl -X POST http://localhost:5000/api/biometric/capture \
  -H "Content-Type: application/json" \
  -d '{"timeout": 30000, "quality": 60, "templateFormat": "ISO"}'
```

---

## Step 5: Configure Node.js to Use C# Service

The Node.js service is already configured! Just ensure in `.env.example`:

```env
CSHARP_SERVICE_URL=http://localhost:5000
CSHARP_SERVICE_TIMEOUT=30000
```

---

## Step 6: Complete Flow Test

1. **Start C# Service** (port 5000)

   ```bash
   cd BiometricService
   dotnet run
   ```

2. **Start Node.js API** (port 3000)

   ```bash
   cd ..
   npm start
   ```

3. **Open Browser**

   ```
   http://localhost:3000
   ```

4. **Test Complete Flow:**
   - Register a user
   - Enroll fingerprint (captures via C# → Mantra SDK)
   - Verify fingerprint (matches via C# → Mantra SDK)

---

## Troubleshooting

### SDK DLL Not Found

- Ensure DLLs are in `SDK/` folder
- Check `csproj` file has correct references
- Try absolute paths in `HintPath`

### Device Not Found

- Check USB connection
- Install Mantra drivers
- Verify device service is running
- Check Windows Device Manager

### Matching Always Fails

- Verify threshold value (too high?)
- Check template format compatibility (ISO vs ANSI)
- Ensure same finger is being used
- Check quality scores

---

## SDK Documentation

Once you provide the SDK, look for:

- `README.md` or `Documentation/` folder
- API reference documentation
- Code samples / examples
- Class diagrams

---

## Next Steps

1. ✅ Provide Mantra C# SDK files
2. Integrate SDK into `MantraFingerprintService.cs`
3. Test capture and matching
4. Adjust match threshold as needed
5. Production deployment

---

## Need Help?

When you provide the SDK, I can help you:

- Integrate the specific SDK methods
- Configure device initialization
- Optimize matching thresholds
- Handle error scenarios
- Deploy the services

Just share the SDK files and I'll update the code accordingly!
