using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Controls;

public class CheckBox : Base {

    public Texture2D Normal { get; set; }                                                                   // Normal texture
    public Texture2D Over { get; set; }                                                                     // Highlight texture
    public Texture2D Pressed { get; set; }                                                                  // Pressed texture
    public Texture2D Checked { get; set; }                                                                  // Checked texture
    public Texture2D CheckedOver { get; set; }                                                              // Checked highlight texture
    public Texture2D CheckedPressed { get; set; }                                                           // Checked pressed texture

    public CheckBox() {

        MouseClick += MonoCheckBox_MouseClick;                                                              // Subscribe to the MouseClick event
    }

    public override void Update() {

        base.Update();                                                                                      // Call the base Update method
    }

    public override void Draw(SpriteBatch spriteBatch) {

        if (!Visible) return;                                                                               // If the control is not visible, return

        if (SelectedState) {                                                                                // If the control is checked

            if (IsMouseDown)                                                                                // If the mouse is down
                spriteBatch.Draw(CheckedPressed, Position, Color.White);                                    // Draw the pressed texture
            else if (IsMouseOver)                                                                           // If the mouse is over
                spriteBatch.Draw(CheckedOver, Position, Color.White);                                       // Draw the highlight texture
            else                                                                                            // If the mouse is not over
                spriteBatch.Draw(Checked, Position, Color.White);                                           // Draw the normal texture

        } else {                                                                                            // If the control is not checked

            if (IsMouseDown)                                                                                // If the mouse is down
                spriteBatch.Draw(Pressed, Position, Color.White);                                           // Draw the pressed texture
            else if (IsMouseOver)                                                                           // If the mouse is over
                spriteBatch.Draw(Over, Position, Color.White);                                              // Draw the highlight texture
            else                                                                                            // If the mouse is not over
                spriteBatch.Draw(Normal, Position, Color.White);                                            // Draw the normal texture
        }

        base.Draw(spriteBatch);                                                                             // Call the base Draw method
    }

    #region Events

    /// <summary>
    /// MouseClick event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MonoCheckBox_MouseClick(object sender, EventArgs e) {

        if (!Visible) return;                                                                               // If the control is not visible, return

        SelectedState = !SelectedState;                                                                     // Call the CheckedStateChanged event
    }

    #endregion

}
