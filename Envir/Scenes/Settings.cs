using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Settings : IScene {

    Color menuHotColor = Color.Lime;                                                                       // Selected menu item colour
    Color menuColdColor = Color.White;                                                                      // Unselected menu item colour

    public static GameScene returnScene;                                                                                  // The scene to return to
    public void SetReturnScene(GameScene scene) => returnScene = scene;                         // Set the return scene

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

        // Check for Y button press, reset the settings to default
        if (IsAnyInputPressed(Keys.Y, Buttons.Y))
            SetDefaultValues();

        // Check for menu navigation
        if (IsAnyInputPressed(Keys.Down, Buttons.DPadDown, Buttons.LeftThumbstickDown, Buttons.RightThumbstickDown))
            SelectedMenuIndex++;

        if (IsAnyInputPressed(Keys.Up, Buttons.DPadUp, Buttons.LeftThumbstickUp, Buttons.RightThumbstickUp))
            SelectedMenuIndex--;

        // Check for value changes
        if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight, Buttons.LeftThumbstickRight, Buttons.RightThumbstickRight))
            if (SelectedMenuIndex == 0)
                GameState.MusicVolume += 0.05f;
            else if (SelectedMenuIndex == 1)
                GameState.SFXVolume += 0.05f;

        if (IsAnyInputPressed(Keys.Left, Buttons.DPadLeft, Buttons.LeftThumbstickLeft, Buttons.RightThumbstickLeft))
            if (SelectedMenuIndex == 0)
                GameState.MusicVolume -= 0.05f;
            else if (SelectedMenuIndex == 1)
                GameState.SFXVolume -= 0.05f;

        if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight, Buttons.LeftThumbstickRight, Buttons.RightThumbstickRight))
            if (SelectedMenuIndex == 2 && GameState.MaxPhotos < 26)                                         // If the value is in the limited range then we can increment it
                GameState.MaxPhotos++;
            else if (SelectedMenuIndex == 2 && GameState.MaxPhotos == int.MaxValue)                         // If the value is unlimited, then we can increment it
                GameState.MaxPhotos = 1;

        if (IsAnyInputPressed(Keys.Left, Buttons.DPadLeft, Buttons.LeftThumbstickLeft, Buttons.RightThumbstickLeft))
            if (SelectedMenuIndex == 2 && GameState.MaxPhotos > 26)                                         // After the 25th photo, the player can take unlimited photos, so we check for a value greater than 26, any value = unlimited, so when we press left, we want to set the value to the limited range
                GameState.MaxPhotos = 25;                                                                   // Set the max photos to 25
            else if (SelectedMenuIndex == 2 && GameState.MaxPhotos < 26)                                    // If the value is in the limited range then we can decrement it
                GameState.MaxPhotos--;

        if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight, Buttons.LeftThumbstickRight, Buttons.RightThumbstickRight))
            if (SelectedMenuIndex == 3)
                GameState.ControllerSensitivity++;

        if (IsAnyInputPressed(Keys.Left, Buttons.DPadLeft, Buttons.LeftThumbstickLeft, Buttons.RightThumbstickLeft))
            if (SelectedMenuIndex == 3)
                GameState.ControllerSensitivity--;

        if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight, Buttons.LeftThumbstickRight, Buttons.RightThumbstickRight) || IsAnyInputPressed(Keys.Left, Buttons.DPadLeft, Buttons.LeftThumbstickLeft, Buttons.RightThumbstickLeft))
            if (SelectedMenuIndex == 4)
                GameState.IsFullScreen = !GameState.IsFullScreen;

        if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight, Buttons.LeftThumbstickRight, Buttons.RightThumbstickRight) || IsAnyInputPressed(Keys.Left, Buttons.DPadLeft, Buttons.LeftThumbstickLeft, Buttons.RightThumbstickLeft))
            if (SelectedMenuIndex == 5)
                GameState.EnableSubmersionEffect = !GameState.EnableSubmersionEffect;

        // if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight, Buttons.LeftThumbstickRight, Buttons.RightThumbstickRight) || IsAnyInputPressed(Keys.Left, Buttons.DPadLeft, Buttons.LeftThumbstickLeft, Buttons.RightThumbstickLeft))
        //     if (SelectedMenuIndex == 6)
        //         GameState.AllowShapeShift = !GameState.AllowShapeShift;


        // Check for return to main menu
        if (IsAnyInputPressed(Keys.B, Buttons.B, Buttons.Back))
            GameState.CurrentScene = returnScene;
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
        spriteBatch.DrawString(FLib.MainMenuFont, "Controller Sensitivity", new Vector2((1920 / 2) - 50 - FLib.MainMenuFont.MeasureString("Controller Sensitivity").X, 350), SelectedMenuIndex == 3 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, "Full Screen", new Vector2((1920 / 2) - 50 - FLib.MainMenuFont.MeasureString("Full Screen").X, 400), SelectedMenuIndex == 4 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, "Enable Submersion Effect", new Vector2((1920 / 2) - 50 - FLib.MainMenuFont.MeasureString("Enable Submersion Effect").X, 450), SelectedMenuIndex == 5 ? menuHotColor : menuColdColor);
        // if (GameState.Achievements.Achievement_16_LargestParty.isAchieved)
        //     spriteBatch.DrawString(FLib.MainMenuFont, "Allow Shape Shift", new Vector2((1920 / 2) - 50 - FLib.MainMenuFont.MeasureString("Allow Shape Shift").X, 500), SelectedMenuIndex == 6 ? menuHotColor : menuColdColor);

        // Values
        spriteBatch.DrawString(FLib.MainMenuFont, $"{GameState.MusicVolume * 100:0}%", new Vector2((1920 / 2) + 50, 200), SelectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, $"{GameState.SFXVolume * 100:0}%", new Vector2((1920 / 2) + 50, 250), SelectedMenuIndex == 1 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, $"{maxPhotosText}", new Vector2((1920 / 2) + 50, 300), SelectedMenuIndex == 2 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, $"{GameState.ControllerSensitivity:0}", new Vector2((1920 / 2) + 50, 350), SelectedMenuIndex == 3 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, $"{GameState.IsFullScreen}", new Vector2((1920 / 2) + 50, 400), SelectedMenuIndex == 4 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.MainMenuFont, $"{GameState.EnableSubmersionEffect}", new Vector2((1920 / 2) + 50, 450), SelectedMenuIndex == 5 ? menuHotColor : menuColdColor);
        // if (GameState.Achievements.Achievement_16_LargestParty.isAchieved)
        //     spriteBatch.DrawString(FLib.MainMenuFont, $"{GameState.AllowShapeShift}", new Vector2((1920 / 2) + 50, 500), SelectedMenuIndex == 6 ? menuHotColor : menuColdColor);

        if (GameState.MaxPhotos == int.MaxValue)
            DrawTextCenteredScreen(spriteBatch,FLib.LeaderboardFont, "Please note: Game will not save photos in Unlimited mode.",1080-50, new Vector2(1920, 1080), Color.Yellow);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        GameState.LoadSettings();                                                                           // Load the settings
    }

    public void OnSceneEnd() {

        GameState.SaveSettings();                                                                           // Save the settings
    }

    #endregion

    public static void SetDefaultValues() {

        GameState.MusicVolume = 1f;
        GameState.SFXVolume = 1f;
        GameState.MaxPhotos = 5;
        GameState.ControllerSensitivity = 5;
        GameState.IsFullScreen = false;
        GameState.EnableSubmersionEffect = false;
    }

    enum MenuItems {

        MusicVolume = 0,
        SFXVolume = 1,
        MaxPhotos = 2,
        ControllerSensitivity = 3,
        FullScreen = 4,
        EnableSubmersionEffect = 5,
        //AllowShapeShift = 6
    }
}
