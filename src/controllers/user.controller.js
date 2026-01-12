/**
 * User Controller
 * Handles all user-related business logic using Sequelize ORM
 */

const { User, Fingerprint } = require("../models");
const {
  HTTP_STATUS,
  ERROR_MESSAGES,
  SUCCESS_MESSAGES,
} = require("../config/constants");

/**
 * Create a new user
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const createUser = async (req, res, next) => {
  try {
    const { name, email } = req.body;

    const user = await User.create({ name, email });

    res.status(HTTP_STATUS.CREATED).json({
      success: true,
      message: SUCCESS_MESSAGES.USER_CREATED,
      data: {
        id: user.id,
        name: user.name,
        email: user.email,
      },
    });
  } catch (error) {
    // Handle Sequelize unique constraint error
    if (error.name === "SequelizeUniqueConstraintError") {
      return res.status(HTTP_STATUS.CONFLICT).json({
        success: false,
        error: "Email already exists",
      });
    }
    next(error);
  }
};

/**
 * Get all users with their fingerprint count
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const getAllUsers = async (req, res, next) => {
  try {
    const users = await User.findAll({
      attributes: [
        "id",
        "name",
        "email",
        "created_at",
        [
          User.sequelize.fn("COUNT", User.sequelize.col("fingerprints.id")),
          "fingerprint_count",
        ],
      ],
      include: [
        {
          model: Fingerprint,
          as: "fingerprints",
          attributes: [],
        },
      ],
      group: ["User.id"],
      order: [["created_at", "DESC"]],
      raw: true,
    });

    res.status(HTTP_STATUS.OK).json({
      success: true,
      data: users,
    });
  } catch (error) {
    next(error);
  }
};

/**
 * Get user by ID with all fingerprints
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const getUserById = async (req, res, next) => {
  try {
    const { id } = req.params;

    const user = await User.findByPk(id, {
      include: [
        {
          model: Fingerprint,
          as: "fingerprints",
          attributes: ["id", "finger_name", "quality_score", "created_at"],
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
      data: user,
    });
  } catch (error) {
    next(error);
  }
};

/**
 * Delete user and all associated fingerprints
 * @param {Object} req - Express request object
 * @param {Object} res - Express response object
 * @param {Function} next - Express next function
 */
const deleteUser = async (req, res, next) => {
  try {
    const { id } = req.params;

    const user = await User.findByPk(id);

    if (!user) {
      return res.status(HTTP_STATUS.NOT_FOUND).json({
        success: false,
        error: ERROR_MESSAGES.USER_NOT_FOUND,
      });
    }

    // Delete user (fingerprints will be deleted automatically due to CASCADE)
    await user.destroy();

    res.status(HTTP_STATUS.OK).json({
      success: true,
      message: SUCCESS_MESSAGES.USER_DELETED,
    });
  } catch (error) {
    next(error);
  }
};

module.exports = {
  createUser,
  getAllUsers,
  getUserById,
  deleteUser,
};
