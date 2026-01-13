/**
 * Main Application Logic
 * Handles UI interactions and API communication
 */

// API Configuration
const API_BASE_URL = window.location.origin + "/api";

// Finger positions available for selection
const FINGER_POSITIONS = [
  "Left Thumb",
  "Left Index",
  "Left Middle",
  "Left Ring",
  "Left Little",
  "Right Thumb",
  "Right Index",
  "Right Middle",
  "Right Ring",
  "Right Little",
];

// Initialize device
let device;

// Application State
const app = {
  currentUser: null,
  users: [],
  deviceConnected: false,
};

/**
 * Initialize application
 */
document.addEventListener("DOMContentLoaded", async () => {
  console.log("Biometric Fingerprint Management System - Initializing...");

  // Initialize device
  device = new BiometricDevice();
  app.deviceConnected = await device.initialize();
  updateDeviceStatus();

  // Listen for real-time device status changes
  device.onStatusChange((status) => {
    app.deviceConnected = status.status === "connected";
    updateDeviceStatus();
  });

  // Populate finger select options
  populateFingerSelect();

  // Load users
  await loadUsers();

  // Event listeners
  setupEventListeners();

  console.log("✓ Application initialized");
  console.log("✓ Real-time device monitoring enabled");
});

/**
 * Setup all event listeners
 */
function setupEventListeners() {
  // User registration form
  document
    .getElementById("userForm")
    .addEventListener("submit", handleUserRegistration);

  // Fingerprint enrollment form
  document
    .getElementById("enrollmentForm")
    .addEventListener("submit", handleFingerprintEnrollment);

  // Capture fingerprint button
  document
    .getElementById("captureBtn")
    .addEventListener("click", captureFingerprint);

  // Verification form
  document
    .getElementById("verificationForm")
    .addEventListener("submit", handleFingerprintVerification);

  // Verify capture button
  document
    .getElementById("verifyCaptureBtn")
    .addEventListener("click", captureFingerprintForVerification);
}

/**
 * Update device status indicator
 */
function updateDeviceStatus() {
  const statusElement = document.getElementById("deviceStatus");
  const indicatorElement = document.getElementById("statusIndicator");

  if (app.deviceConnected) {
    statusElement.textContent = "Device Connected";
    statusElement.title = "Ready for capture";
    indicatorElement.className = "status-indicator connected";
  } else {
    // Check detailed state
    const state = device.getConnectionState();

    if (state.status === "service_error") {
      statusElement.textContent = "Service Offline";
      statusElement.title =
        "C# Biometric Service is not running. Please run: dotnet run --project BiometricService";
      indicatorElement.className = "status-indicator offline";
      // Also show a persistent alert if it's a service error
      if (!document.querySelector(".service-alert")) {
        const alert = document.createElement("div");
        alert.className = "alert alert-error service-alert";
        alert.style.marginBottom = "1rem";
        alert.innerHTML = `
                <strong>⚠️ Service Offline</strong><br>
                The Biometric Service is not running.<br>
                Please run: <code>dotnet run --project BiometricService</code>
            `;
        const container = document.querySelector(".container");
        container.insertBefore(alert, container.firstChild);
      }
    } else {
      statusElement.textContent = "Device Not Connected";
      statusElement.title = "Please connect USB device";
      indicatorElement.className = "status-indicator disconnected";
      // Remove service alert if it exists
      const serviceAlert = document.querySelector(".service-alert");
      if (serviceAlert) serviceAlert.remove();
    }
  }
}

/**
 * Populate finger select dropdown
 */
function populateFingerSelect() {
  const select = document.getElementById("fingerSelect");
  select.innerHTML = '<option value="">Select finger position</option>';

  FINGER_POSITIONS.forEach((finger) => {
    const option = document.createElement("option");
    option.value = finger;
    option.textContent = finger;
    select.appendChild(option);
  });
}

/**
 * Show alert message using toast notifications
 */
function showAlert(message, type = "success") {
  if (window.toast) {
    toast.show(message, type);
  } else {
    // Fallback if toast not loaded
    console.log(`[${type.toUpperCase()}] ${message}`);
  }
}

/**
 * Handle user registration
 */
async function handleUserRegistration(e) {
  e.preventDefault();

  const name = document.getElementById("userName").value.trim();
  const email = document.getElementById("userEmail").value.trim();
  const submitBtn = e.target.querySelector('button[type="submit"]');

  if (!name || !email) {
    showAlert("Please fill in all fields", "error");
    return;
  }

  submitBtn.classList.add("loading");
  submitBtn.disabled = true;
  const originalText = submitBtn.innerHTML;

  try {
    const response = await fetch(`${API_BASE_URL}/users`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name, email }),
    });

    const data = await response.json();

    if (data.success) {
      showAlert(`User ${name} registered successfully!`, "success");
      document.getElementById("userForm").reset();
      await loadUsers();
    } else {
      showAlert(data.error || "Registration failed", "error");
    }
  } catch (error) {
    console.error("Registration error:", error);
    showAlert("Failed to register user", "error");
  } finally {
    submitBtn.classList.remove("loading");
    submitBtn.disabled = false;
    submitBtn.innerHTML = originalText;
  }
}

