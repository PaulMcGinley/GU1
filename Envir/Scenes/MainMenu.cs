using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GU1.Envir.Scenes;

public class MainMenu : IScene {

    Graphic2D background;                                                                                   // Background image

    Color menuHotColor = Color.AliceBlue *0.90f;                                                            // Selected menu item colour
    Color menuColdColor = Color.White *0.5f;                                                                // Unselected menu item colour

    Color textHotColor = Color.Black;                                                                       // Selected menu item text colour
    Color textColdColor = Color.Black*0.7f;                                                                 // Unselected menu item text colour

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


    #region IScene Implementation

    public void Initialize(GraphicsDevice device) {

        background = new Graphic2D(TLib.mainMenuBackground, new Vector2(device.Viewport.Width / 2, device.Viewport.Height / 2));
    }

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        // Check for menu navigation
        if (IsAnyInputPressed(Keys.Down, Buttons.DPadDown, Buttons.LeftThumbstickDown))
            SelectedMenuIndex++;

        if (IsAnyInputPressed(Keys.Up, Buttons.DPadUp, Buttons.LeftThumbstickUp))
            SelectedMenuIndex--;

        // Check for menu selection
        if (IsAnyInputPressed(Keys.Enter, Buttons.A, Buttons.Start)) {

            switch ((MenuItems)SelectedMenuIndex) {

                case MenuItems.Play:
                    GameState.CurrentScene = GameScene.Lobby;
                    break;

                case MenuItems.Photographs:
                    GameState.CurrentScene = GameScene.PhotoBook;
                    break;

                case MenuItems.Achievements:
                    GameState.CurrentScene = GameScene.Achievements;
                    break;

                case MenuItems.Settings:
                    Settings.returnScene = GameScene.MainMenu;
                    GameState.CurrentScene = GameScene.Settings;
                    break;

                case MenuItems.HowToPlay:
                    GameState.CurrentScene = GameScene.Controls;
                    break;

                case MenuItems.Credits:
                    GameState.CurrentScene = GameScene.Credits;
                    break;

                case MenuItems.Exit:
                    GameState.SaveAchievements();
                    Environment.Exit(0); // Exit code 0 = success, no errors
                    break;
            }
        }
    }

    public void FixedUpdate(GameTime gameTime) { }                                                          // Not Implemented

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin( );

        int yOffset = 110;

        //Background
        background.Draw(spriteBatch);

        // Menu boxes
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 300 -yOffset, 400, 100), selectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 180 -yOffset, 400, 100), selectedMenuIndex == 1 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 60 -yOffset, 400, 100), selectedMenuIndex == 2 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 60 -yOffset, 400, 100), selectedMenuIndex == 3 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 180 -yOffset, 400, 100), selectedMenuIndex == 4 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 300 -yOffset, 400, 100), selectedMenuIndex == 5 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 420 -yOffset, 400, 100), selectedMenuIndex == 6 ? menuHotColor : Color.Red*0.5f);

        // Menu text
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Play", (1080/2)-265 -yOffset, new Vector2(1920, 1080), selectedMenuIndex == 0 ? textHotColor : textColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Gallery", (1080/2)-145 -yOffset, new Vector2(1920, 1080), selectedMenuIndex == 1 ? textHotColor : textColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Achievements", (1080/2)-25 -yOffset, new Vector2(1920, 1080), selectedMenuIndex == 2 ? textHotColor : textColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Settings", (1080/2)+95 -yOffset, new Vector2(1920, 1080), selectedMenuIndex == 3 ? textHotColor : textColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Instructions", (1080/2)+215 -yOffset, new Vector2(1920, 1080), selectedMenuIndex == 4 ? textHotColor : textColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Credits", (1080/2)+335 -yOffset, new Vector2(1920, 1080), selectedMenuIndex == 5 ? textHotColor : textColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Exit", (1080/2)+455 -yOffset, new Vector2(1920, 1080), selectedMenuIndex == 6 ? textHotColor : textColdColor);

        // Left Nessie
        spriteBatch.Draw(TLib.TheNessie, new Vector2((1920/2)-350, (1080/2)-295 -yOffset + (SelectedMenuIndex * 120)), menuHotColor);

        // Right Nessie
        spriteBatch.Draw(TLib.TheNessie, new Vector2((1920/2)+225, (1080/2)-295 -yOffset + (SelectedMenuIndex * 120)), null, menuHotColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);

        spriteBatch.End( );
    }

    public void OnSceneStart() {

        if (Settings.returnScene == GameScene.MainMenu)                                                     // If the return scene is the main menu
            return;

        // Play the menu music
        MediaPlayer.IsRepeating = true;                                                                     // Set the menu music to repeat
        MediaPlayer.Play(SLib.MenuMusic);                                                                   // Play the menu music
    }

    public void OnSceneEnd() {

        Settings.returnScene = GameScene.MainMenu;                                                          // Set the return scene to the main menu
    }

    #endregion


    /// <summary>
    /// Enumerator for the menu items
    /// </summary>
    enum MenuItems {

        Play = 0,
        Photographs = 1,
        Achievements = 2,
        Settings = 3,
        HowToPlay = 4,
        Credits = 5,
        Exit = 6
    }
}
