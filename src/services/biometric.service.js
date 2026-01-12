/**
 * Biometric Service Client
 * HTTP client for communicating with C# fingerprint service
 */

const { DEVICE_CONFIG } = require("../config/constants");

/**
 * Biometric service configuration
 */
const BIOMETRIC_SERVICE_URL =
  process.env.CSHARP_SERVICE_URL || "http://localhost:5050";
const SERVICE_TIMEOUT = parseInt(process.env.CSHARP_SERVICE_TIMEOUT) || 30000;

/**
 * Make HTTP request to C# service
 * @param {string} endpoint - API endpoint
 * @param {string} method - HTTP method
 * @param {Object} body - Request body
 * @returns {Promise<Object>} Response data
 */
const makeRequest = async (endpoint, method = "GET", body = null) => {
  const url = `${BIOMETRIC_SERVICE_URL}${endpoint}`;

  const options = {
    method,
    headers: {
      "Content-Type": "application/json",
    },
    timeout: SERVICE_TIMEOUT,
  };

  if (body) {
    options.body = JSON.stringify(body);
  }

  try {
    const response = await fetch(url, options);

    if (!response.ok) {
      const error = await response
        .json()
        .catch(() => ({ error: "Unknown error" }));
      throw new Error(
        error.errorMessage || error.message || "Service request failed"
      );
    }

    return await response.json();
  } catch (error) {
    console.error(`Biometric service error (${endpoint}):`, error.message);
    throw error;
  }
};

/**
 * Check if C# service is available
 * @returns {Promise<boolean>} Service availability
 */
const isServiceAvailable = async () => {
  try {
    const response = await makeRequest("/api/biometric/health");
    return response.status === "healthy";
  } catch (error) {
    console.warn("C# fingerprint service is not available:", error.message);
    return false;
  }
};

/**
 * Capture fingerprint from device via C# service
 * @param {Object} options - Capture options
 * @returns {Promise<Object>} Capture result
 */
const captureFingerprint = async (options = {}) => {
  const request = {
    timeout: options.timeout || DEVICE_CONFIG.TIMEOUT,
    quality: options.quality || 60,
    templateFormat: options.templateFormat || DEVICE_CONFIG.IMAGE_FORMAT,
  };

  try {
    const response = await makeRequest(
      "/api/biometric/capture",
      "POST",
      request
    );

    return {
      success: response.success,
      templateData: response.templateData,
      qualityScore: response.qualityScore,
      bitmapData: response.bitmapData,
      errorMessage: response.errorMessage,
      errorCode: response.errorCode,
    };
  } catch (error) {
    return {
      success: false,
      errorMessage: error.message,
      templateData: null,
      qualityScore: 0,
    };
  }
};

/**
 * Verify fingerprint templates via C# service
 * @param {string} capturedTemplate - Newly captured template
 * @param {string} enrolledTemplate - Stored template to match against
 * @returns {Promise<Object>} Verification result
 */
const verifyFingerprint = async (capturedTemplate, enrolledTemplate) => {
  try {
    const response = await makeRequest("/api/biometric/verify", "POST", {
      capturedTemplate,
      enrolledTemplate,
    });

    return {
      success: response.success,
      isMatch: response.isMatch,
      matchScore: response.matchScore,
      errorMessage: response.errorMessage,
    };
  } catch (error) {
    return {
      success: false,
      isMatch: false,
      matchScore: 0,
      errorMessage: error.message,
    };
  }
};

/**
 * Get device information via C# service
 * @returns {Promise<Object>} Device information
 */
const getDeviceInfo = async () => {
  try {
    return await makeRequest("/api/biometric/device-info");
  } catch (error) {
    return {
      success: false,
      errorMessage: error.message,
      isConnected: false,
    };
  }
};

/**
 * Check device status via C# service
 * @returns {Promise<Object>} Device connection status object
 */
const isDeviceConnected = async () => {
  try {
    const response = await makeRequest("/api/biometric/device-status");

    if (response.connected === true) {
      return {
        status: "connected",
        message: "Device connected and ready",
        details: response,
      };
    } else {
      return {
        status: "disconnected",
        message: "Device not found or disconnected",
        details: response,
      };
    }
  } catch (error) {
    // If request fails, it means C# service is likely down
    return {
      status: "service_error",
      message: "Biometric Service is not running",
      error: error.message,
    };
  }
};

module.exports = {
  isServiceAvailable,
  captureFingerprint,
  verifyFingerprint,
  getDeviceInfo,
  isDeviceConnected,
};
