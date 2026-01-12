/**
 * Request validation middleware using Joi
 * Validates incoming requests against defined schemas
 */

const Joi = require("joi");
const { FINGER_POSITIONS, QUALITY_THRESHOLDS } = require("../config/constants");

/**
 * Validation schemas for different endpoints
 */
const schemas = {
  // User creation schema
  createUser: Joi.object({
    name: Joi.string().trim().min(2).max(255).required().messages({
      "string.empty": "Name is required",
      "string.min": "Name must be at least 2 characters",
      "string.max": "Name must not exceed 255 characters",
    }),
    email: Joi.string()
      .trim()
      .email()
      .lowercase()
      .max(255)
      .required()
      .messages({
        "string.empty": "Email is required",
        "string.email": "Email must be valid",
        "string.max": "Email must not exceed 255 characters",
      }),
  }),

  // Fingerprint enrollment schema
  enrollFingerprint: Joi.object({
    userId: Joi.number().integer().positive().required().messages({
      "number.base": "User ID must be a number",
      "number.positive": "User ID must be positive",
      "any.required": "User ID is required",
    }),
    fingerName: Joi.string()
      .valid(...Object.values(FINGER_POSITIONS))
      .required()
      .messages({
        "any.only": "Invalid finger position",
        "any.required": "Finger name is required",
      }),
    templateData: Joi.string().required().messages({
      "string.empty": "Template data is required",
      "any.required": "Template data is required",
    }),
    qualityScore: Joi.number().integer().min(0).max(100).required().messages({
      "number.base": "Quality score must be a number",
      "number.min": "Quality score must be between 0 and 100",
      "number.max": "Quality score must be between 0 and 100",
      "any.required": "Quality score is required",
    }),
  }),

  // Fingerprint verification schema
  verifyFingerprint: Joi.object({
    userId: Joi.number().integer().positive().required().messages({
      "number.base": "User ID must be a number",
      "number.positive": "User ID must be positive",
      "any.required": "User ID is required",
    }),
    templateData: Joi.string().required().messages({
      "string.empty": "Template data is required",
      "any.required": "Template data is required",
    }),
  }),
};

/**
 * Validate request body middleware factory
 * @param {Object} schema - Joi validation schema
 * @returns {Function} Express middleware function
 */
const validate = (schema) => {
  return (req, res, next) => {
    const { error, value } = schema.validate(req.body, {
      abortEarly: false, // Return all errors
      stripUnknown: true, // Remove unknown fields
    });

    if (error) {
      error.isJoi = true;
      return next(error);
    }

    // Replace request body with validated and sanitized value
    req.body = value;
    next();
  };
};

module.exports = {
  schemas,
  validate,
};
