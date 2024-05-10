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
    /// Invert a color
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
    /// Convert a color to its complementary color
    /// </summary>
    /// <param name="colour"></param>
    public static void ToComplementary(ref this Color colour) {

        float h, s, l;              // Hue, Saturation, Lightness
        ColorToHsl(colour, out h, out s, out l);

        h = (h + 0.5f) % 1;

        colour = HslToColor(h, s, l);
    }

    /// <summary>
    /// Convert a HSL color to a Color
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="l"></param>
    /// <returns></returns>
    private static Color HslToColor(float h, float s, float l) {

        byte r, g, b;

        if (s < epsilon) {

            r = g = b = (byte)(l * 255);
        } else {

            float v1, v2;
            float hue = h;

            v2 = l < 0.5 ? l * (1 + s) : (l + s) - (l * s);
            v1 = 2 * l - v2;

            r = (byte)(255 * HueToRgb(v1, v2, hue + (1.0f / 3)));
            g = (byte)(255 * HueToRgb(v1, v2, hue));
            b = (byte)(255 * HueToRgb(v1, v2, hue - (1.0f / 3)));
        }

        return new Color(r, g, b);
    }

    /// <summary>
    /// Convert a hue to an RGB value
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    private static int HueToRgb(float v1, float v2, float v) {

        if (v < 0) v += 1;
        if (v > 1) v -= 1;

        if ((6 * v) < 1) return (int)(v1 + (v2 - v1) * 6 * v);
        if ((2 * v) < 1) return (int)v2;
        if ((3 * v) < 2) return (int)(v1 + (v2 - v1) * ((2.0f / 3) - v) * 6);

        return (int)v1;
    }

    /// <summary>
    /// Convert a Color to HSL
    /// </summary>
    /// <param name="colour"></param>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="l"></param>
    public static void ColorToHsl(Color colour, out float hue, out float saturation, out float lightness) {

        float r = colour.R / 255f;
        float g = colour.G / 255f;
        float b = colour.B / 255f;

        float max = Math.Max(r, Math.Max(g, b));
        float min = Math.Min(r, Math.Min(g, b));

        hue = (max + min) / 2;
        saturation = (max + min) / 2;
        lightness = (max + min) / 2;


        if (Math.Abs(max - min) < epsilon) { // Check if the absolute difference is within the range

            hue = saturation = 0; // achromatic
        } else {

            float d = max - min;

            saturation = lightness > 0.5 ? d / (2 - max - min) : d / (max + min);

            if (Math.Abs(max - r) < epsilon)
                hue = (g - b) / d + (g < b ? 6 : 0);
            else if (Math.Abs(max - g) < epsilon)
                hue = (b - r) / d + 2;
            else if (Math.Abs(max - b) < epsilon)
                hue = (r - g) / d + 4;

            hue /= 6;
        }
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
