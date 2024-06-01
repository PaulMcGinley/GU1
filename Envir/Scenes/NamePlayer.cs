using System;
using System.Data;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class NamePlayer : IScene {

    int playerIndex = 0;
    Color colour;

    int selectedIndex = 0;
    int Index {
        get => selectedIndex;
        set {
            value = Math.Clamp(value, 0, 2);
        }
    }

    int firstLetter = (char)'A';
    int lastLetter = (char)'Z';

    char[] playerName = new char[5] { 'A', 'A', 'A', 'A', 'A'};
    string Name => new string(playerName);

    public void Initialize(GraphicsDevice device) {

    }

    public void LoadContent(ContentManager content) {

    }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadUp)) {

            playerName[selectedIndex] = (char)(playerName[selectedIndex] + 1);
            playerName[selectedIndex] = (char)Math.Clamp(playerName[selectedIndex], firstLetter, lastLetter); // this wont work if its already out of bounds
        }

        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadDown)) {

            playerName[selectedIndex] = (char)(playerName[selectedIndex] - 1);
            playerName[selectedIndex] = (char)Math.Clamp(playerName[selectedIndex], firstLetter, lastLetter); // this wont work if its already out of bounds
        }

        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadLeft))
            selectedIndex--;

        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadRight))
            selectedIndex++;

        if (IsGamePadButtonPressed(playerIndex, Buttons.Start))
            GameState.CurrentScene = GameScene.Lobby;

    }

    public void FixedUpdate(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch) {

        if (GameState.CurrentScene != GameScene.NamePlayer)
            return;

        spriteBatch.Begin();

        spriteBatch.Draw(TLib.Pixel, new Rectangle(0, 0, 1920, 1080), Color.Black*0.85f);

        // Draw TLib.NamePlayerBackground centered on the screen
        spriteBatch.Draw(TLib.NamePlayerBackground, new Vector2(1920 / 2, 1080 / 2), null, colour, 0, new Vector2(TLib.NamePlayerBackground.Width / 2, TLib.NamePlayerBackground.Height / 2), 1, SpriteEffects.None, 0);

        // Draw TLib.NamePlayerTitle centered on the screen above the background
        spriteBatch.Draw(TLib.NamePlayerTitle, new Vector2(1920 / 2, 1080 / 2 - 180), null, colour, 0, new Vector2(TLib.NamePlayerTitle.Width / 2, TLib.NamePlayerTitle.Height / 2), 1, SpriteEffects.None, 0);

        //Draw string player index in NamePlayerTitle
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "Player " + (playerIndex + 1), 1080/2 - 190, new Vector2(1920, 1080), Color.White);

        // Draw TLib.NamePlayerCursor at the selected index
        spriteBatch.Draw(TLib.NamePlayerCursor, new Vector2(1920 / 2 + (selectedIndex * TLib.NamePlayerCursor.Width) - (2*TLib.NamePlayerCursor.Width), 1080 / 2 -5), null, colour, 0, new Vector2(TLib.NamePlayerCursor.Width / 2, TLib.NamePlayerCursor.Height / 2), 1, SpriteEffects.None, 0);

        //spriteBatch.DrawString(FLib.DebugFont, selectedIndex.ToString(), new Vector2(1920 / 2, 1080 / 2), Color.White);

        for (int i = 0; i < playerName.Length; i++) {

            spriteBatch.DrawString(FLib.DebugFont, playerName[i].ToString(), new Vector2(1920 / 2 + (i * TLib.NamePlayerCursor.Width) - (2*TLib.NamePlayerCursor.Width), (1080 / 2 )), Color.White);
        }

        spriteBatch.End();

    }

    public void OnSceneStart() {

        foreach (Player player in GameState.Players) {

            if (player.CameraView.playerName == string.Empty) {

                playerIndex = player.ControllerIndex;
                colour = player.CameraView.colour;
            }
        }

    }

    public void OnSceneEnd() {

        GameState.Players.Where(p => p.ControllerIndex == playerIndex).First().CameraView.playerName = Name;

        selectedIndex = 0;
        colour = Color.White;
        playerName = new char[5] { 'A', 'A', 'A', 'A', 'A'};
    }

}
