using System;

namespace GU1.Engine.Extensions;

public static class RandomExtensions {

    #region Float

    public static float Float(this Random random) => (float)random.NextDouble();
    public static float Float(this Random random, float max) => (float)random.NextDouble() * max;
    public static float Float(this Random random, float min, float max) => min + (float)random.NextDouble() * (max - min);
    public static float Float(this Random random, float min, float max, int precision) => (float)Math.Round(min + (float)random.NextDouble() * (max - min), precision);

    #endregion


    #region Int

    public static int Int(this Random random) => random.Next();
    public static int Int(this Random random, int max) => random.Next(max);
    public static int Int(this Random random, int min, int max) => random.Next(min, max);

    #endregion


    #region Bool

    public static bool Bool(this Random random) => random.Next(2) == 0;

    #endregion


    #region Enum

    public static T Enumerator<T>(this Random random) where T : Enum => (T)Enum.GetValues(typeof(T)).GetValue(random.Next(Enum.GetValues(typeof(T)).Length));
    public static T Enumerator<T>(this Random random, T exclude) where T : Enum {

        T value;

        do {
            value = (T)Enum.GetValues(typeof(T)).GetValue(random.Next(Enum.GetValues(typeof(T)).Length));
        } while (value.Equals(exclude));

        return value;
    }

    public static T RandomEnum<T>(this Random random, params T[] exclude) where T : Enum {

        T value;

        do {
            value = (T)Enum.GetValues(typeof(T)).GetValue(random.Next(Enum.GetValues(typeof(T)).Length));
        } while (Array.Exists(exclude, e => e.Equals(value)));

        return value;
    }

    #endregion


    #region Color

    public static Microsoft.Xna.Framework.Color RandomColor(this Random random) => new(random.Next(256), random.Next(256), random.Next(256));

    #endregion
}
