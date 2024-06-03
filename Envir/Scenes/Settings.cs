using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Settings : IScene {

    Color menuHotColor = Color.Lime;                                                                       // Selected menu item colour
    Color menuColdColor = Color.White;                                                                      // Unselected menu item colour

    int selectedMenuIndex = 0;                                                                              // The index of the selected menu item (variable)
    int SelectedMenuIndex {                                                                                 // The index of the selected menu item (property)

        get => selectedMenuIndex;                                                                           // Get the selected menu index
        set {
            if (value < 0)                                                                                  // If the value is less than 0 (less than the first menu item)
                selectedMenuIndex = Enum.GetValues(typeof(MenuItems)).Length - 1;                           // Set the selected menu index to the last menu item
            else if (value > Enum.GetValues(typeof(MenuItems)).Length - 1)                                  // If the value is greater than the last menu item
                selectedMenuIndex = 0;                                                                      // Set the selected menu index to the first menu item
            else                                                                                            // Otherwise
                selectedMenuIndex = value;                                                                  // Set the selected menu index to the value

            SLib.Click.Play(GameState.SFXVolume, 0, 0);                                                     // Play the click sound
        }
    }

    #region  IScene Implementation

    public void Initialize(GraphicsDevice device) { }                                                       // Not Implemented

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        // Check for menu navigation
        if (IsAnyInputPressed(Keys.Down, Buttons.DPadDown))
            SelectedMenuIndex++;

        if (IsAnyInputPressed(Keys.Up, Buttons.DPadUp))
            SelectedMenuIndex--;

        // Check for value changes
        if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight))
            if (SelectedMenuIndex == 0)
                GameState.MusicVolume += 0.05f;
            else if (SelectedMenuIndex == 1)
                GameState.SFXVolume += 0.05f;

        if (IsAnyInputPressed(Keys.Left, Buttons.DPadLeft))
            if (SelectedMenuIndex == 0)
                GameState.MusicVolume -= 0.05f;
            else if (SelectedMenuIndex == 1)
                GameState.SFXVolume -= 0.05f;

        // Check for return to main menu
        if (IsAnyInputPressed(Keys.B, Buttons.B, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;
    }

    public void FixedUpdate(GameTime gameTime) { }                                                          // Not Implemented

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        // Draw the background
        spriteBatch.Draw(TLib.LobbyBackground, Vector2.Zero, Color.White);
        spriteBatch.Draw(TLib.Pixel, new Rectangle(0, 0, 1920, 1080), Color.Black * 0.5f);

        // Name
        spriteBatch.DrawString(FLib.DebugFont, "Music Volume", new Vector2((1920 / 2) - 50 - FLib.DebugFont.MeasureString("Music Volume").X, 200), SelectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.DebugFont, "SFX Volume", new Vector2((1920 / 2) - 50 - FLib.DebugFont.MeasureString("SFX Volume").X, 250), SelectedMenuIndex == 1 ? menuHotColor : menuColdColor);

        // Values
        spriteBatch.DrawString(FLib.DebugFont, $"{GameState.MusicVolume * 100:0}%", new Vector2((1920 / 2) + 50, 200), SelectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.DebugFont, $"{GameState.SFXVolume * 100:0}%", new Vector2((1920 / 2) + 50, 250), SelectedMenuIndex == 1 ? menuHotColor : menuColdColor);

        spriteBatch.End();
    }

    public void OnSceneStart() { }                                                                          // Not Implemented

    public void OnSceneEnd() { }                                                                            // Not Implemented

    #endregion

    enum MenuItems {

        MusicVolume = 0,
        SFXVolume = 1,
    }
}
