using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Lobby : IScene {

    List<int> controllerIndexs = new();                                                                     // List of controller indexes that have joined the lobby

    float nessies => controllerIndexs.Count / 3.3f;                                                         // Nessies should be 33% of the players
    float tourists => controllerIndexs.Count - nessies;                                                     // Tourists should be the remaining players

    FancyNumber playerCount = new();
    FancyNumber nessieCount = new();
    FancyNumber touristCount = new();

    public void Initialize(GraphicsDevice device) {

    }

    public void LoadContent(ContentManager content) {

    }

    public void UnloadContent() {

    }

    public void Update(GameTime gameTime) {

        for (int i = 0; i < 16; i++)
            if (IsGamePadConnected(i) && !controllerIndexs.Contains(i) && IsGamePadButtonPressed(i, Buttons.A))
                controllerIndexs.Add(i);

        for (int i = 0; i < 16; i++)
            if (IsGamePadConnected(i) && controllerIndexs.Contains(i) && IsGamePadButtonPressed(i, Buttons.B))
                controllerIndexs.Remove(i);

        playerCount.Value = (uint)controllerIndexs.Count;
        nessieCount.Value = (uint)Math.Round(nessies);
        touristCount.Value = (uint)Math.Round(tourists);

        if (IsAnyInputDown(Keys.Back, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;

        if (IsAnyInputDown(Buttons.Start) && controllerIndexs.Count > 1)
            StartGame();
    }

    public void FixedUpdate(GameTime gameTime) {


    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.Draw(TLib.LobbyBackground, new Vector2(0, 0), Color.White);

        // TODO: Use TextStudio to generate a graphic to replace this text
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "Press A to join, B to leave. Press START to begin.", 1080-100, new Vector2(1920,1080), Color.Black);
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "Press BACK to go back to the main menu.", 1080-70, new Vector2(1920,1080), Color.Black);

        //spriteBatch.DrawString(FLib.DebugFont, $"Players in queue: {controllerIndexs.Count} (Split:    Nessie: {Math.Round( nessies )}    |   Tourist: {Math.Round( tourists )})", new(10, 500), Color.White);
        //spriteBatch.DrawString(FLib.DebugFont, $"{string.Join(", ", controllerIndexs.ToArray())}", new(11, 520), Color.White);

        playerCount.Draw(spriteBatch, new Vector2(1920/2, 1080/2), playerCount < 2 ? Color.Red : Color.White);
        nessieCount.Draw(spriteBatch, new Vector2(1920/4, 1080/3), Color.White);
        touristCount.Draw(spriteBatch, new Vector2(1920/4*3, 1080/3), Color.White);

        spriteBatch.End();

    }


    void StartGame() {

        if (playerCount < 2)
            return;

        GameState.CurrentScene = GameScene.Playing;
    }

    public void OnSceneStart() {

    }

    public void OnSceneEnd() {

    }
}
