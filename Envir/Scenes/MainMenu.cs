using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class MainMenu : IScene {

    Graphic2D background;

    Color menuHotColor = Color.AliceBlue;
    Color menuColdColor = Color.White *0.5f;

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
            SelectedMenuIndex++; //when the player moves down the Slected menu box value increases thus changing box

        if (IsGamePadButtonPressed(0, Buttons.DPadUp))
            SelectedMenuIndex--; //when the player moves up the Slected menu box value decreases thus changing box

        if (IsAnyInputDown(Keys.Enter, Buttons.A, Buttons.Start)) {

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
                        //GameState.CurrentScene = GameScene.HowToPlay;
                        break;

                    case MenuItems.Credits:
                        GameState.CurrentScene = GameScene.Credits;
                        break;

                    case MenuItems.Exit:
                        Environment.Exit(0); // Exit code 0 = success, no errors
                        break;
                }
            }

        #endregion

        #region Keyboard Input

        if (IsKeyPressed(Keys.Down))
            SelectedMenuIndex++; //when the player moves down the Slected menu box value increases thus changing box

        if (IsKeyPressed(Keys.Up))
            SelectedMenuIndex--; //when the player moves up the Slected menu box value decreases thus changing box

        #endregion
    }

    public void FixedUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin( );

        // Draw the background
        background.Draw(spriteBatch);

        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 300, 400, 100), selectedMenuIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 180, 400, 100), selectedMenuIndex == 1 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 60, 400, 100), selectedMenuIndex == 2 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 60, 400, 100), selectedMenuIndex == 3 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 180, 400, 100), selectedMenuIndex == 4 ? menuHotColor : menuColdColor);
        spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 300, 400, 100), selectedMenuIndex == 5 ? menuHotColor : Color.Red*0.5f);

        // Draw the text
        spriteBatch.DrawString(FLib.DebugFont, "PLAY GAME", new Vector2((1920/2)-50, (1080/2)-260), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "PHOTOGRAPHS", new Vector2((1920/2)-50, (1080/2)-140), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "SETTINGS", new Vector2((1920/2)-50, (1080/2)-20), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "HOW TO PLAY", new Vector2((1920/2)-50, (1080/2)+100), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "CREDITS", new Vector2((1920/2)-50, (1080/2)+220), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "EXIT GAME", new Vector2((1920/2)-50, (1080/2)+340), Color.Black);

        // Draw the arrow
        spriteBatch.Draw(TLib.Arrow, new Vector2((1920/2)-250, (1080/2)-275 + (SelectedMenuIndex * 120)), menuHotColor);

        // Draw the arrow flipped on the other side
        spriteBatch.Draw(TLib.Arrow, new Vector2((1920/2)+225, (1080/2)-275 + (SelectedMenuIndex * 120)), null, menuHotColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);

        spriteBatch.End( );
    }

    public void OnSceneStart() { }

    public void OnSceneEnd() { }

    enum MenuItems { //declaring the menu items box values

        Play = 0, //sets the value of the box Play Game to 0
        Photographs = 1, //Sets the value of the Settings Box to 1
        Settings = 2,
        HowToPlay = 3,
        Credits = 4, //Sets the value of the Credits box to 2
        Exit = 5 //sets the value of Exit Game box to 3
    }
}
