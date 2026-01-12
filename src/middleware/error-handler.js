/**
 * Centralized error handling middleware
 * Catches all errors and returns consistent error responses
 */

const { HTTP_STATUS } = require("../config/constants");

/**
 * Error handler middleware
 * @param {Error} err - Error object
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const errorHandler = (err, req, res, next) => {
  // Log error for debugging
  console.error("Error:", {
    message: err.message,
    stack: process.env.NODE_ENV === "development" ? err.stack : undefined,
    path: req.path,
    method: req.method,
  });

  // Joi validation error
  if (err.isJoi) {
    return res.status(HTTP_STATUS.BAD_REQUEST).json({
      success: false,
      error: "Validation Error",
      details: err.details.map((detail) => detail.message),
    });
  }

  // Sequelize validation error
  if (err.name === "SequelizeValidationError") {
    return res.status(HTTP_STATUS.BAD_REQUEST).json({
      success: false,
      error: "Validation Error",
      details: err.errors.map((e) => e.message),
    });
  }

  // Sequelize unique constraint error
  if (err.name === "SequelizeUniqueConstraintError") {
    return res.status(HTTP_STATUS.CONFLICT).json({
      success: false,
      error: "Duplicate Entry",
      message: "A record with this information already exists",
    });
  }

  // Sequelize foreign key constraint error
  if (err.name === "SequelizeForeignKeyConstraintError") {
    return res.status(HTTP_STATUS.BAD_REQUEST).json({
      success: false,
      error: "Invalid Reference",
      message: "Referenced record does not exist",
    });
  }

  // MySQL duplicate entry error (fallback for raw queries)
  if (err.code === "ER_DUP_ENTRY") {
    return res.status(HTTP_STATUS.CONFLICT).json({
      success: false,
      error: "Duplicate Entry",
      message: "A record with this information already exists",
    });
  }

  // MySQL foreign key constraint error (fallback for raw queries)
  if (err.code === "ER_NO_REFERENCED_ROW_2") {
    return res.status(HTTP_STATUS.BAD_REQUEST).json({
      success: false,
      error: "Invalid Reference",
      message: "Referenced record does not exist",
    });
  }

  // Custom application errors
  if (err.statusCode) {
    return res.status(err.statusCode).json({
      success: false,
      error: err.name || "Application Error",
      message: err.message,
    });
  }

  // Default server error
  res.status(HTTP_STATUS.INTERNAL_SERVER_ERROR).json({
    success: false,
    error: "Internal Server Error",
    message:
      process.env.NODE_ENV === "development"
        ? err.message
        : "An unexpected error occurred",
  });
};

/**
 * 404 Not Found handler
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 */
const notFoundHandler = (req, res) => {
  res.status(HTTP_STATUS.NOT_FOUND).json({
    success: false,
    error: "Not Found",
    message: `Route ${req.method} ${req.path} not found`,
  });
};

module.exports = {
  errorHandler,
  notFoundHandler,
};
