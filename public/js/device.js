/**
 * Biometric Device Integration - Professional Architecture
 * All device operations route through Node.js API → C# Service → Mantra SDK
 *
 * CRITICAL: Frontend does NOT directly communicate with device anymore
 * All operations go through proper service layer for security and consistency
 */

class BiometricDevice {
  constructor() {
    this.isConnected = false;
    this.connectionState = {
      status: "unknown",
      message: "Initializing...",
    };
    this.timeout = 30000;
    this.apiBaseUrl = window.location.origin + "/api";
  }

  /**
   * Test device connectivity through C# service
   * @returns {Promise<boolean>} Connection status
   */
  async testConnection() {
    try {
      const response = await fetch(
        `${this.apiBaseUrl}/biometric/device-status`,
        {
          method: "GET",
        }
      );

      if (response.ok) {
        const data = await response.json();

        // Handle new status object format
        if (data.status) {
          this.connectionState = data;
          this.isConnected = data.status === "connected";
        } else {
          // Fallback for old format
          this.isConnected = data.connected || false;
          this.connectionState = {
            status: this.isConnected ? "connected" : "disconnected",
            message: this.isConnected
              ? "Device connected"
              : "Device not connected",
          };
        }

        return this.isConnected;
      }

      this.isConnected = false;
      this.connectionState = {
        status: "service_error",
        message: "Failed to reach backend service",
      };
      return false;
    } catch (error) {
      console.error("Device connection error:", error);
      this.isConnected = false;
      this.connectionState = {
        status: "service_error",
        message: "Network error or backend unreachable",
      };
      return false;
    }
  }

  /**
   * Capture fingerprint through Node.js → C# Service → Mantra SDK
   * @param {Object} options - Capture options
   * @returns {Promise<Object>} Fingerprint data with template and quality
   */
  async captureFingerprint(options = {}) {
    try {
      const response = await fetch(`${this.apiBaseUrl}/biometric/capture`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          timeout: options.timeout || this.timeout,
          quality: options.quality || 60,
          templateFormat: options.templateFormat || "ISO",
        }),
      });

      if (!response.ok) {
        const error = await response.json();
        throw new Error(error.error || "Failed to capture fingerprint");
      }

      const data = await response.json();

      if (!data.success) {
        throw new Error(data.errorMessage || "Fingerprint capture failed");
      }

      return {
        success: true,
        templateData: data.templateData,
        qualityScore: data.qualityScore,
        bitmapData: data.bitmapData,
      };
    } catch (error) {
      console.error("Fingerprint capture error:", error);
      return {
        success: false,
        error: error.message,
        templateData: null,
        qualityScore: 0,
      };
    }
  }

  /**
   * Get device information through service layer
   * @returns {Promise<Object>} Device details
   */
  async getDeviceInfo() {
    try {
      const response = await fetch(`${this.apiBaseUrl}/biometric/device-info`, {
        method: "GET",
      });

      if (!response.ok) {
        throw new Error("Failed to get device information");
      }

      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Device info error:", error);
      return {
        success: false,
        error: error.message,
      };
    }
  }

  /**
   * Initialize device connection
   * @returns {Promise<boolean>} Initialization status
   */
  async initialize() {
    const connected = await this.testConnection();

    if (connected) {
      console.log("✓ Biometric device connected via service layer");
    } else {
      console.warn("✗ Biometric device not detected");
      console.warn("Please ensure:");
      console.warn("1. C# Service is running (port 5050)");
      console.warn("2. Device is connected via USB");
      console.warn("3. Device drivers are installed");
      console.warn("4. Mantra SDK is integrated in C# service");
    }

    return connected;
  }

  /**
   * Check device status
   * @returns {boolean} Current connection status
   */
  getStatus() {
    return this.isConnected;
  }

  /**
   * Get detailed connection state
   * @returns {Object} Connection state object
   */
  getConnectionState() {
    return this.connectionState;
  }
}

// Export for use in app.js
window.BiometricDevice = BiometricDevice;
