/**
 * Express Server Configuration
 * Main application entry point
 */

const express = require("express");
const bodyParser = require("body-parser");
const cors = require("cors");
const path = require("path");
const http = require("http");
const { Server } = require("socket.io");
require("dotenv").config();

const { testConnection } = require("./models");
const { errorHandler, notFoundHandler } = require("./middleware/error-handler");
const userRoutes = require("./routes/user.routes");
const fingerprintRoutes = require("./routes/fingerprint.routes");

const app = express();
const PORT = process.env.PORT || 3000;

// Create HTTP server for socket.io integration
const httpServer = http.createServer(app);
const io = new Server(httpServer, {
  cors: {
    origin: "*",
    methods: ["GET", "POST"],
  },
});

// Middleware
app.use(cors());
app.use(bodyParser.json({ limit: "10mb" })); // Increased limit for fingerprint templates
app.use(bodyParser.urlencoded({ extended: true, limit: "10mb" }));

// Request logging middleware
app.use((req, res, next) => {
  // console.log(`${new Date().toISOString()} - ${req.method} ${req.path}`);
  next();
});

// Serve static files from public directory
app.use(express.static(path.join(__dirname, "../public")));

// API Routes
const biometricRoutes = require("./routes/biometric.routes");
app.use("/api/biometric", biometricRoutes);
app.use("/api/users", userRoutes);
app.use("/api/fingerprints", fingerprintRoutes);

// Health check endpoint
app.get("/api/health", (req, res) => {
  res.json({
    success: true,
    message: "Server is running",
    timestamp: new Date().toISOString(),
  });
});

// 404 handler
app.use(notFoundHandler);

// Error handling middleware (must be last)
app.use(errorHandler);

// WebSocket connection handling
const biometricService = require("./services/biometric.service");

let deviceStatusCache = {
  status: "unknown",
  message: "Checking device...",
  lastChecked: null,
};

// Function to check device status and broadcast to all clients
const checkAndBroadcastDeviceStatus = async () => {
  try {
    const status = await biometricService.isDeviceConnected();
    const newStatus = {
      ...status,
      lastChecked: new Date().toISOString(),
    };

    // Only broadcast if status changed or this is first check
    if (
      !deviceStatusCache.lastChecked ||
      deviceStatusCache.status !== newStatus.status
    ) {
      deviceStatusCache = newStatus;
      io.emit("device-status", deviceStatusCache);
    } else {
      deviceStatusCache = newStatus;
    }
  } catch (error) {
    console.error("Error checking device status:", error.message);
  }
};

io.on("connection", (socket) => {
  // Send current device status immediately to new client
  socket.emit("device-status", deviceStatusCache);

  socket.on("disconnect", () => {
    // Client disconnected
  });
});

// Start server
const startServer = async () => {
  try {
    // Test database connection
    const dbConnected = await testConnection();

    if (!dbConnected) {
      console.error(
        "Failed to connect to database. Please check your configuration."
      );
      console.log("\nMake sure to:");
      console.log("1. Update .env file with your MySQL credentials");
      console.log("2. Ensure MySQL server is running");
      process.exit(1);
    }

    // Start listening
    httpServer.listen(PORT, () => {
      console.log("\n=================================");
      console.log(`✓ Server running on port ${PORT}`);
      console.log(`✓ Environment: ${process.env.NODE_ENV || "development"}`);
      console.log(`✓ API URL: http://localhost:${PORT}/api`);
      console.log(`✓ Web App: http://localhost:${PORT}`);
      console.log(`✓ WebSocket: enabled`);
      console.log("=================================\n");

      // Start periodic device status monitoring (every 3 seconds)
      setInterval(checkAndBroadcastDeviceStatus, 3000);

      // Initial device status check
      checkAndBroadcastDeviceStatus();
    });
  } catch (error) {
    console.error("Failed to start server:", error.message);
    process.exit(1);
  }
};

startServer();

module.exports = app;
