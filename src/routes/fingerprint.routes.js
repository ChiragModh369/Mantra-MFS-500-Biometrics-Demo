/**
 * Fingerprint Routes
 * Defines all fingerprint-related API endpoints
 */

const express = require("express");
const router = express.Router();
const fingerprintController = require("../controllers/fingerprint.controller");
const { validate, schemas } = require("../middleware/validation");

// Enroll a new fingerprint
router.post(
  "/enroll",
  validate(schemas.enrollFingerprint),
  fingerprintController.enrollFingerprint
);

// Verify a fingerprint
router.post(
  "/verify",
  validate(schemas.verifyFingerprint),
  fingerprintController.verifyFingerprint
);

// Get all fingerprints for a user
router.get("/user/:userId", fingerprintController.getUserFingerprints);

// Delete a fingerprint
router.delete("/:id", fingerprintController.deleteFingerprint);

module.exports = router;