/**
 * Load all users
 */
async function loadUsers() {
  try {
    const response = await fetch(`${API_BASE_URL}/users`);
    const data = await response.json();

    if (data.success) {
      app.users = data.data;
      renderUserList();
      updateUserSelects();
    }
  } catch (error) {
    console.error("Load users error:", error);
  }
}

/**
 * Render user list
 */
function renderUserList() {
  const userList = document.getElementById("userList");

  if (app.users.length === 0) {
    userList.innerHTML =
      '<p style="text-align: center; color: var(--text-muted);">No users registered yet</p>';
    return;
  }

  userList.innerHTML = app.users
    .map(
      (user) => `
    <div class="user-item">
      <div class="user-info">
        <h3>${escapeHtml(user.name)}</h3>
        <p>${escapeHtml(user.email)}</p>
        <span class="fingerprint-badge">
          🔒 ${user.fingerprint_count} fingerprint${
        user.fingerprint_count !== 1 ? "s" : ""
      }
        </span>
      </div>
      <div class="user-actions">
        <button class="btn btn-danger btn-small" onclick="deleteUser(${
          user.id
        })">Delete</button>
      </div>
    </div>
  `
    )
    .join("");
}

/**
 * Update user select dropdowns
 */
function updateUserSelects() {
  const enrollSelect = document.getElementById("enrollUserSelect");
  const verifySelect = document.getElementById("verifyUserSelect");

  const options = app.users
    .map(
      (user) =>
        `<option value="${user.id}">${escapeHtml(user.name)} (${escapeHtml(
          user.email
        )})</option>`
    )
    .join("");

  const defaultOption = '<option value="">Select a user</option>';
  enrollSelect.innerHTML = defaultOption + options;
  verifySelect.innerHTML = defaultOption + options;
}

/**
 * Delete user
 */
async function deleteUser(userId) {
  if (
    !confirm(
      "Are you sure you want to delete this user and all their fingerprints?"
    )
  ) {
    return;
  }

  try {
    const response = await fetch(`${API_BASE_URL}/users/${userId}`, {
      method: "DELETE",
    });

    const data = await response.json();

    if (data.success) {
      showAlert("User deleted successfully", "success");
      await loadUsers();
    } else {
      showAlert(data.error || "Failed to delete user", "error");
    }
  } catch (error) {
    console.error("Delete user error:", error);
    showAlert("Failed to delete user", "error");
  }
}

/**
 * Capture fingerprint for enrollment
 */
async function captureFingerprint() {
  const captureBtn = document.getElementById("captureBtn");
  const preview = document.getElementById("fingerprintPreview");

  if (!app.deviceConnected) {
    showAlert("Device not connected. Please check device connection.", "error");
    return;
  }

  captureBtn.disabled = true;
  captureBtn.innerHTML =
    '<span class="spinner"></span> Place finger on scanner...';
  preview.innerHTML =
    '<div class="fingerprint-icon">👆</div><p>Scanning...</p>';

  try {
    const result = await device.captureFingerprint();

    if (result.success) {
      document.getElementById("capturedTemplate").value = result.templateData;
      document.getElementById("capturedQuality").value = result.qualityScore;

      preview.innerHTML = `
        <div class="fingerprint-icon">✓</div>
        <p>Fingerprint captured successfully!</p>
        <p style="color: var(--text-secondary); font-size: 0.9rem;">Quality Score: ${result.qualityScore}%</p>
      `;

      showAlert(
        `Fingerprint captured with quality score: ${result.qualityScore}%`,
        "success"
      );
    } else {
      preview.innerHTML =
        '<div class="fingerprint-icon">❌</div><p>Capture failed</p>';
      showAlert(result.error || "Failed to capture fingerprint", "error");
    }
  } catch (error) {
    console.error("Capture error:", error);
    preview.innerHTML =
      '<div class="fingerprint-icon">❌</div><p>Capture failed</p>';
    showAlert("Failed to capture fingerprint", "error");
  } finally {
    captureBtn.disabled = false;
    captureBtn.innerHTML = "📷 Capture Fingerprint";
  }
}

/**
 * Handle fingerprint enrollment
 */
