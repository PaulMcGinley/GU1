using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Lobby : IScene {

    List<int> controllerIndexs = new();
    // nessies should be 33% of the players rounded up
    float nessies => controllerIndexs.Count / 3.3f;
    float tourists => controllerIndexs.Count - nessies;

    public void Initialize(GraphicsDevice device) {

    }

    public void LoadContent(ContentManager content) {

    }

    public void UnloadContent() {

    }

    public void Update(GameTime gameTime) {

        if (IsKeyPressed(Keys.OemPlus))
            controllerIndexs.Add(0);

        if (IsKeyPressed(Keys.OemMinus))
            controllerIndexs.Remove(0);

        for (int i = 0; i < 16; i++)
            if (IsGamePadConnected(i) && !controllerIndexs.Contains(i) && IsGamePadButtonPressed(i, Buttons.A))
                controllerIndexs.Add(i);

        for (int i = 0; i < 16; i++)
            if (IsGamePadConnected(i) && controllerIndexs.Contains(i) && IsGamePadButtonPressed(i, Buttons.B))
                controllerIndexs.Remove(i);

        // for (int i = 0; i < 16; i++)
        //     if (IsGamePadConnected(i) && controllerIndexs.Contains(i) && IsGamePadButtonPressed(i, Buttons.Start))
        //         GameState.CurrentScene = GameScene.Playing;

        if (IsAnyInputDown(Keys.Back, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;

        if (IsAnyInputDown(Buttons.Start) && controllerIndexs.Count > 1)
            StartGame();
    }

    public void FixedUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.DrawString(FLib.DebugFont, "Lobby Scene", new(10, 10), Color.White);

        spriteBatch.DrawString(FLib.DebugFont, "Press A to join, B to unjoin. Press START to begin.", new(10, 30), Color.White);

        spriteBatch.DrawString(FLib.DebugFont, "Press BACK to go back to the main menu.", new(10, 50), Color.White);

        // Nessie should be 25

        spriteBatch.DrawString(FLib.DebugFont, $"Players in queue: {controllerIndexs.Count} (Split:    Nessie: {Math.Round( nessies )}    |   Tourist: {Math.Round( tourists )})", new(10, 500), Color.White);
        spriteBatch.DrawString(FLib.DebugFont, $"{string.Join(", ", controllerIndexs.ToArray())}", new(11, 520), Color.White);


        spriteBatch.End();

    }


    void StartGame() {

        GameState.CurrentScene = GameScene.Playing;
    }

    public void OnSceneStart() {

        System.Console.WriteLine("Lobby Scene Started");
    }

    public void OnSceneEnd() {

        System.Console.WriteLine("Lobby Scene Ended");
    }
}
