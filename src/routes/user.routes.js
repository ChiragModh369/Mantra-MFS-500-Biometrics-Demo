/**
 * User Routes
 * Defines all user-related API endpoints
 */

const express = require("express");
const router = express.Router();
const userController = require("../controllers/user.controller");
const { validate, schemas } = require("../middleware/validation");

// Create a new user
router.post("/", validate(schemas.createUser), userController.createUser);

// Get all users
router.get("/", userController.getAllUsers);

// Get user by ID
router.get("/:id", userController.getUserById);

// Delete user
router.delete("/:id", userController.deleteUser);

module.exports = router;
