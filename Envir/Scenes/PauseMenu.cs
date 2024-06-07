using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class PauseMenu : IScene {

    Color menuHotColor = Color.AliceBlue *0.90f;                                                                   // Selected menu item colour
    Color menuColdColor = Color.White *0.5f;                                                                // Unselected menu item colour

    Color textHotColor = Color.Black;                                                                       // Selected menu item text colour
    Color textColdColor = Color.Black*0.7f;                                                                      // Unselected menu item text colour

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

    public void Initialize(GraphicsDevice device) {}

    public void LoadContent(ContentManager content) {}

    public void UnloadContent() {}

    public void Update(GameTime gameTime) {

        // Check for menu navigation
        if (IsAnyInputPressed(Keys.Down, Buttons.DPadDown))
            SelectedMenuIndex++;

        if (IsAnyInputPressed(Keys.Up, Buttons.DPadUp))
            SelectedMenuIndex--;

        // Check for B button press, return to the game
        if (IsAnyInputPressed(Keys.B, Buttons.B)) {

            GameState.CurrentScene = GameScene.Playing;
            OnSceneEnd(); // Call here as the jumping around scenes doesn't trigger the OnSceneEnd event
        }

        // Check for menu selection
        if (IsAnyInputPressed(Keys.Enter, Buttons.A, Buttons.Start)) {

            switch ((MenuItems)SelectedMenuIndex) {

                case MenuItems.Resume:
                    GameState.CurrentScene = GameScene.Playing;
                    break;

                case MenuItems.Settings:
                    Settings.returnScene = GameScene.PauseMenu;
                    GameState.CurrentScene = GameScene.Settings;
                    break;

                case MenuItems.Exit:
                    GameState.CurrentScene = GameScene.MainMenu;
                    GameState.Players.Clear();
                    break;
            }
        }
    }

    public void FixedUpdate(GameTime gameTime) {}

    public void Draw(SpriteBatch spriteBatch) {

        if (GameState.CurrentScene != GameScene.PauseMenu)
            return;

        spriteBatch.Begin();

        // Menu boxes
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 300, 400, 100), selectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 180, 400, 100), selectedMenuIndex == 1 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 60, 400, 100), selectedMenuIndex == 2 ? menuHotColor : menuColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Resume", (1080/2)-265, new Vector2(1920, 1080), selectedMenuIndex == 0 ? textHotColor : textColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Settings", (1080/2)-145, new Vector2(1920, 1080), selectedMenuIndex == 1 ? textHotColor : textColdColor);
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Exit", (1080/2)-25, new Vector2(1920, 1080), selectedMenuIndex == 2 ? textHotColor : textColdColor);

        // Left Nessie
        spriteBatch.Draw(TLib.TheNessie, new Vector2((1920/2)-350, (1080/2)-295 + (SelectedMenuIndex * 120)), menuHotColor);

        // Right Nessie
        spriteBatch.Draw(TLib.TheNessie, new Vector2((1920/2)+225, (1080/2)-295 + (SelectedMenuIndex * 120)), null, menuHotColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);

        spriteBatch.End();
    }

    public void OnSceneStart() { }

    public void OnSceneEnd() {

        SelectedMenuIndex = 0;
    }

    #endregion

    enum MenuItems {

        Resume = 0,
        Settings = 1,
        Exit = 2,
    }

}
