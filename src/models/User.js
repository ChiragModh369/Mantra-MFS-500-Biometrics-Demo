/**
 * User Model
 * Represents users in the biometric system
 */

const { DataTypes } = require("sequelize");

module.exports = (sequelize) => {
  const User = sequelize.define(
    "User",
    {
      id: {
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true,
        allowNull: false,
      },
      name: {
        type: DataTypes.STRING(255),
        allowNull: false,
        validate: {
          notEmpty: {
            msg: "Name cannot be empty",
          },
          len: {
            args: [2, 255],
            msg: "Name must be between 2 and 255 characters",
          },
        },
      },
      email: {
        type: DataTypes.STRING(255),
        allowNull: false,
        unique: {
          msg: "Email address already exists",
        },
        validate: {
          isEmail: {
            msg: "Must be a valid email address",
          },
        },
      },
    },
    {
      tableName: "users",
      timestamps: true,
      underscored: true,
      createdAt: "created_at",
      updatedAt: "updated_at",
    }
  );

  // Define associations
  User.associate = (models) => {
    User.hasMany(models.Fingerprint, {
      foreignKey: "user_id",
      as: "fingerprints",
      onDelete: "CASCADE",
    });
  };

  return User;
};
