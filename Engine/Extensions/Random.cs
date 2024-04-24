using System;

namespace GU1.Engine.Extensions;

public static class RandomExtensions {

    public static float RandomFloat(this Random random) => (float)random.NextDouble();
    public static float RandomFloat(this Random random, float max) => (float)random.NextDouble() * max;
    public static float RandomFloat(this Random random, float min, float max) => min + (float)random.NextDouble() * (max - min);
    public static float RandomFloat(this Random random, float min, float max, int precision) => (float)Math.Round(min + (float)random.NextDouble() * (max - min), precision);
    public static int RandomInt(this Random random) => random.Next();
    public static int RandomInt(this Random random, int max) => random.Next(max);
    public static int RandomInt(this Random random, int min, int max) => random.Next(min, max);
    public static bool RandomBool(this Random random) => random.Next(2) == 0;
    public static T RandomEnum<T>(this Random random) where T : Enum => (T)Enum.GetValues(typeof(T)).GetValue(random.Next(Enum.GetValues(typeof(T)).Length));
    public static T RandomEnum<T>(this Random random, T exclude) where T : Enum {

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
    public static Microsoft.Xna.Framework.Color RandomColor(this Random random) => new(random.Next(256), random.Next(256), random.Next(256));
}
