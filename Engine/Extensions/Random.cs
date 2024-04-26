namespace GU1.Engine.Extensions;

public static class RandomExtensions {

    #region Float

    /// <summary>
    /// Returns a random float (0.0 to 1.0)
    /// </summary>
    /// <param name="random"></param>
    /// <returns></returns>
    public static float Float(this System.Random random) => (float)random.NextDouble();

    /// <summary>
    /// Returns a random float (0.0 to max)
    /// </summary>
    /// <param name="random"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float Float(this System.Random random, float max) => (float)random.NextDouble() * max;

    /// <summary>
    /// Returns a random float (min to max)
    /// </summary>
    /// <param name="random"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float Float(this System.Random random, float min, float max) => min + (float)random.NextDouble() * (max - min);

    /// <summary>
    /// Returns a random float (min to max) with a specified decimal precision
    /// </summary>
    /// <param name="random"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="precision">Decimal places</param>
    /// <returns></returns>
    public static float Float(this System.Random random, float min, float max, int precision) => (float)System.Math.Round(min + (float)random.NextDouble() * (max - min), precision);

    #endregion


    #region Int

    /// <summary>
    /// Returns a non-negative random integer (0 to 2,147,483,647)
    /// </summary>
    /// <param name="random"></param>
    /// <returns></returns>
    public static int Int(this System.Random random) => random.Next();

    /// <summary>
    /// Returns a non-negative random integer (0 to max)
    /// </summary>
    /// <param name="random"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Int(this System.Random random, int max) => random.Next(max);

    /// <summary>
    /// Returns a random integer (min to max)
    /// </summary>
    /// <param name="random"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Int(this System.Random random, int min, int max) => random.Next(min, max);

    #endregion


    #region Bool

    /// <summary>
    /// Returns a random boolean (true or false)
    /// </summary>
    /// <param name="random"></param>
    /// <returns></returns>
    public static bool Bool(this System.Random random) => random.Next(2) == 0;

    #endregion


    #region Vector2

    /// <summary>
    /// Returns a random Vector2 (0.0 to 1.0)
    /// </summary>
    /// <param name="random"></param>
    /// <returns></returns>
    public static Microsoft.Xna.Framework.Vector2 RandomVector2(this System.Random random) => new(random.Int(), random.Int());

    /// <summary>
    /// Returns a random Vector2 (0.0 to max)
    /// </summary>
    /// <param name="random"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Microsoft.Xna.Framework.Vector2 RandomVector2(this System.Random random, float max) => new(random.Float(max), random.Float(max));

    /// <summary>
    /// Returns a random Vector2
    /// </summary>
    /// <param name="random"></param>
    /// <param name="minX"></param>
    /// <param name="maxX"></param>
    /// <param name="minY"></param>
    /// <param name="maxY"></param>
    /// <returns></returns>
    public static Microsoft.Xna.Framework.Vector2 RandomVector2(this System.Random random, float minX, float maxX, float minY, float maxY) => new(random.Float(minX, maxX), random.Float(minY, maxY));

    #endregion


    #region Enum

    /// <summary>
    /// Returns a random enum value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="random"></param>
    /// <returns></returns>
    public static T Enumerator<T>(this System.Random random) where T : System.Enum => (T)System.Enum.GetValues(typeof(T)).GetValue(random.Next(System.Enum.GetValues(typeof(T)).Length));

    /// <summary>
    /// Returns a random enum value excluding a specified value
    /// Use this to exclude entries such as None's or Default's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="random"></param>
    /// <param name="exclude"></param>
    /// <returns></returns>
    public static T Enumerator<T>(this System.Random random, T exclude) where T : System.Enum {

        T value;

        do {
            value = (T)System.Enum.GetValues(typeof(T)).GetValue(random.Next(System.Enum.GetValues(typeof(T)).Length));
        } while (value.Equals(exclude));

        return value;
    }

    public static T RandomEnum<T>(this System.Random random, params T[] exclude) where T : System.Enum {

        T value;

        do {
            value = (T)System.Enum.GetValues(typeof(T)).GetValue(random.Next(System.Enum.GetValues(typeof(T)).Length));
        } while (System.Array.Exists(exclude, e => e.Equals(value)));

        return value;
    }

    #endregion


    #region Color

    /// <summary>
    /// Returns a random color
    /// </summary>
    /// <param name="random"></param>
    /// <returns></returns>
    public static Microsoft.Xna.Framework.Color RandomColor(this System.Random random) => new(random.Next(256), random.Next(256), random.Next(256));

    #endregion
}
