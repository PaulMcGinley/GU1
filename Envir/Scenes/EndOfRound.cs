using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class EndOfRound : IScene {

    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (IsGamePadButtonDown(0, Buttons.A)) {                                                               // If the A button is pressed
            GameState.CurrentScene = GameScene.StartOfRound;                                                     // Move to the MainMenu scene
        }
    }

    public void FixedUpdate(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch) {

        if (GameState.CurrentScene != GameScene.EndOfRound)                                               // If the current scene is not the EndOfRound scene
            return;                                                                                         // Exit the method

        spriteBatch.Begin();                                                                                 // Begin drawing

        for (int i = 0; i < GameState.Players.Count; i++) {                                                // For each player in the game

            var player = GameState.Players[i];                                                              // Get the player
            var position = new Vector2(1920/2, 300 + (i * 50));                                                // Calculate the position of the player
            var score = player.Score;                                                                       // Get the player's score

            spriteBatch.DrawString(FLib.DebugFont, $"{player.ControllerIndex}: {score}", position + new Vector2(1,1), Color.Black);             // Draw the player's name and score
            spriteBatch.DrawString(FLib.DebugFont, $"{player.ControllerIndex}: {score}", position, Color.White);             // Draw the player's name and score
        }

        spriteBatch.End();                                                                                   // End drawing
    }

    public void OnSceneStart() { }

    public void OnSceneEnd() { 

       // GameState.CurrentScene = GameScene.StartOfRound;                                                   // Move to the StartOfRound scene
    }
}
