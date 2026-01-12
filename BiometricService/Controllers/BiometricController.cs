using Microsoft.AspNetCore.Mvc;
using BiometricService.Models;
using BiometricService.Services;

namespace BiometricService.Controllers
{
    /// <summary>
    /// API Controller for biometric fingerprint operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BiometricController : ControllerBase
    {
        private readonly IFingerprintService _fingerprintService;
        private readonly ILogger<BiometricController> _logger;

        public BiometricController(
            IFingerprintService fingerprintService,
            ILogger<BiometricController> logger)
        {
            _fingerprintService = fingerprintService;
            _logger = logger;
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "healthy",
                service = "Biometric Fingerprint Service",
                timestamp = DateTime.UtcNow
            });
        }

        /// <summary>
        /// Capture a fingerprint from the device
        /// </summary>
        /// <param name="request">Capture parameters</param>
        /// <returns>Fingerprint template data</returns>
        [HttpPost("capture")]
        public async Task<ActionResult<CaptureResponse>> Capture([FromBody] CaptureRequest request)
        {
            _logger.LogInformation("Capture request received");

            var result = await _fingerprintService.CaptureFingerprint(request);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Verify a captured fingerprint against an enrolled template
        /// </summary>
        /// <param name="request">Verification parameters</param>
        /// <returns>Match result and score</returns>
        [HttpPost("verify")]
        public async Task<ActionResult<VerifyResponse>> Verify([FromBody] VerifyRequest request)
        {
            _logger.LogInformation("Verify request received");

            if (string.IsNullOrEmpty(request.CapturedTemplate))
            {
                return BadRequest(new VerifyResponse
                {
                    Success = false,
                    IsMatch = false,
                    ErrorMessage = "Captured template is required"
                });
            }

            if (string.IsNullOrEmpty(request.EnrolledTemplate))
            {
                return BadRequest(new VerifyResponse
                {
                    Success = false,
                    IsMatch = false,
                    ErrorMessage = "Enrolled template is required"
                });
            }

            var result = await _fingerprintService.VerifyFingerprint(request);

            return Ok(result);
        }

        /// <summary>
        /// Get information about the connected device
        /// </summary>
        /// <returns>Device information</returns>
        [HttpGet("device-info")]
        public async Task<ActionResult<DeviceInfoResponse>> GetDeviceInfo()
        {
            _logger.LogInformation("Device info request received");

            var result = await _fingerprintService.GetDeviceInfo();

            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode(503, result); // Service Unavailable
        }

        /// <summary>
        /// Check if device is connected and ready
        /// </summary>
        /// <returns>Device status</returns>
        [HttpGet("device-status")]
        public async Task<IActionResult> GetDeviceStatus()
        {
            var isReady = await _fingerprintService.IsDeviceReady();

            return Ok(new
            {
                connected = isReady,
                timestamp = DateTime.UtcNow
            });
        }
    }
}
