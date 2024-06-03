using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class EndOfGame : IScene {

    Random rand = new();

    Player winner;                                                                                          // The player who won the game

    float scale = 0.25f;                                                                                    // The scale of the winner title

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) { }                                                       // Not Implemented

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        if (IsAnyInputPressed(Keys.B, Buttons.B, Buttons.Back)) {

            GameState.CurrentScene = GameScene.Credits;                                                    // Change the scene to the credits scene

            // This should be done in the OnSceneEnd method but it is not working
            scale = 0.25f;                                                                                  // Reset the scale
            winner = null;                                                                                  // Reset the winner
            GameState.Players.Clear();                                                                      // Clear the players
        }
    }

    public void FixedUpdate(GameTime gameTime) {

        // This should be done in the OnSceneStart method but it is not working
        if (winner == null) {                                                                               // If the winner is not set

            winner = GameState.Players[0];                                                                  // Set the winner to the first player

            for (int i = 1; i < GameState.Players.Count; i++)                                               // Loop through the players
                if (GameState.Players[i].Score > winner.Score)                                              // If the player's score is greater than the winner's score
                    winner = GameState.Players[i];                                                          // Set the winner to that player
        }

        // Winner title zooms in
        if (scale < 1)                                                                                      // If the scale is less than 1
            scale += 0.01f;                                                                                 // Increase the scale
    }

    public void Draw(SpriteBatch spriteBatch) {

        if (GameState.CurrentScene != GameScene.EndOfGame)                                                 // If the current scene is not the end of game scene
            return;                                                                                        // Return

        if (winner == null)                                                                                // If the winner is not set
            return;                                                                                         // Return

        spriteBatch.Begin();

        // Draw the winner title
        spriteBatch.Draw(
            TLib.WinnerTitle,                                                                               // Texture
            new Vector2(1920/2, (1080/2) - 200),                                                            // Position
            null,                                                                                           // Source Rectangle
            new Color (255,255,0),                                  // Colour
            0,                                                                                              // Rotation
            new Vector2(TLib.WinnerTitle.Width/2, TLib.WinnerTitle.Height/2),                               // Origin
            scale,                                                                                          // Scale
            SpriteEffects.None,                                                                             // Effects
            0);                                                                                             // Layer

        // Draw the player's row background
        DrawFilledRectangle(new Rectangle((1920/2)-400, (1080/2)+200, 800, 50), spriteBatch, winner.CameraView.colour);

        // spriteBatch.DrawString(FLib.LeaderboardFont, $"Player: {winner.ControllerIndex+1} - {winner.CameraView.playerName}", new Vector2((1920/2)-400+30+64, (1080/2)+200+14), Color.White);  // Draw the player's name
        // spriteBatch.DrawString(FLib.LeaderboardFont, $"{winner.Score:#,##0}", new Vector2((1920/2)+400-30-100, (1080/2)+200+14), Color.White);                                                // Draw the player's score

        DrawShadowedText(spriteBatch, FLib.LeaderboardFont, $"Player: {winner.ControllerIndex+1} - {winner.CameraView.playerName}", new Vector2((1920/2)-400+30+64, (1080/2)+200+14), Color.White, new Vector2(2,2), Color.Black*0.8f);  // Draw the player's name
        DrawShadowedText(spriteBatch, FLib.LeaderboardFont, $"{winner.Score:#,##0}", new Vector2((1920/2)+400-30-100, (1080/2)+200+14), Color.White, new Vector2(2,2), Color.Black*0.8f);                                                // Draw the player's score

        spriteBatch.End();
    }

    public void OnSceneStart() {

        System.Diagnostics.Debug.WriteLine("EndOfGame.OnSceneStart");
        SLib.Victory.Play(GameState.SFXVolume, 0, 0);                                                       // Play the victory sound

        // Should be able to use LINQ here
        // winner = GameState.Players[0];
        // for (int i = 1; i < GameState.Players.Count; i++)
        //     if (GameState.Players[i].Score > winner.Score)
        //         winner = GameState.Players[i];
    }

    public void OnSceneEnd() {

        System.Diagnostics.Debug.WriteLine("EndOfGame.OnSceneEnd");

        // scale = 0.25f;
        // winner = null;
        // GameState.Players.Clear();
    }

    #endregion

}
