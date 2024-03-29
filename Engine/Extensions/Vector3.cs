using Microsoft.Xna.Framework;

namespace GU1.Engine.Extensions;

public static class Vector3Extensions {

    /// <summary>
    /// Convert a Vector3 to a Vector2
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector2 ToVector2(this Vector3 vector) => new (vector.X, vector.Y);

}
