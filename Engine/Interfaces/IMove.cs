using System;
using System.Numerics;

namespace GU1.Engine.Interfaces;

public interface IMove {

    public Vector2 Velocity { get; set; }
    public float Acceleration { get; set; }
    public float Friction { get; set; }
    public float MaxSpeed { get; set; }
    public float MinSpeed { get; set; }
    public float RotationSpeed { get; set; }
    public float Rotation { get; set; }
    public float RotationAcceleration { get; set; }
    public float RotationFriction { get; set; }
    public float RotationMaxSpeed { get; set; }
    public float RotationMinSpeed { get; set; }
    public float RotationDirection { get; set; }
    public float RotationTarget { get; set; }
    public float RotationTargetSpeed { get; set; }

    public void Move(Vector2 direction);
}
