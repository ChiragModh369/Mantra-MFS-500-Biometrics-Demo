/**
 * Database initialization script
 * Creates database and tables if they don't exist
 */

const mysql = require("mysql2/promise");
const fs = require("fs").promises;
const path = require("path");
require("dotenv").config();

const initDatabase = async () => {
  let connection;

  try {
    // Connect without database to create it if needed
    connection = await mysql.createConnection({
      host: process.env.DB_HOST || "localhost",
      port: parseInt(process.env.DB_PORT) || 3306,
      user: process.env.DB_USER || "root",
      password: process.env.DB_PASSWORD || "",
    });

    console.log("Connected to MySQL server");

    // Create database if not exists
    const dbName = process.env.DB_NAME || "biometric_demo";
    await connection.query(`CREATE DATABASE IF NOT EXISTS ${dbName}`);
    console.log(`✓ Database '${dbName}' ready`);

    // Use the database
    await connection.query(`USE ${dbName}`);

    // Read and execute schema file
    const schemaPath = path.join(__dirname, "schema.sql");
    const schema = await fs.readFile(schemaPath, "utf8");

    // Split by semicolon and execute each statement
    const statements = schema
      .split(";")
      .map((stmt) => stmt.trim())
      .filter((stmt) => stmt.length > 0);

    for (const statement of statements) {
      await connection.query(statement);
    }

    console.log("✓ Database tables created successfully");
    console.log("\n=================================");
    console.log("Database initialization complete!");
    console.log("=================================\n");
  } catch (error) {
    console.error("Database initialization failed:", error.message);
    process.exit(1);
  } finally {
    if (connection) {
      await connection.end();
    }
  }
};

// Run initialization
initDatabase();
