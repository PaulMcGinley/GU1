using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Settings : IScene {

    Color menuHotColor = Color.Lime;                                                                       // Selected menu item colour
    Color menuColdColor = Color.White;                                                                      // Unselected menu item colour

    string maxPhotosText => GameState.MaxPhotos == int.MaxValue ? "Unlimited" : GameState.MaxPhotos.ToString(); // The text to display for the max photos

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

        if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight))
            if (SelectedMenuIndex == 2)
                GameState.MaxPhotos++;

        if (IsAnyInputPressed(Keys.Left, Buttons.DPadLeft))
            if (SelectedMenuIndex == 2 && GameState.MaxPhotos > 26)                                         // After the 25th photo, the player can take unlimited photos, so we check for a value greater than 26, any value = unlimited, so when we press left, we want to set the value to the limited range
                GameState.MaxPhotos = 25;                                                                   // Set the max photos to 25
            else if (SelectedMenuIndex == 2 && GameState.MaxPhotos < 26)                                    // If the value is in the limited range then we can decrement it
                GameState.MaxPhotos--;

        // Check for return to main menu
        if (IsAnyInputPressed(Keys.B, Buttons.B, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;
    }

    public void FixedUpdate(GameTime gameTime) { }                                                          // Not Implemented

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        // Draw the background
        spriteBatch.Draw(TLib.mainMenuBackground, Vector2.Zero, Color.White);
        spriteBatch.Draw(TLib.Pixel, new Rectangle(0, 0, 1920, 1080), Color.Black * 0.5f);

        // Name
        spriteBatch.DrawString(FLib.MainMenuFont, "Music Volume", new Vector2((1920 / 2) - 50 - FLib.MainMenuFont.MeasureString("Music Volume").X, 200), SelectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, "SFX Volume", new Vector2((1920 / 2) - 50 - FLib.MainMenuFont.MeasureString("SFX Volume").X, 250), SelectedMenuIndex == 1 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, "Max Photos", new Vector2((1920 / 2) - 50 - FLib.MainMenuFont.MeasureString("Max Photos").X, 300), SelectedMenuIndex == 2 ? menuHotColor : menuColdColor);

        // Values
        spriteBatch.DrawString(FLib.MainMenuFont, $"{GameState.MusicVolume * 100:0}%", new Vector2((1920 / 2) + 50, 200), SelectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, $"{GameState.SFXVolume * 100:0}%", new Vector2((1920 / 2) + 50, 250), SelectedMenuIndex == 1 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, $"{maxPhotosText}", new Vector2((1920 / 2) + 50, 300), SelectedMenuIndex == 2 ? menuHotColor : menuColdColor);

        if (GameState.MaxPhotos == int.MaxValue)
            DrawTextCenteredScreen(spriteBatch,FLib.LeaderboardFont, "Please note: Game will not save photos in Unlimited mode.",350, new Vector2(1920, 1080), Color.DarkRed);

        spriteBatch.End();
    }

    public void OnSceneStart() { }                                                                          // Not Implemented

    public void OnSceneEnd() { }                                                                            // Not Implemented

    #endregion

    enum MenuItems {

        MusicVolume = 0,
        SFXVolume = 1,
        MaxPhotos = 2
    }
}
