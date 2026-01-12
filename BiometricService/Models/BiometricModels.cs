namespace BiometricService.Models
{
    /// <summary>
    /// Request model for fingerprint capture
    /// </summary>
    public class CaptureRequest
    {
        /// <summary>
        /// Timeout for capture operation in milliseconds
        /// </summary>
        public int Timeout { get; set; } = 30000;

        /// <summary>
        /// Minimum quality score required (0-100)
        /// </summary>
        public int Quality { get; set; } = 60;

        /// <summary>
        /// Template format (ISO or ANSI)
        /// </summary>
        public string TemplateFormat { get; set; } = "ISO";
    }

    /// <summary>
    /// Response model for fingerprint capture
    /// </summary>
    public class CaptureResponse
    {
        /// <summary>
        /// Indicates if capture was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Fingerprint template data (base64 encoded)
        /// </summary>
        public string? TemplateData { get; set; }

        /// <summary>
        /// Quality score of captured fingerprint (0-100)
        /// </summary>
        public int QualityScore { get; set; }

        /// <summary>
        /// Bitmap data of fingerprint image (optional)
        /// </summary>
        public string? BitmapData { get; set; }

        /// <summary>
        /// Error message if capture failed
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Error code from device
        /// </summary>
        public int ErrorCode { get; set; }
    }

    /// <summary>
    /// Request model for fingerprint verification
    /// </summary>
    public class VerifyRequest
    {
        /// <summary>
        /// Captured template to verify
        /// </summary>
        public string CapturedTemplate { get; set; } = string.Empty;

        /// <summary>
        /// Enrolled template to match against
        /// </summary>
        public string EnrolledTemplate { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response model for fingerprint verification
    /// </summary>
    public class VerifyResponse
    {
        /// <summary>
        /// Indicates if verification was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Indicates if fingerprints match
        /// </summary>
        public bool IsMatch { get; set; }

        /// <summary>
        /// Match score/confidence (0-100)
        /// </summary>
        public int MatchScore { get; set; }

        /// <summary>
        /// Error message if verification failed
        /// </summary>
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// Response model for device information
    /// </summary>
    public class DeviceInfoResponse
    {
        public bool Success { get; set; }
        public string? DeviceName { get; set; }
        public string? SerialNumber { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public bool IsConnected { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
