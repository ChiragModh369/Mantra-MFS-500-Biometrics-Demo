/**
 * Fingerprint Model
 * Represents fingerprint data for users
 */

const { DataTypes } = require("sequelize");

module.exports = (sequelize) => {
  const Fingerprint = sequelize.define(
    "Fingerprint",
    {
      id: {
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true,
        allowNull: false,
      },
      user_id: {
        type: DataTypes.INTEGER,
        allowNull: false,
        references: {
          model: "users",
          key: "id",
        },
        onDelete: "CASCADE",
      },
      finger_name: {
        type: DataTypes.STRING(50),
        allowNull: false,
        validate: {
          notEmpty: {
            msg: "Finger name cannot be empty",
          },
          isIn: {
            args: [
              [
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
              ],
            ],
            msg: "Invalid finger position",
          },
        },
      },
      template_data: {
        type: DataTypes.TEXT,
        allowNull: false,
        validate: {
          notEmpty: {
            msg: "Template data cannot be empty",
          },
        },
      },
      quality_score: {
        type: DataTypes.INTEGER,
        allowNull: false,
        validate: {
          min: {
            args: [0],
            msg: "Quality score must be at least 0",
          },
          max: {
            args: [100],
            msg: "Quality score cannot exceed 100",
          },
        },
      },
    },
    {
      tableName: "fingerprints",
      timestamps: true,
      underscored: true,
      createdAt: "created_at",
      updatedAt: "updated_at",
      indexes: [
        {
          unique: true,
          fields: ["user_id", "finger_name"],
          name: "unique_user_finger",
        },
        {
          fields: ["user_id"],
          name: "idx_user_id",
        },
      ],
    }
  );

  // Define associations
  Fingerprint.associate = (models) => {
    Fingerprint.belongsTo(models.User, {
      foreignKey: "user_id",
      as: "user",
    });
  };

  return Fingerprint;
};
