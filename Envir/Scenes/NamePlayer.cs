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

    int selectedIndex = 0;
    int Index {
        get => selectedIndex;
        set {
            value = Math.Clamp(value, 0, 2);
        }
    }

    int firstLetter = (char)'A';
    int lastLetter = (char)'Z';

    char[] playerName = new char[3] { 'A', 'A', 'A' };
    string Name => new string(playerName);

    public void Initialize(GraphicsDevice device) {

    }

    public void LoadContent(ContentManager content) {

    }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadUp)) {

            playerName[selectedIndex] = (char)(playerName[selectedIndex] + 1);
            playerName[selectedIndex] = (char)Math.Clamp(playerName[selectedIndex], firstLetter, lastLetter);
        }

        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadDown)) {

            playerName[selectedIndex] = (char)(playerName[selectedIndex] - 1);
            playerName[selectedIndex] = (char)Math.Clamp(playerName[selectedIndex], firstLetter, lastLetter);
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
        spriteBatch.DrawString(FLib.DebugFont, selectedIndex.ToString(), new Vector2(1920 / 2, 1080 / 2), Color.White);

        for (int i = 0; i < playerName.Length; i++) {

            spriteBatch.DrawString(FLib.DebugFont, playerName[i].ToString(), new Vector2(1920 / 2 + (i * 50), 1080 / 2 + 50), Color.White);
        }

        spriteBatch.End();

    }

    public void OnSceneStart() {

        foreach (Player player in GameState.Players) {

            if (player.CameraView.playerName == string.Empty) {

                playerIndex = player.ControllerIndex;
            }
        }

    }

    public void OnSceneEnd() {

        GameState.Players.Where(p => p.ControllerIndex == playerIndex).First().CameraView.playerName = Name;

        selectedIndex = 0;
        playerName = new char[3] { 'A', 'A', 'A' };
    }

}
