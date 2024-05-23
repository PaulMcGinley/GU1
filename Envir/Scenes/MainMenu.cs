using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class MainMenu : IScene {

    Graphic2D background;

    int selectedMenuIndex = 0;
    int SelectedMenuIndex {

        get => selectedMenuIndex;
        set {
            if (value < 0)
                selectedMenuIndex = Enum.GetValues(typeof(MenuItems)).Length - 1; // TODO: Put this in a function where it can be reused
            else if (value > Enum.GetValues(typeof(MenuItems)).Length - 1)
                selectedMenuIndex = 0;
            else
                selectedMenuIndex = value;
        }
    }

    public void Initialize(GraphicsDevice device) {

        background = new Graphic2D(TLib.mainMenuBackground, new Vector2(device.Viewport.Width / 2, device.Viewport.Height / 2));
    }

    public void LoadContent(ContentManager content) {

    }

    public void UnloadContent() {

    }

    public void Update(GameTime gameTime) {

        #region Gamepad Input

        if (IsGamePadButtonPressed(0, Buttons.DPadDown))
            SelectedMenuIndex++;

        if (IsGamePadButtonPressed(0, Buttons.DPadUp))
            SelectedMenuIndex--;

        if (IsAnyInputDown(Keys.Enter, Buttons.A, Buttons.Start)) {

                switch ((MenuItems)SelectedMenuIndex) {

                    case MenuItems.Play:
                        GameState.CurrentScene = GameScene.Lobby;
                        break;

                    case MenuItems.Settings:
                        // TODO: ...
                        break;

                    case MenuItems.Credits:
                        // TODO: ...
                        break;

                    case MenuItems.Exit:
                        Environment.Exit(0); // Exit code 0 = success, no errors
                        break;
                }
            }

        #endregion

        #region Keyboard Input

        if (IsKeyPressed(Keys.Down))
            SelectedMenuIndex++;

        if (IsKeyPressed(Keys.Up))
            SelectedMenuIndex--;

        #endregion
    }

    public void FixedUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin( );

        // Draw the background
        background.Draw(spriteBatch);

        // Draw the menu items
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920/2)-200, (1080/2)-300, 400, 100), Color.White);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920/2)-200, (1080/2)-180, 400, 100), Color.White);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920/2)-200, (1080/2)-60, 400, 100), Color.White);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920/2)-200, (1080/2)+60, 400, 100), Color.White);

        // Draw the text
        spriteBatch.DrawString(FLib.DebugFont, "Play", new Vector2((1920/2)-200, (1080/2)-300), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "Settings", new Vector2((1920/2)-200, (1080/2)-180), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "Credits", new Vector2((1920/2)-200, (1080/2)-60), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "Exit", new Vector2((1920/2)-200, (1080/2)+60), Color.Black);

        // Draw the arrow
        spriteBatch.Draw(TLib.Arrow, new Vector2((1920/2)-250, (1080/2)-300 + (SelectedMenuIndex * 120)), Color.White);

        spriteBatch.End( );
    }

    public void OnSceneStart() {

        System.Diagnostics.Debug.WriteLine("MainMenu scene started");
    }

    public void OnSceneEnd() {

        System.Diagnostics.Debug.WriteLine("MainMenu scene ended");
    }

    enum MenuItems {

        Play = 0,
        Settings = 1,
        Credits = 2,
        Exit = 3
    }
}