async function handleFingerprintEnrollment(e) {
  e.preventDefault();

  const userId = document.getElementById("enrollUserSelect").value;
  const fingerName = document.getElementById("fingerSelect").value;
  const templateData = document.getElementById("capturedTemplate").value;
  const qualityScore = parseInt(
    document.getElementById("capturedQuality").value
  );

  if (!userId || !fingerName || !templateData) {
    showAlert(
      "Please select a user, finger position, and capture a fingerprint",
      "error"
    );
    return;
  }

  const submitBtn = e.target.querySelector('button[type="submit"]');
  submitBtn.disabled = true;
  submitBtn.innerHTML = '<span class="spinner"></span> Enrolling...';

  try {
    const response = await fetch(`${API_BASE_URL}/fingerprints/enroll`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        userId: parseInt(userId),
        fingerName,
        templateData,
        qualityScore,
      }),
    });

    const data = await response.json();

    if (data.success) {
      showAlert(`${fingerName} enrolled successfully!`, "success");
      document.getElementById("enrollmentForm").reset();
      document.getElementById("fingerprintPreview").innerHTML =
        '<div class="fingerprint-icon">🔒</div><p>No fingerprint captured</p>';
      await loadUsers();
    } else {
      showAlert(data.error || data.message || "Enrollment failed", "error");
    }
  } catch (error) {
    console.error("Enrollment error:", error);
    showAlert("Failed to enroll fingerprint", "error");
  } finally {
    submitBtn.disabled = false;
    submitBtn.innerHTML = "Enroll Fingerprint";
  }
}

/**
 * Capture fingerprint for verification
 */
async function captureFingerprintForVerification() {
  const captureBtn = document.getElementById("verifyCaptureBtn");
  const preview = document.getElementById("verifyPreview");

  if (!app.deviceConnected) {
    showAlert("Device not connected. Please check device connection.", "error");
    return;
  }

  captureBtn.disabled = true;
  captureBtn.innerHTML =
    '<span class="spinner"></span> Place finger on scanner...';
  preview.innerHTML =
    '<div class="fingerprint-icon">👆</div><p>Scanning...</p>';

  try {
    const result = await device.captureFingerprint();

    if (result.success) {
      document.getElementById("verifyTemplate").value = result.templateData;

      preview.innerHTML = `
        <div class="fingerprint-icon">✓</div>
        <p>Fingerprint captured!</p>
        <p style="color: var(--text-secondary); font-size: 0.9rem;">Quality Score: ${result.qualityScore}%</p>
      `;

      showAlert(
        `Fingerprint captured with quality score: ${result.qualityScore}%`,
        "success"
      );
    } else {
      preview.innerHTML =
        '<div class="fingerprint-icon">❌</div><p>Capture failed</p>';
      showAlert(result.error || "Failed to capture fingerprint", "error");
    }
  } catch (error) {
    console.error("Capture error:", error);
    preview.innerHTML =
      '<div class="fingerprint-icon">❌</div><p>Capture failed</p>';
    showAlert("Failed to capture fingerprint", "error");
  } finally {
    captureBtn.disabled = false;
    captureBtn.innerHTML = "📷 Capture Fingerprint";
  }
}

/**
 * Handle fingerprint verification
 */
async function handleFingerprintVerification(e) {
  e.preventDefault();

  const userId = document.getElementById("verifyUserSelect").value;
  const templateData = document.getElementById("verifyTemplate").value;

  if (!userId || !templateData) {
    showAlert("Please select a user and capture a fingerprint", "error");
    return;
  }

  const submitBtn = e.target.querySelector('button[type="submit"]');
  submitBtn.disabled = true;
  submitBtn.innerHTML = '<span class="spinner"></span> Verifying...';

  try {
    const response = await fetch(`${API_BASE_URL}/fingerprints/verify`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        userId: parseInt(userId),
        templateData,
      }),
    });

    const data = await response.json();

    if (data.success && data.data.verified) {
      showAlert(
        `✓ Verified! Welcome ${data.data.userName} (${data.data.matchedFinger})`,
        "success"
      );
      document.getElementById("verificationForm").reset();
      document.getElementById("verifyPreview").innerHTML =
        '<div class="fingerprint-icon">✓</div><p>Verification Successful!</p>';
    } else {
      showAlert("✗ Verification failed - Fingerprint does not match", "error");
      document.getElementById("verifyPreview").innerHTML =
        '<div class="fingerprint-icon">❌</div><p>Verification Failed</p>';
    }
  } catch (error) {
    console.error("Verification error:", error);
    showAlert("Failed to verify fingerprint", "error");
  } finally {
    submitBtn.disabled = false;
    submitBtn.innerHTML = "Verify Fingerprint";
  }
}

/**
 * Escape HTML to prevent XSS
 */
function escapeHtml(text) {
  const div = document.createElement("div");
  div.textContent = text;
  return div.innerHTML;
}
