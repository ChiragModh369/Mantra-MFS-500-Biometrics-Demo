using BiometricService.Models;

namespace BiometricService.Services
{
    /// <summary>
    /// Interface for fingerprint operations
    /// Abstracts the underlying SDK implementation
    /// </summary>
    public interface IFingerprintService
    {
        /// <summary>
        /// Capture a fingerprint from the connected device
        /// </summary>
        /// <param name="request">Capture parameters</param>
        /// <returns>Capture result with template data</returns>
        Task<CaptureResponse> CaptureFingerprint(CaptureRequest request);

        /// <summary>
        /// Verify a captured fingerprint against an enrolled template
        /// </summary>
        /// <param name="request">Verification parameters</param>
        /// <returns>Verification result with match score</returns>
        Task<VerifyResponse> VerifyFingerprint(VerifyRequest request);

        /// <summary>
        /// Get information about the connected device
        /// </summary>
        /// <returns>Device information</returns>
        Task<DeviceInfoResponse> GetDeviceInfo();

        /// <summary>
        /// Check if device is connected and ready
        /// </summary>
        /// <returns>True if device is ready</returns>
        Task<bool> IsDeviceReady();
    }
}
