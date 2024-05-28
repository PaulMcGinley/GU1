using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Graphics;

public static partial class Draw {

    /// <summary>
    /// Draw text on screen at a given position
    /// </summary>
    /// <param name="text"></param>
    /// <param name="position"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="font"></param>
    /// <param name="colour"></param>
    public static void DrawText(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 position, Color colour = default) {

        spriteBatch.DrawString(font, text, position, colour);                                               // Draw the text
    }

    /// <summary>
    /// Draw text on screen centered at a given position
    /// </summary>
    /// <param name="text"></param>
    /// <param name="position"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="font"></param>
    /// <param name="colour"></param>
    public static void TextCenteredAtPosition(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 position, Color colour = default) {

        Vector2 size = font.MeasureString(text);                                                            // Get the size of the text
        Vector2 drawPosition = position - size / 2;                                                         // Calculate the position to draw the text

        DrawText(spriteBatch, font, text, drawPosition, colour);                                            // Draw the text
    }

    /// <summary>
    /// Draw text on screen centered at a given y position
    /// </summary>
    /// <param name="text"></param>
    /// <param name="yPosition"></param>
    /// <param name="screenDimensions"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="font"></param>
    /// <param name="colour"></param>
    public static void DrawTextCenteredScreen(SpriteBatch spriteBatch, SpriteFont font, string text, float yPosition, Vector2 screenDimensions, Color colour = default) {

        Vector2 size = font.MeasureString(text);                                                            // Get the size of the text
        Vector2 drawPosition = new(screenDimensions.X / 2 - size.X / 2, yPosition);                         // Calculate the position to draw the text

        DrawText(spriteBatch, font, text, drawPosition, colour);                                            // Draw the text
    }
    public static void DrawTextBottomLeftScreen(SpriteBatch spriteBatch, SpriteFont font, string text, float yPosition, Vector2 screenDimensions, Color colour = default) {

        Vector2 size = font.MeasureString(text);                                                            // Get the size of the text
        Vector2 drawPosition = new(screenDimensions.X / 7 , 10 * yPosition );                               // Calculate the position to draw the text

        DrawText(spriteBatch, font, text, drawPosition, colour);                                            // Draw the text
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <param name="font"></param>
    /// <param name="what"></param>
    /// <param name="who"></param>
    /// <param name="yPosition"></param>
    /// <param name="screenDimensions"></param>
    /// <param name="colour"></param>
    public static void DrawTextCredits(SpriteBatch spriteBatch, SpriteFont font, string what, string who, float yPosition, Vector2 screenDimensions, Color colour = default) {

        float padding = 500;                                                                                // White space at the side of the screen to center the text
        float whatWidth = font.MeasureString(what).X;                                                       // Width of the what text
        float whoWidth = font.MeasureString(who).X;                                                         // Width of the who text

        Vector2 jobPos = new(padding, yPosition);                                                           // Position of the what text
        Vector2 namePos = new(screenDimensions.X - padding - whoWidth, yPosition);                          // Position of the who text

        int lineGap = 20;                                                                                   // Gap between the line and the text
        Rectangle line = new Rectangle(
            (int)(padding + whatWidth + lineGap),                                                           // X
            (int)yPosition + 20,                                                                            // Y
            (int)(screenDimensions.X - whatWidth -  whoWidth - (lineGap*2) - (padding*2)),                  // Width
            1);                                                                                             // Height

        spriteBatch.DrawString(font, what, jobPos, colour);                                                 // Draw the what text
        spriteBatch.DrawString(font, who, namePos, colour);                                                 // Draw the who text

        spriteBatch.Draw(TLib.Pixel, line, Color.White);                                                    // Draw the line


        // Example of how it should look:
        //          What  _________________________________  Who

        //          Programming ______________________  John Doe
        //          Art __________________________  Jane Doenuts
        // Etc...
    }

}
