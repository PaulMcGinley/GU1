using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Controls;

public class MonoSlider : Base {

    public Texture2D Background { get; set; }                                                               // Background texture
    public Texture2D Fill { get; set; }                                                                     // Fill texture

    public Texture2D SliderNormal { get; set; }                                                             // Slider normal texture
    public Texture2D SliderOver { get; set; }                                                               // Slider highlight texture
    public Texture2D SliderDown { get; set; }                                                               // Slider pressed texture
    public Vector2 SliderPosition { get; set; }                                                             // Slider position
    public System.Drawing.Size SliderSize { get; set; }                                                     // Slider size
    public Rectangle SliderBounds => new(                                                                   // Slider bounds
        (int)SliderPosition.X,                                                                              // X position
        (int)SliderPosition.Y,                                                                              // Y position
        SliderSize.Width,                                                                                   // Width
        SliderSize.Height);                                                                                 // Height
    public int SliderLeftBound { get; set; }                                                                // Left offset (subtractive) of the slider
    public int SliderRightBound { get; set; }                                                               // Right offset (additive) of the slider
    private bool SliderIsMouseDown { get; set; } = false;                                                   // Is the slider being dragged?

    public event EventHandler ValueChanged;                                                                 // Value changed event

    public int Minimum { get; set; } = 0;                                                                   // Minimum value of the slider
    public int Maximum { get; set; } = 100;                                                                 // Maximum value of the slider

    private int percent { get; set; } = 0;                                                                  // Value of the slider
    public int Value {                                                                                       // Value of the slider


        get { return percent; }                                                                             // Return the value
        set {

            if (value < Minimum) value = Minimum;                                                           // If the value is less than the minimum, set it to the minimum
            if (value > Maximum) value = Maximum;                                                           // If the value is greater than the maximum, set it to the maximum
            percent = value;                                                                                // Set the value
            ValueChanged?.Invoke(this, EventArgs.Empty);                                                    // Invoke the value changed event
        }
    } // End of the Value property

    /// <summary>
    /// Calculate the percentage value of the slider
    /// </summary>
    /// <returns></returns>
    public int CalculateValue() {

        SliderIsMouseDown = true;                                                                           // Set the slider to being dragged

        SliderPosition = new Vector2(                                                                       // Set the slider position
            MousePosition.X - (SliderNormal.Width / 2),                                                     // X position
            SliderPosition.Y);                                                                              // Y position

        if (SliderPosition.X < Position.X - SliderLeftBound - (SliderBounds.Width / 2))                     // If the slider is outside of the left bound
            SliderPosition = new Vector2(                                                                   // Set the slider position
                Position.X - SliderLeftBound - (SliderBounds.Width / 2),                                    // X position
                SliderPosition.Y);                                                                          // Y position

        else if (SliderPosition.X > Position.X + SliderRightBound - (SliderBounds.Width / 2))               // If the slider is outside of the right bound
            SliderPosition = new Vector2(                                                                   // Set the slider position
                Position.X + SliderRightBound - (SliderBounds.Width / 2),                                   // X position
                SliderPosition.Y);                                                                          // Y position

        // Calculate the percentage value of the slider
        return (int)Math.Round((SliderPosition.X - Position.X + SliderLeftBound + (SliderBounds.Width / 2)) / (float)(SliderLeftBound + SliderRightBound) * (Maximum - Minimum) + Minimum);
    } // End of the CalculateValue method

    /// <summary>
    /// Update method
    /// </summary>
    /// <param name="mouseState"></param>
    public override void Update() {

        if (!Visible) return;                                                                               // If the control is not visible, return
        if (!Enabled) return;                                                                               // If the control is not enabled, return

        if ((IsLeftMouseButtonDown && SliderBounds.Contains(MousePosition)) ||                              // If the mouse is down and the slider is being hovered over
            (IsLeftMouseButtonDown && SliderIsMouseDown))                                                   // Or if the mouse is down and the slider is being dragged
            Value = CalculateValue();                                                                       // Calculate the value of the slider
        else if (IsLeftMouseButtonDown &&                                                                   // If mouse in not down
            SliderIsMouseDown)                                                                              // And SliderIsMouseDown is true
            SliderIsMouseDown = false;                                                                      // Set SliderIsMouseDown to false

        base.Update();                                                                                      // Call the base update method
    } // End of the Update method

    /// <summary>
    /// Draw method
    /// </summary>
    /// <param name="spriteBatch"></param>
    public override void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Draw(Background, Position, Color.White);                                                // Draw the background

        double fillWidth = (double)Fill.Width / 100 * Value;                                                // Calculate the fill width

        spriteBatch.Draw(                                                                                   // Draw the fill
            Fill,                                                                                           // Texture
            new Rectangle(                                                                                  // Destination rectangle
                (int)Position.X,                                                                            // X position
                (int)Position.Y,                                                                            // Y position
                (int)fillWidth,                                                                             // Width
                Fill.Height),                                                                               // Height
            new Rectangle(                                                                                  // Source rectangle
                0,                                                                                          // X position
                0,                                                                                          // Y position
                (int)fillWidth,                                                                             // Width
                Fill.Height),                                                                               // Height
            Color.White);                                                                                   // Color

        if (IsMouseDown)                                                                                    // If the mouse is down
            spriteBatch.Draw(SliderDown, SliderPosition, Color.White);                                      // Draw the slider Widget
        else if (SliderBounds.Contains(MousePosition))                                                      // If the mouse is hovering over the slider
            spriteBatch.Draw(SliderOver, SliderPosition, Color.White);                                      // Draw the slider Widget
        else                                                                                                // If the mouse is not hovering over the slider
            spriteBatch.Draw(SliderNormal, SliderPosition, Color.White);                                    // Draw the slider Widget

        base.Draw(spriteBatch);                                                                             // Call the base draw method
    }
}
