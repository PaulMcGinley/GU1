using System.Data.SqlTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class FancyNumber {

    public uint Value = 0;                                                                                  // The number to display in the fancy format
    public float Scale = 0.2f;                                                                              // Set the scale to 20% as TextStudios provides large images

    /// <summary>
    /// Draw the number centered at the specified position
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <param name="position">Central point of the numbers (Similar to an origin point)</param>
    /// <param name="colour"></param>
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color colour = default) {

        string value = Value.ToString();                                                                    // Convert the number to a string to iterate through each digit

        float totalWidth = 0;                                                                               // Calculate the total width of the number

        for (int i = 0; i < value.Length; i++)                                                              // Iterate through each digit and add the width of the image to the total width
            totalWidth += TLib.Numbers[int.Parse(value[i].ToString())].Width;                               // The width of the image is the width of the number at the index of the digit

        float x = position.X - totalWidth * Scale / 2;                                                      // Calculate the starting x position to center the number

        for (int i = 0; i < value.Length; i++) {                                                            // Iterate through each digit

            int num = int.Parse(value[i].ToString());                                                       // Convert the digit to an integer

            spriteBatch.Draw(
                TLib.Numbers[num],                                                                          // Texture
                new Vector2(x, position.Y),                                                                 // Position
                null,                                                                                       // Source Rectangle
                colour,                                                                                     // Colour
                0,                                                                                          // Rotation
                Vector2.Zero,                                                                               // Origin
                Scale,                                                                                      // Scale
                SpriteEffects.None,                                                                         // Effects
                0);                                                                                         // Layer Depth

            x += TLib.Numbers[num].Width * Scale;                                                           // Move the x position to the right by the width of the number
        }
    }

    #region Operator Overloads

    /// <summary>
    ///
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator <(FancyNumber a, int b) => a.Value < b;

    /// <summary>
    ///
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator >(FancyNumber a, int b) => a.Value > b;

    /// <summary>
    ///
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(FancyNumber a, int b) => a.Value == b;

    /// <summary>
    ///
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(FancyNumber a, int b) => a.Value != b;

    #endregion
}
