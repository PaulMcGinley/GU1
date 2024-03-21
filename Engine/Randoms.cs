using System;
using Microsoft.Xna.Framework;

namespace GU1.Engine;

public static class Randoms {
    
    private static readonly Random random = new();

    #region  Integer

    /// <summary>
    /// Returns a random integer between 0 and int.MaxValue
    /// </summary>
    /// <returns></returns>
    public static int RandomInteger() => random.Next();

    /// <summary>
    /// Returns a random integer from 0 to max
    /// Note: max is inclusive
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int RandomInteger(int max) => random.Next(max + 1);

    /// <summary>
    /// Returns a random integer from min to max
    /// Note: min and max are inclusive
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int RandomInteger(int min, int max) => random.Next(min, max + 1);

    #endregion

    #region Float
    
    /// <summary>
    /// Returns a random float between 0 and 1
    /// </summary>
    /// <returns></returns>
    public static float RandomFloat() => (float)random.NextDouble();

    /// <summary>
    /// Returns a random float from 0 to max
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float RandomFloat(float max) => (float)random.NextDouble() * max;

    /// <summary>
    /// Returns a random float from min to max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float RandomFloat(float min, float max) => (float)random.NextDouble() * (max - min) + min;

    #endregion

    #region Double

    /// <summary>
    /// Returns a random double between 0 and 1
    /// </summary>
    /// <returns></returns>
    public static double RandomDouble() => random.NextDouble();

    /// <summary>
    /// Returns a random double from 0 to max
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static double RandomDouble(double max) => random.NextDouble() * max;

    /// <summary>
    /// Returns a random double from min to max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static double RandomDouble(double min, double max) => random.NextDouble() * (max - min) + min;

    #endregion

    #region Byte

    /// <summary>
    /// Returns a random byte between 0 and 255
    /// </summary>
    /// <returns></returns>
    public static byte RandomByte() => (byte)random.Next(0, 256);

    /// <summary>
    /// Returns a random byte from 0 to max
    /// Note: max is inclusive
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static byte RandomByte(byte max) => (byte)random.Next(0, max + 1);

    /// <summary>
    /// Returns a random byte from min to max
    /// Note: min and max are inclusive
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static byte RandomByte(byte min, byte max) => (byte)random.Next(min, max + 1);

    #endregion

    #region Miscelaneous

    /// <summary>
    /// Returns a random boolean
    /// </summary>
    /// <returns></returns>
    public static bool RandomBoolean() => random.Next(0, 2) == 0;

    /// <summary>
    /// Returns a random enum value
    /// Note: T must be an enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T RandomEnumerator<T>() where T : Enum => (T)Enum.GetValues(typeof(T)).GetValue(random.Next(Enum.GetValues(typeof(T)).Length));

    #endregion

    #region Color

    /// <summary>
    /// Returns a random color
    /// </summary>
    /// <returns></returns>
    public static Color RandomColor() => new(RandomByte(), RandomByte(), RandomByte());

    /// <summary>
    /// Returns a random vector2
    /// </summary>
    /// <returns></returns>
    public static Vector2 RandomVector2() => new (RandomFloat(), RandomFloat());

    /// <summary>
    /// Returns a random vector2 from 0 to max
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Vector2 RandomVector2(float max) => new (RandomFloat(max), RandomFloat(max));

    /// <summary>
    /// Returns a random vector2 from min to max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Vector2 RandomVector2(float min, float max) => new(RandomFloat(min, max), RandomFloat(min, max));

    #endregion

}
