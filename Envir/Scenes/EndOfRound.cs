using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class EndOfRound : IScene {

    float animationScale = 0;                                                                               // Scale animated text

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) { }                                                       // Not Implemented

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        if (IsAnyInputPressed(Keys.B, Buttons.A, Buttons.B))                                                   // Check for input to continue
            GameState.CurrentScene = GameScene.StartOfRound;                                                // Move to the next scene
    }

    public void FixedUpdate(GameTime gameTime) {

        animationScale = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 0.1f + 1;                   // Update the animation scale
    }

    public void Draw(SpriteBatch spriteBatch) {

        // Don't draw the leader board if the game is not in the EndOfRound scene
        if (GameState.CurrentScene != GameScene.EndOfRound)
            return;

        spriteBatch.Begin();

        // Leader board title
        spriteBatch.Draw(
            TLib.LeaderboardTitle,                                                                          // Texture
            new Vector2(1920/2, 200),                                                                       // Position
            null,                                                                                           // Source Rectangle
            Color.White,                                                                                    // Colour
            0,                                                                                              // Rotation
            new Vector2(                                                                                    // Origin
                TLib.LeaderboardTitle.Width/2,                                                              // - X
                TLib.LeaderboardTitle.Height),                                                              // - Y
            0.5f,                                                                                           // Scale
            SpriteEffects.None,                                                                             // Effects
            0);                                                                                             // Layer


        // Sort the players by score
        List<Player> sortedPlayers = GameState.Players;                                                     // Get the players in the game
        sortedPlayers.Sort((a, b) => b.Score.CompareTo(a.Score));                                           // Sort the players by score, highest first


        // Draw the leader board
        for (int i = 0; i < GameState.Players.Count; i++) {

            Player p = GameState.Players[i];                                                                // Get the player

            // Draw the player's row background
            DrawFilledRectangle(new Rectangle((1920/2)-400, 200 + (i * 55), 800, 50), spriteBatch, p.CameraView.colour);

            if (p.Role == ActorType.Nessie)
                spriteBatch.Draw(                                                                           // Draw the player's avatar (Nessie)
                    TLib.NessieAvatar,                                                                      // Texture
                    new Vector2(                                                                            // Position
                        (1920/2)-400+30,                                                                    // - X
                        200 + (i * 55)+25),                                                                 // - Y
                    null,                                                                                   // Source Rectangle
                    Color.White,                                                                            // Colour
                    0,                                                                                      // Rotation
                    new Vector2(                                                                            // Origin
                        TLib.NessieAvatar.Width/2,                                                          // - X
                        TLib.NessieAvatar.Height/2),                                                        // - Y
                    0.05f,                                                                                  // Scale
                    SpriteEffects.None,                                                                     // Effects
                    0);                                                                                     // Layer
            else
                spriteBatch.Draw(                                                                           // Draw the player's avatar (Tourist)
                    TLib.TouristAvatar,                                                                     // Texture
                    new Vector2(                                                                            // Position
                        (1920/2)-400+30,                                                                    // - X
                        200 + (i * 55)+25),                                                                 // - Y
                    null,                                                                                   // Source Rectangle
                    Color.White,                                                                            // Colour
                    0,                                                                                      // Rotation
                    new Vector2(                                                                            // Origin
                        TLib.TouristAvatar.Width/2,                                                         // - X
                        TLib.TouristAvatar.Height/2),                                                       // - Y
                    0.05f,                                                                                  // Scale
                    SpriteEffects.None,                                                                     // Effects
                    0);                                                                                     // Layer

            spriteBatch.DrawString(FLib.DebugFont, $"Player: {p.ControllerIndex+1} - {p.CameraView.playerName}", new Vector2((1920/2)-400+30+64, 200 + (i * 55)+14), Color.White);      // Draw the player's name
            spriteBatch.DrawString(FLib.DebugFont, $"{p.Score:#,##0}", new Vector2((1920/2)+400-30-100, 200 + (i * 55)+14), Color.White);                                               // Draw the player's score

        }

        // Draw the press A to continue text
        spriteBatch.Draw(TLib.Press_A_ToContinue, new Vector2(1920/2, 1080 - (TLib.Press_A_ToContinue.Height/2)), new Rectangle(0,0, TLib.Press_A_ToContinue.Width, TLib.Press_A_ToContinue.Height), Color.AliceBlue, 0, new Vector2(TLib.Press_A_ToContinue.Width/2, TLib.Press_A_ToContinue.Height/2), 0.5f * animationScale, SpriteEffects.None, 0);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        //SLib.Victory.Play();                                                                                // Play the victory sound
    }

    public void OnSceneEnd() {

    }

    #endregion
}
