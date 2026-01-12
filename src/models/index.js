/**
 * Models Index
 * Initializes Sequelize and loads all models
 */

const { Sequelize } = require("sequelize");
const config = require("../config/sequelize.config");

const env = process.env.NODE_ENV || "development";
const dbConfig = config[env];

// Initialize Sequelize
const sequelize = new Sequelize(
  dbConfig.database,
  dbConfig.username,
  dbConfig.password,
  dbConfig
);

// Import models
const User = require("./User")(sequelize);
const Fingerprint = require("./Fingerprint")(sequelize);

// Store models in an object
const models = {
  User,
  Fingerprint,
  sequelize,
  Sequelize,
};

// Run associations
Object.keys(models).forEach((modelName) => {
  if (models[modelName].associate) {
    models[modelName].associate(models);
  }
});

/**
 * Test database connection
 */
const testConnection = async () => {
  try {
    await sequelize.authenticate();
    console.log("✓ Sequelize: Database connection established successfully");
    return true;
  } catch (error) {
    console.error("✗ Sequelize: Unable to connect to database:", error.message);
    return false;
  }
};

/**
 * Sync all models with database
 * @param {Object} options - Sync options
 */
const syncModels = async (options = {}) => {
  try {
    await sequelize.sync(options);
    console.log("✓ Sequelize: All models synchronized successfully");
  } catch (error) {
    console.error("✗ Sequelize: Model synchronization failed:", error.message);
    throw error;
  }
};

module.exports = {
  ...models,
  testConnection,
  syncModels,
};
