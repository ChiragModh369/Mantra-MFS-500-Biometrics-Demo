/**
 * Fingerprint Controller
 * Handles fingerprint enrollment and verification logic using Sequelize ORM
 */

const { User, Fingerprint } = require("../models");
const {
  HTTP_STATUS,
  ERROR_MESSAGES,
  SUCCESS_MESSAGES,
  QUALITY_THRESHOLDS,
} = require("../config/constants");

/**
 * Enroll a new fingerprint for a user
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const enrollFingerprint = async (req, res, next) => {
  try {
    const { userId, fingerName, templateData, qualityScore } = req.body;

    // Verify user exists
    const user = await User.findByPk(userId, {
      include: [
        {
          model: Fingerprint,
          as: "fingerprints",
          attributes: ["id", "finger_name", "template_data"],
        },
      ],
    });

    if (!user) {
      return res.status(HTTP_STATUS.NOT_FOUND).json({
        success: false,
        error: ERROR_MESSAGES.USER_NOT_FOUND,
      });
    }

    // Check quality threshold
    if (qualityScore < QUALITY_THRESHOLDS.MINIMUM) {
      return res.status(HTTP_STATUS.BAD_REQUEST).json({
        success: false,
        error: ERROR_MESSAGES.LOW_QUALITY,
        message: `Quality score ${qualityScore} is below minimum threshold of ${QUALITY_THRESHOLDS.MINIMUM}`,
      });
    }

    // DUPLICATE DETECTION: Check if this fingerprint is already enrolled under a different finger name
    if (user.fingerprints && user.fingerprints.length > 0) {
      const biometricService = require("../services/biometric.service");

      for (const existingFingerprint of user.fingerprints) {
        try {
          const matchResult = await biometricService.verifyFingerprint(
            templateData,
            existingFingerprint.template_data
          );

          // If match score is high (>= 80), this is the same finger
          if (matchResult.success && matchResult.isMatch) {
            return res.status(HTTP_STATUS.CONFLICT).json({
              success: false,
              error: "Duplicate fingerprint detected",
              message: `This fingerprint is already enrolled as "${existingFingerprint.finger_name}". Please use a different finger.`,
              data: {
                existingFingerName: existingFingerprint.finger_name,
                matchScore: matchResult.matchScore,
              },
            });
          }
        } catch (verifyError) {
          // Log but continue if individual verification fails
          console.error(
            `Error verifying against ${existingFingerprint.finger_name}:`,
            verifyError.message
          );
        }
      }
    }

    // Create fingerprint (unique constraint will prevent duplicates)
    const fingerprint = await Fingerprint.create({
      user_id: userId,
      finger_name: fingerName,
      template_data: templateData,
      quality_score: qualityScore,
    });

    res.status(HTTP_STATUS.CREATED).json({
      success: true,
      message: SUCCESS_MESSAGES.FINGERPRINT_ENROLLED,
      data: {
        id: fingerprint.id,
        userId: fingerprint.user_id,
        fingerName: fingerprint.finger_name,
        qualityScore: fingerprint.quality_score,
      },
    });
  } catch (error) {
    // Handle Sequelize unique constraint error
    if (error.name === "Sequelize UniqueConstraintError") {
      return res.status(HTTP_STATUS.CONFLICT).json({
        success: false,
        error: "Fingerprint already enrolled",
        message: `${req.body.fingerName} is already enrolled for this user`,
      });
    }
    next(error);
  }
};

/**
 * Verify a fingerprint against stored templates for a user
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const verifyFingerprint = async (req, res, next) => {
  try {
    const { userId, templateData } = req.body;

    // Verify user exists
    const user = await User.findByPk(userId, {
      include: [
        {
          model: Fingerprint,
          as: "fingerprints",
          attributes: ["id", "finger_name", "template_data"],
        },
      ],
    });

    if (!user) {
      return res.status(HTTP_STATUS.NOT_FOUND).json({
        success: false,
        error: ERROR_MESSAGES.USER_NOT_FOUND,
      });
    }

    if (!user.fingerprints || user.fingerprints.length === 0) {
      return res.status(HTTP_STATUS.NOT_FOUND).json({
        success: false,
        error: "No fingerprints enrolled",
        message: "User has no enrolled fingerprints",
      });
    }

    // Use C# Biometric Service for SDK-based matching
    const biometricService = require("../services/biometric.service");

    // Try to match against each enrolled fingerprint
    let bestMatch = null;
    let highestScore = 0;

    for (const fingerprint of user.fingerprints) {
      const matchResult = await biometricService.verifyFingerprint(
        templateData,
        fingerprint.template_data
      );

      if (
        matchResult.success &&
        matchResult.isMatch &&
        matchResult.matchScore > highestScore
      ) {
        highestScore = matchResult.matchScore;
        bestMatch = fingerprint;
      }
    }

    if (bestMatch !== null) {
      res.status(HTTP_STATUS.OK).json({
        success: true,
        message: SUCCESS_MESSAGES.VERIFICATION_SUCCESS,
        data: {
          verified: true,
          userName: user.name,
          matchedFinger: bestMatch.finger_name,
          matchScore: highestScore,
        },
      });
    } else {
      res.status(HTTP_STATUS.UNAUTHORIZED).json({
        success: false,
        error: ERROR_MESSAGES.VERIFICATION_FAILED,
        data: {
          verified: false,
        },
      });
    }
  } catch (error) {
    next(error);
  }
};

/**
 * Get all fingerprints for a specific user
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const getUserFingerprints = async (req, res, next) => {
  try {
    const { userId } = req.params;

    // Verify user exists and get fingerprints
    const user = await User.findByPk(userId, {
      attributes: ["id", "name"],
      include: [
        {
          model: Fingerprint,
          as: "fingerprints",
          attributes: ["id", "finger_name", "quality_score", "created_at"],
          order: [["created_at", "DESC"]],
        },
      ],
    });

    if (!user) {
      return res.status(HTTP_STATUS.NOT_FOUND).json({
        success: false,
        error: ERROR_MESSAGES.USER_NOT_FOUND,
      });
    }

    res.status(HTTP_STATUS.OK).json({
      success: true,
      data: {
        user: {
          id: user.id,
          name: user.name,
        },
        fingerprints: user.fingerprints,
      },
    });
  } catch (error) {
    next(error);
  }
};

/**
 * Delete a specific fingerprint
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const deleteFingerprint = async (req, res, next) => {
  try {
    const { id } = req.params;

    const fingerprint = await Fingerprint.findByPk(id);

    if (!fingerprint) {
      return res.status(HTTP_STATUS.NOT_FOUND).json({
        success: false,
        error: ERROR_MESSAGES.FINGERPRINT_NOT_FOUND,
      });
    }

    await fingerprint.destroy();

    res.status(HTTP_STATUS.OK).json({
      success: true,
      message: SUCCESS_MESSAGES.FINGERPRINT_DELETED,
    });
  } catch (error) {
    next(error);
  }
};

module.exports = {
  enrollFingerprint,
  verifyFingerprint,
  getUserFingerprints,
  deleteFingerprint,
};
