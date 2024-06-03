using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class MainMenu : IScene {

    Graphic2D background;                                                                                   // Background image

    Color menuHotColor = Color.AliceBlue;                                                                   // Selected menu item colour
    Color menuColdColor = Color.White *0.5f;                                                                // Unselected menu item colour

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

            SLib.Click.Play();                                                                              // Play the click sound
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
        if (IsAnyInputPressed(Keys.Down, Buttons.DPadDown))
            SelectedMenuIndex++;

        if (IsAnyInputPressed(Keys.Up, Buttons.DPadUp))
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

                case MenuItems.Settings:
                    GameState.CurrentScene = GameScene.Settings;
                    break;

                case MenuItems.HowToPlay:
                    GameState.CurrentScene = GameScene.Controls;
                    break;

                case MenuItems.Credits:
                    GameState.CurrentScene = GameScene.Credits;
                    break;

                case MenuItems.Exit:
                    Environment.Exit(0); // Exit code 0 = success, no errors
                    break;
            }
        }
    }

    public void FixedUpdate(GameTime gameTime) { }                                                          // Not Implemented

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin( );

        //Background
        background.Draw(spriteBatch);

        // Menu boxes
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 300, 400, 100), selectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 180, 400, 100), selectedMenuIndex == 1 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 60, 400, 100), selectedMenuIndex == 2 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 60, 400, 100), selectedMenuIndex == 3 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 180, 400, 100), selectedMenuIndex == 4 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 300, 400, 100), selectedMenuIndex == 5 ? menuHotColor : Color.Red*0.5f);

        // Menu text
        spriteBatch.DrawString(FLib.DebugFont, "PLAY GAME", new Vector2((1920/2)-50, (1080/2)-260), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "PHOTOGRAPHS", new Vector2((1920/2)-50, (1080/2)-140), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "SETTINGS", new Vector2((1920/2)-50, (1080/2)-20), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "HOW TO PLAY", new Vector2((1920/2)-50, (1080/2)+100), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "CREDITS", new Vector2((1920/2)-50, (1080/2)+220), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "EXIT GAME", new Vector2((1920/2)-50, (1080/2)+340), Color.Black);

        // Left Nessie
        spriteBatch.Draw(TLib.TheNessie, new Vector2((1920/2)-350, (1080/2)-295 + (SelectedMenuIndex * 120)), menuHotColor);

        // Right Nessie
        spriteBatch.Draw(TLib.TheNessie, new Vector2((1920/2)+225, (1080/2)-295 + (SelectedMenuIndex * 120)), null, menuHotColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);

        spriteBatch.End( );
    }

    public void OnSceneStart() { }                                                                          // Not Implemented

    public void OnSceneEnd() { }                                                                            // Not Implemented

    #endregion


    /// <summary>
    /// Enumerator for the menu items
    /// </summary>
    enum MenuItems {

        Play = 0,
        Photographs = 1,
        Settings = 2,
        HowToPlay = 3,
        Credits = 4,
        Exit = 5
    }
}
