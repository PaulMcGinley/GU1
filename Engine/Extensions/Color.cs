using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

namespace GU1.Engine.Extensions;

public static class ColorExtensions
{
    /// <summary>
    /// Lighten a color by a given factor
    /// </summary>
    /// <param name="color"></param>
    /// <param name="factor"></param>
    /// <returns></returns>
    public static Color Lighten(this Color color, float factor) {

        // Clamp factor to ensure it's not negative
        factor = Math.Max(factor, 0);

        // Multiply each component by the factor
        int r = (int)Math.Min((1 + color.R) * factor, 255);
        int g = (int)Math.Min((1 + color.G) * factor, 255);
        int b = (int)Math.Min((1 + color.B) * factor, 255);

        byte a = color.A;

        return new Color(r, g, b, a);
    }

    /// <summary>
    /// Convert a hexadecimal string to a Color
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static Color FromHex(string hex) {

        hex = hex.Replace("#", "");
        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);
        byte a = hex.Length == 8 ? Convert.ToByte(hex.Substring(6, 2), 16) : (byte)255;

        return new Color(r, g, b, a);
    }

    /// <summary>
    /// Convert a Color to a hexadecimal string
    /// </summary>
    /// <param name="color"></param>
    /// <param name="includeAlpha"></param>
    /// <returns></returns>
    public static string ToHex(this Color color, bool includeAlpha = false) => includeAlpha ? $"#{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}" : $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    
}
