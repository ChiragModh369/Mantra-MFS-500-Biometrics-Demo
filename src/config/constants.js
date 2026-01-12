/**
 * Application constants
 * All constant values used throughout the application
 */

const FINGER_POSITIONS = {
  LEFT_THUMB: "Left Thumb",
  LEFT_INDEX: "Left Index",
  LEFT_MIDDLE: "Left Middle",
  LEFT_RING: "Left Ring",
  LEFT_LITTLE: "Left Little",
  RIGHT_THUMB: "Right Thumb",
  RIGHT_INDEX: "Right Index",
  RIGHT_MIDDLE: "Right Middle",
  RIGHT_RING: "Right Ring",
  RIGHT_LITTLE: "Right Little",
};

const QUALITY_THRESHOLDS = {
  MINIMUM: parseInt(process.env.MIN_QUALITY_SCORE) || 60,
  GOOD: 75,
  EXCELLENT: 90,
};

const HTTP_STATUS = {
  OK: 200,
  CREATED: 201,
  BAD_REQUEST: 400,
  UNAUTHORIZED: 401,
  NOT_FOUND: 404,
  CONFLICT: 409,
  INTERNAL_SERVER_ERROR: 500,
};

const ERROR_MESSAGES = {
  USER_NOT_FOUND: "User not found",
  FINGERPRINT_NOT_FOUND: "Fingerprint not found",
  DUPLICATE_EMAIL: "Email already exists",
  INVALID_INPUT: "Invalid input data",
  LOW_QUALITY: "Fingerprint quality too low",
  VERIFICATION_FAILED: "Fingerprint verification failed",
  DATABASE_ERROR: "Database operation failed",
  DEVICE_ERROR: "Device communication error",
};

const SUCCESS_MESSAGES = {
  USER_CREATED: "User created successfully",
  USER_DELETED: "User deleted successfully",
  FINGERPRINT_ENROLLED: "Fingerprint enrolled successfully",
  FINGERPRINT_DELETED: "Fingerprint deleted successfully",
  VERIFICATION_SUCCESS: "Fingerprint verified successfully",
};

const DEVICE_CONFIG = {
  SERVICE_URL: process.env.DEVICE_SERVICE_URL || "http://127.0.0.1:8005",
  TIMEOUT: parseInt(process.env.DEVICE_TIMEOUT) || 30000,
  IMAGE_FORMAT: "ISO",
  DEVICE_TYPE: "MANTRA_MFS500",
};

module.exports = {
  FINGER_POSITIONS,
  QUALITY_THRESHOLDS,
  HTTP_STATUS,
  ERROR_MESSAGES,
  SUCCESS_MESSAGES,
  DEVICE_CONFIG,
};
