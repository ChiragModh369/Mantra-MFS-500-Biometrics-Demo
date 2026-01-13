using BiometricService.Models;
using MorFin_Auth;

namespace BiometricService.Services
{
    /// <summary>
    /// Implementation of fingerprint service using Mantra MorFin_Auth SDK
    /// </summary>
    public class MantraFingerprintService : IFingerprintService, IDisposable
    {
        private readonly ILogger<MantraFingerprintService> _logger;
        private readonly IConfiguration _configuration;
        private MorFinAuth? _morFinAuth;
        private string _deviceName;
        private bool _isInitialized;
        private bool _sdkAvailable;
        private string _sdkError;
        private FINGER_DEVICE_INFO _deviceInfo;
        private readonly int _defaultTimeout;
        private readonly int _defaultQuality;
        private readonly string _clientKey;
        private readonly object _lockObject = new object();

        public MantraFingerprintService(
            ILogger<MantraFingerprintService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _deviceName = string.Empty;
            _isInitialized = false;
            _sdkAvailable = false;
            _sdkError = string.Empty;

            // Load configuration
            _defaultTimeout = _configuration.GetValue<int>("MantraDevice:Timeout", 30000);
            _defaultQuality = _configuration.GetValue<int>("MantraDevice:Quality", 60);
            _clientKey = _configuration.GetValue<string>("MantraDevice:ClientKey", string.Empty) ?? string.Empty;

            // Try to initialize SDK - this may fail if native dependencies are missing
            try
            {
                _morFinAuth = new MorFinAuth();
                _sdkAvailable = true;
                _logger.LogInformation("MorFinAuth SDK initialized successfully");
            }
            catch (Exception ex)
            {
                _sdkAvailable = false;
                _sdkError = $"SDK initialization failed: {ex.Message}";
                _logger.LogError(ex, "Failed to initialize MorFinAuth SDK.");
                _logger.LogError($"Exception Type: {ex.GetType().FullName}");
                _logger.LogError($"Exception Message: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }
                _logger.LogWarning("Device operations will not be available until the Mantra driver is installed.");
            }
        }

        /// <summary>
        /// Get connected device name
        /// </summary>
        private string GetConnectedDeviceName()
        {
            if (!_sdkAvailable || _morFinAuth == null)
            {
                return string.Empty;
            }

            try
            {
                List<string> deviceList = new List<string>();
                int ret = _morFinAuth.GetConnectedDevices(deviceList);

                if (ret == 0 && deviceList.Count > 0)
                {
                    return deviceList[0]; // Return first connected device
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting connected device");
                return string.Empty;
            }
        }

        /// <summary>
        /// Initialize device if not already initialized
        /// </summary>
        private bool EnsureDeviceInitialized(bool forceReinit = false)
        {
            lock (_lockObject)
            {
                // If force re-init requested, uninitialize first
                if (forceReinit && _isInitialized)
                {
                    try
                    {
                        _logger.LogWarning("Force re-initialization requested, uninitializing device first");
                        _morFinAuth?.Uninit();
                        _isInitialized = false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error during forced uninitialization");
                        _isInitialized = false;
                    }
                }

                if (_isInitialized)
                {
                    return true;
                }

                try
                {
                    _deviceName = GetConnectedDeviceName();
                    if (string.IsNullOrEmpty(_deviceName))
                    {
                        _logger.LogWarning("No device connected");
                        return false;
                    }

                    _deviceInfo = new FINGER_DEVICE_INFO();
                    int ret = _morFinAuth!.Init(_deviceName, ref _deviceInfo, _clientKey);

                    if (ret == 0)
                    {
                        _isInitialized = true;
                        _logger.LogInformation($"Device initialized: {_deviceInfo.Make} {_deviceInfo.Model}, SN: {_deviceInfo.SerialNo}");
                        return true;
                    }
                    else
                    {
                        _logger.LogError($"Device initialization failed: {_morFinAuth.GetErrDescription(ret)}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error initializing device");
                    return false;
                }
            }
        }

        /// <summary>
        /// Reset device initialization state
        /// </summary>
        private void ResetDeviceState()
        {
            lock (_lockObject)
            {
                _isInitialized = false;
            }
        }

        /// <summary>
        /// Check if device is ready
        /// </summary>
        public async Task<bool> IsDeviceReady()
        {
            if (!_sdkAvailable || _morFinAuth == null)
            {
                return false;
            }

            try
            {
                _deviceName = GetConnectedDeviceName();
                if (string.IsNullOrEmpty(_deviceName))
                {
                    return false;
                }

                int ret = _morFinAuth.IsConnected(_deviceName);
                return ret == 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking device status");
                return false;
            }
        }

        /// <summary>
        /// Get device information
        /// </summary>
        public async Task<DeviceInfoResponse> GetDeviceInfo()
        {
            if (!_sdkAvailable || _morFinAuth == null)
            {
                return new DeviceInfoResponse
                {
                    Success = false,
                    ErrorMessage = _sdkError,
                    IsConnected = false
                };
            }

            try
            {
                if (!EnsureDeviceInitialized())
                {
                    return new DeviceInfoResponse
                    {
                        Success = false,
                        ErrorMessage = "Device not connected or initialization failed",
                        IsConnected = false
                    };
                }

                return new DeviceInfoResponse
                {
                    Success = true,
                    DeviceName = _deviceName,
                    SerialNumber = _deviceInfo.SerialNo,
                    Make = _deviceInfo.Make,
                    Model = _deviceInfo.Model,
                    IsConnected = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting device info");
                return new DeviceInfoResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    IsConnected = false
                };
            }
        }

        /// <summary>
        /// Capture fingerprint using Mantra SDK
        /// </summary>
        public async Task<CaptureResponse> CaptureFingerprint(CaptureRequest request)
        {
            if (!_sdkAvailable || _morFinAuth == null)
            {
                return new CaptureResponse
                {
                    Success = false,
                    ErrorMessage = _sdkError,
                    ErrorCode = -1
                };
            }

            // Try capture, with automatic retry on device initialization failure
            return await AttemptCaptureWithRetry(request, retryCount: 1);
        }

        /// <summary>
        /// Attempt capture with automatic retry on initialization failures
        /// </summary>
        private async Task<CaptureResponse> AttemptCaptureWithRetry(CaptureRequest request, int retryCount)
        {
            try
            {
                if (!EnsureDeviceInitialized())
                {
                    return new CaptureResponse
                    {
                        Success = false,
                        ErrorMessage = "Device not initialized",
                        ErrorCode = -1
                    };
                }

                _logger.LogInformation("Starting fingerprint capture...");

                int quality = 0;
                int nfiq = 0;
                int timeout = request.Timeout > 0 ? request.Timeout : _defaultTimeout;
                int qualityThreshold = request.Quality > 0 ? request.Quality : _defaultQuality;

                // Use AutoCapture for automatic quality-based capture
                int ret = await Task.Run(() => _morFinAuth!.AutoCapture(out quality, out nfiq, timeout, qualityThreshold));

                if (ret != 0)
                {
                    string errorMsg = _morFinAuth.GetErrDescription(ret);
                    
                    // Check if error is due to device not being initialized
                    if (errorMsg.Contains("not initialized", StringComparison.OrdinalIgnoreCase) && retryCount > 0)
                    {
                        _logger.LogWarning($"Device initialization lost, attempting automatic recovery (retries left: {retryCount})");
                        ResetDeviceState();
                        
                        // Force re-initialization and retry
                        if (EnsureDeviceInitialized(forceReinit: true))
                        {
                            _logger.LogInformation("Device re-initialized successfully, retrying capture");
                            return await AttemptCaptureWithRetry(request, retryCount - 1);
                        }
                    }

                    _logger.LogError($"Capture failed: {errorMsg}");
                    return new CaptureResponse
                    {
                        Success = false,
                        ErrorMessage = errorMsg,
                        ErrorCode = ret
                    };
                }

                // Get template data
                byte[]? templateData = null;
                TEMPLATE_FORMAT templateFormat = TEMPLATE_FORMAT.FMR_V2005; // ISO format

                ret = _morFinAuth.GetTemplate(out templateData, templateFormat, 5);
                if (ret != 0)
                {
                    return new CaptureResponse
                    {
                        Success = false,
                        ErrorMessage = _morFinAuth.GetErrDescription(ret),
                        ErrorCode = ret
                    };
                }

                // Get bitmap image
                byte[]? bitmapData = null;
                ret = _morFinAuth.GetImage(out bitmapData, IMAGE_FORMAT.BMP, 5);

                string? templateBase64 = templateData != null ? Convert.ToBase64String(templateData) : null;
                string? bitmapBase64 = bitmapData != null ? Convert.ToBase64String(bitmapData) : null;

                _logger.LogInformation($"Capture successful - Quality: {quality}, NFIQ: {nfiq}");

                return new CaptureResponse
                {
                    Success = true,
                    TemplateData = templateBase64,
                    QualityScore = quality,
                    BitmapData = bitmapBase64,
                    ErrorCode = 0
                };
            }
            catch (Exception ex)
            {
                // On exception, try one more time with force re-init if retries available
                if (retryCount > 0 && (ex.Message.Contains("not initialized", StringComparison.OrdinalIgnoreCase) ||
                                       ex.Message.Contains("device", StringComparison.OrdinalIgnoreCase)))
                {
                    _logger.LogWarning(ex, $"Exception during capture, attempting recovery (retries left: {retryCount})");
                    ResetDeviceState();
                    
                    if (EnsureDeviceInitialized(forceReinit: true))
                    {
                        return await AttemptCaptureWithRetry(request, retryCount - 1);
                    }
                }

                _logger.LogError(ex, "Error capturing fingerprint");
                return new CaptureResponse
                {
                    Success = false,
                    ErrorMessage = $"Capture failed: {ex.Message}",
                    ErrorCode = -1
                };
            }
        }

        /// <summary>
        /// Verify fingerprint using Mantra SDK matching
        /// </summary>
        public async Task<VerifyResponse> VerifyFingerprint(VerifyRequest request)
        {
            if (!_sdkAvailable || _morFinAuth == null)
            {
                return new VerifyResponse
                {
                    Success = false,
                    IsMatch = false,
                    ErrorMessage = _sdkError,
                    MatchScore = 0
                };
            }

            try
            {
                _logger.LogInformation("Attempting to verify fingerprint...");

                // Validate input
                if (string.IsNullOrEmpty(request.CapturedTemplate) ||
                    string.IsNullOrEmpty(request.EnrolledTemplate))
                {
                    return new VerifyResponse
                    {
                        Success = false,
                        IsMatch = false,
                        ErrorMessage = "Both captured and enrolled templates are required"
                    };
                }

                // Convert base64 templates to byte arrays
                byte[] capturedBytes = Convert.FromBase64String(request.CapturedTemplate);
                byte[] enrolledBytes = Convert.FromBase64String(request.EnrolledTemplate);

                int matchScore = 0;
                TEMPLATE_FORMAT templateFormat = TEMPLATE_FORMAT.FMR_V2005; // ISO format

                int ret = await Task.Run(() => _morFinAuth.MatchTemplate(
                    capturedBytes,
                    capturedBytes.Length,
                    enrolledBytes,
                    enrolledBytes.Length,
                    out matchScore,
                    templateFormat
                ));

                if (ret != 0)
                {
                    string errorMsg = _morFinAuth.GetErrDescription(ret);
                    _logger.LogError($"Matching failed: {errorMsg}");
                    return new VerifyResponse
                    {
                        Success = false,
                        IsMatch = false,
                        ErrorMessage = errorMsg,
                        MatchScore = 0
                    };
                }

                // Determine if it's a match based on threshold (typically 80+)
                bool isMatch = matchScore >= 80;

                _logger.LogInformation($"Verification completed - Match: {isMatch}, Score: {matchScore}");

                return new VerifyResponse
                {
                    Success = true,
                    IsMatch = isMatch,
                    MatchScore = matchScore,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying fingerprint");
                return new VerifyResponse
                {
                    Success = false,
                    IsMatch = false,
                    ErrorMessage = $"Verification failed: {ex.Message}",
                    MatchScore = 0
                };
            }
        }

        /// <summary>
        /// Cleanup resources
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_isInitialized && _morFinAuth != null)
                {
                    _morFinAuth.Uninit();
                    _isInitialized = false;
                    _logger.LogInformation("Device uninitialized");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during disposal");
            }
        }
    }
}
