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
            SelectedMenuIndex++; //when the player moves down the Slected menu box value increases thus changing box

        if (IsGamePadButtonPressed(0, Buttons.DPadUp))
            SelectedMenuIndex--; //when the player moves up the Slected menu box value decreases thus changing box

        if (IsAnyInputDown(Keys.Enter, Buttons.A, Buttons.Start)) {

                switch ((MenuItems)SelectedMenuIndex) {

                    case MenuItems.Play:
                        GameState.CurrentScene = GameScene.Lobby;
                        break;

                    case MenuItems.Controls:
                        GameState.CurrentScene = GameScene.PhotoBook;
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

        // @AlexanderTuffy good work ^_^ , however if you wish to optimize / minify your code, you should look at the ternary operator ?:
        // That way you can do an inline if statement for each box, and only draw the box that is selected
        // Here is an example of how you could do it:
        // spriteBatch.Draw(TLib.Pixel, new Rectangle(x, y, w, h), selectedMenuIndex == 0 ? Color.Gray : Color.White);
        // If you choose to do so, you couuld create variables for the colours =]
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
        // Draw the menu items
        //making the colour change to gray when each indivisual box is selected
        if (selectedMenuIndex == 0) //when the first box is selected
        {
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 300, 400, 100), Color.Gray);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 180, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 60, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 60, 400, 100), Color.White);
        }
        else if (selectedMenuIndex == 1) //when the second box is selected
        {
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 300, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 180, 400, 100), Color.Gray);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 60, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 60, 400, 100), Color.White);
        }
        else if (selectedMenuIndex == 2) //when the Third box is selected
        {
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 300, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 180, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 60, 400, 100), Color.Gray);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 60, 400, 100), Color.White);
        }
        else if (selectedMenuIndex == 3) //when the Fourth box is selected
        {
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 300, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 180, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) - 60, 400, 100), Color.White);
            spriteBatch.Draw(TLib.Pixel, new Rectangle((1920 / 2) - 200, (1080 / 2) + 60, 400, 100), Color.Gray);
        }
        // Draw the text
        spriteBatch.DrawString(FLib.DebugFont, "PLAY GAME", new Vector2((1920/2)-50, (1080/2)-260), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "SETTINGS", new Vector2((1920/2)-50, (1080/2)-140), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "CREDITS", new Vector2((1920/2)-50, (1080/2)-20), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "EXIT GAME", new Vector2((1920/2)-50, (1080/2)+100), Color.Black);
        spriteBatch.DrawString(FLib.DebugFont, "PRESS A ON CONTROLLER TO ENTER MENU", new Vector2((1920/2)-175, (1080/2)+200), Color.GhostWhite);

        // Draw the arrow
        spriteBatch.Draw(TLib.Arrow, new Vector2((1920/2)-250, (1080/2)-275 + (SelectedMenuIndex * 120)), Color.Gray);

        spriteBatch.End( );
    }

    public void OnSceneStart() { }

    public void OnSceneEnd() { }

    enum MenuItems { //declaring the menu items box values

        Play = 0, //sets the value of the box Play Game to 0
        Controls = 1, //Sets the value of the Settings Box to 1
        Credits = 2, //Sets the value of the Credits box to 2
        Exit = 3 //sets the value of Exit Game box to 3
    }
}
