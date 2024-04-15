using System;
using Microsoft.Xna.Framework;

namespace GU1.Engine.Extensions;

public static class ColorExtensions {

    /// <summary>
    /// Lighten a color by a given factor
    /// </summary>
    /// <param name="colour"></param>
    /// <param name="factor"></param>
    /// <returns></returns>
    public static void Lighten(ref this Color colour, float factor) {

        // Clamp factor to ensure it's not negative
        factor = Math.Max(factor, 0);

        // Multiply each component by the factor
        int r = (int)Math.Min((1 + colour.R) * factor, 255);
        int g = (int)Math.Min((1 + colour.G) * factor, 255);
        int b = (int)Math.Min((1 + colour.B) * factor, 255);
        byte a = colour.A;

        colour = new Color(r, g, b, a);
    }

    /// <summary>
    /// Invert
    /// </summary>
    /// <param name="colour"></param>
    public static void Invert(ref this Color colour) {

        byte r = (byte)(255 - colour.R);
        byte g = (byte)(255 - colour.G);
        byte b = (byte)(255 - colour.B);
        byte a = colour.A;

        colour = new Color(r, g, b, a);
    }

    /// <summary>
    /// Convert a hexadecimal string to a Color
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static void FromHex(ref this Color colour, string hex) {

        hex = hex.Replace("#", "");

        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);
        byte a = hex.Length == 8 ? Convert.ToByte(hex.Substring(6, 2), 16) : (byte)255;

        colour = new Color(r, g, b, a);
    }

    /// <summary>
    /// Convert a Color to a hexadecimal string
    /// </summary>
    /// <param name="color"></param>
    /// <param name="includeAlpha"></param>
    /// <returns></returns>
    public static string ToHex(this Color colour, bool includeAlpha = false) => includeAlpha ? $"#{colour.R:X2}{colour.G:X2}{colour.B:X2}{colour.A:X2}" : $"#{colour.R:X2}{colour.G:X2}{colour.B:X2}";

}
