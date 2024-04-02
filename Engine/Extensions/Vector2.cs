using Microsoft.Xna.Framework;

namespace GU1.Engine.Extensions;

public static class Vector2Extensions {

    /// <summary>
    /// Convert a Vector2 to a Vector3
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 vector, float z = 0) => new (vector.X, vector.Y, z);

}
