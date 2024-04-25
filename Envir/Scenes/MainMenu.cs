﻿using System;
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

        if (IsGamePadButtonPressed(0, Microsoft.Xna.Framework.Input.Buttons.DPadDown))
            SelectedMenuIndex++;

        if (IsGamePadButtonPressed(0, Microsoft.Xna.Framework.Input.Buttons.DPadUp))
            SelectedMenuIndex--;

        if (IsAnyInputDown(Keys.Enter, Buttons.A, Buttons.Start)) {

                switch ((MenuItems)SelectedMenuIndex) {

                    case MenuItems.Play:
                        GameState.CurrentScene = GameScene.Playing;
                        break;

                    case MenuItems.Settings:
                        // TODO: ...
                        break;

                    case MenuItems.Credits:
                        // TODO: ...
                        break;

                    case MenuItems.Exit:
                        Environment.Exit(0);
                        break;
                }
            }

        #endregion

        #region Keyboard Input

        if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            SelectedMenuIndex++;

        if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            SelectedMenuIndex--;

        #endregion
    }

    public void FixedUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin( );
        background.Draw(spriteBatch);

        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920/2)-200, (1080/2)-300, 400, 100), Color.White);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920/2)-200, (1080/2)-180, 400, 100), Color.White);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920/2)-200, (1080/2)-60, 400, 100), Color.White);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920/2)-200, (1080/2)+60, 400, 100), Color.White);

        spriteBatch.Draw(TLib.Arrow, new Vector2((1920/2)-250, (1080/2)-300 + (SelectedMenuIndex * 120)), Color.White);

        spriteBatch.DrawString(FLib.DebugFont, "Play", new Vector2((1920/2)-200, (1080/2)-300), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "Settings", new Vector2((1920/2)-200, (1080/2)-180), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "Credits", new Vector2((1920/2)-200, (1080/2)-60), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "Exit", new Vector2((1920/2)-200, (1080/2)+60), Color.Black);

        spriteBatch.End( );
    }

    enum MenuItems : int {

        Play = 0,
        Settings = 1,
        Credits = 2,
        Exit = 3
    }
}
