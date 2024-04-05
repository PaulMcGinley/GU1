using Microsoft.Xna.Framework;

namespace GU1.Engine.Interfaces;

public interface IMove {

    /// <summary>
    /// The velocity of the object
    /// </summary>
    public Vector2 Velocity { get; set; }

    /// <summary>
    /// The acceleration of the object
    /// </summary>
    public float Acceleration { get; set; }

    /// <summary>
    /// The friction of the object
    /// </summary>
    public float Friction { get; set; }

    /// <summary>
    /// The maximum speed of the object
    /// </summary>
    public float MaxSpeed { get; set; }

    /// <summary>
    /// The minimum speed of the object
    /// </summary>
    public float MinSpeed { get; set; }

    /// <summary>
    /// Move the object in a direction based on the input
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector2 direction);
}
