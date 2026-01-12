/**
 * Biometric Routes
 * Provides proxy endpoints for C# biometric service operations
 */

const express = require("express");
const router = express.Router();
const biometricService = require("../services/biometric.service");

/**
 * Capture fingerprint through C# service
 * POST /api/biometric/capture
 */
router.post("/capture", async (req, res, next) => {
  try {
    const { timeout, quality, templateFormat } = req.body;

    const result = await biometricService.captureFingerprint({
      timeout: timeout || 30000,
      quality: quality || 60,
      templateFormat: templateFormat || "ISO",
    });

    if (result.success) {
      res.status(200).json(result);
    } else {
      res.status(500).json({
        success: false,
        error: "Fingerprint capture failed",
        errorMessage: result.errorMessage,
      });
    }
  } catch (error) {
    next(error);
  }
});

/**
 * Get device information through C# service
 * GET /api/biometric/device-info
 */
router.get("/device-info", async (req, res, next) => {
  try {
    const info = await biometricService.getDeviceInfo();
    res.status(200).json(info);
  } catch (error) {
    next(error);
  }
});

/**
 * Check device connection status through C# service
 * GET /api/biometric/device-status
 */
router.get("/device-status", async (req, res, next) => {
  try {
    const status = await biometricService.isDeviceConnected();
    console.log("device status", status);
    res.status(200).json(status);
  } catch (error) {
    next(error);
  }
});

module.exports = router;
