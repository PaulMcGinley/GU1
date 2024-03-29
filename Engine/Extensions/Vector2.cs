using Microsoft.Xna.Framework;

namespace GU1.Engine.Extensions;

public static class Vector2Extensions {

    /// <summary>
    /// Convert a Vector2 to a Vector3 with a Z value of 0
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 vector) => new (vector.X, vector.Y, 0);

    /// <summary>
    /// Convert a Vector2 to a Vector3 with a specified Z value
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 vector, float z) => new (vector.X, vector.Y, z);

}
