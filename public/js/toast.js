/**
 * Toast Notification System
 * Professional toast notifications for user feedback
 */

const Toast = {
  container: null,

  init() {
    if (!this.container) {
      this.container = document.createElement("div");
      this.container.id = "toast-container";
      this.container.className = "toast-container";
      document.body.appendChild(this.container);
    }
  },

  show(message, type = "info", duration = 4000) {
    this.init();

    const toast = document.createElement("div");
    toast.className = `toast toast-${type} toast-enter`;

    const icon = this.getIcon(type);
    const progressBar = document.createElement("div");
    progressBar.className = "toast-progress";

    toast.innerHTML = `
      <div class="toast-content">
        <span class="toast-icon">${icon}</span>
        <span class="toast-message">${message}</span>
        <button class="toast-close" onclick="this.parentElement.parentElement.remove()">×</button>
      </div>
    `;

    toast.appendChild(progressBar);
    this.container.appendChild(toast);

    // Trigger enter animation
    setTimeout(() => toast.classList.remove("toast-enter"), 10);

    // Progress bar animation
    progressBar.style.animation = `toast-progress ${duration}ms linear`;

    // Auto dismiss
    const dismissTimeout = setTimeout(() => {
      this.dismiss(toast);
    }, duration);

    // Clear timeout on manual close
    toast.querySelector(".toast-close").addEventListener("click", () => {
      clearTimeout(dismissTimeout);
      this.dismiss(toast);
    });
  },

  dismiss(toast) {
    toast.classList.add("toast-exit");
    setTimeout(() => toast.remove(), 300);
  },

  getIcon(type) {
    const icons = {
      success: "✓",
      error: "✕",
      warning: "⚠",
      info: "ℹ",
    };
    return icons[type] || icons.info;
  },

  success(message, duration) {
    this.show(message, "success", duration);
  },

  error(message, duration) {
    this.show(message, "error", duration);
  },

  warning(message, duration) {
    this.show(message, "warning", duration);
  },

  info(message, duration) {
    this.show(message, "info", duration);
  },
};

// Make available globally
window.toast = Toast;
