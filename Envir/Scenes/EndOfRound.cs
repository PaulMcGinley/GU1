using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class EndOfRound : IScene {

    float animationScale = 0;                                                                                // Scale animated text

    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (IsGamePadButtonDown(0, Buttons.A)) {                                                               // If the A button is pressed
            GameState.CurrentScene = GameScene.StartOfRound;                                                     // Move to the MainMenu scene
        }
    }

    public void FixedUpdate(GameTime gameTime) {

        animationScale = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 0.1f + 1;                     // Update the animation scale
    }

    public void Draw(SpriteBatch spriteBatch) {

        if (GameState.CurrentScene != GameScene.EndOfRound)                                               // If the current scene is not the EndOfRound scene
            return;                                                                                         // Exit the method

        spriteBatch.Begin();

        // Leaderboard Title
        spriteBatch.Draw(TLib.LeaderboardTitle, new Vector2(1920/2, 200), null, Color.White, 0, new Vector2(TLib.LeaderboardTitle.Width/2 , TLib.LeaderboardTitle.Height), 0.5f, SpriteEffects.None, 0);            // Draw the leaderboard

        // List of players and their scores sorted by score, highest first
        List<Player> sortedPlayers = GameState.Players;                                                      // Get the players in the game
        sortedPlayers.Sort((a, b) => b.Score.CompareTo(a.Score));                                             // Sort the players by score

        for (int i = 0; i < GameState.Players.Count; i++) {                                                // For each player in the game

            Player p = GameState.Players[i];                                                                // Get the player
            DrawFilledRectangle(new Rectangle((1920/2)-400, 200 + (i * 55), 800, 50), spriteBatch, p.CameraView.colour);

            if (p.Role == ActorType.Nessie)
            //spriteBatch.Draw(TLib.NessieAvatar, new Vector2((1920/2)-400+30, 200 + (i * 55)), Color.White, 
            // Draw nessie avatar scaled to 10%
            spriteBatch.Draw(TLib.NessieAvatar, new Vector2((1920/2)-400+30, 200 + (i * 55)+25), null, Color.White, 0, new Vector2(TLib.NessieAvatar.Width/2, TLib.NessieAvatar.Height/2), 0.05f, SpriteEffects.None, 0);            // Draw the player's avatar
            else
            // Draw tourist avatar scaled to 10%
            spriteBatch.Draw(TLib.TouristAvatar, new Vector2((1920/2)-400+30, 200 + (i * 55)+25), null, Color.White, 0, new Vector2(TLib.TouristAvatar.Width/2, TLib.TouristAvatar.Height/2), 0.05f, SpriteEffects.None, 0);            // Draw the player's avatar

            spriteBatch.DrawString(FLib.DebugFont, $"Player: {p.ControllerIndex+1} - {p.CameraView.playerName}", new Vector2((1920/2)-400+30+64, 200 + (i * 55)+14), Color.White);             // Draw the player's name
            spriteBatch.DrawString(FLib.DebugFont, $"{p.Score:#,##0}", new Vector2((1920/2)+400-30-100, 200 + (i * 55)+14), Color.White);             // Draw the player's score

        }

        // Draw the press A to continue text
        //spriteBatch.Draw(TLib.Press_A_ToContinue, new Vector2(1920/2, 1080 - (TLib.Press_A_ToContinue.Height/2)), new Rectangle(0,0, TLib.Press_A_ToContinue.Width, TLib.Press_A_ToContinue.Height), Color.AliceBlue, 0, new Vector2(TLib.Press_A_ToContinue.Width/2, TLib.Press_A_ToContinue.Height/2), 0.5f, SpriteEffects.None, 0);

        spriteBatch.Draw(TLib.Press_A_ToContinue, new Vector2(1920/2, 1080 - (TLib.Press_A_ToContinue.Height/2)), new Rectangle(0,0, TLib.Press_A_ToContinue.Width, TLib.Press_A_ToContinue.Height), Color.AliceBlue, 0, new Vector2(TLib.Press_A_ToContinue.Width/2, TLib.Press_A_ToContinue.Height/2), 0.5f * animationScale, SpriteEffects.None, 0);

        spriteBatch.End();
    }

    public void OnSceneStart() { }

    public void OnSceneEnd() {

       // GameState.CurrentScene = GameScene.StartOfRound;                                                   // Move to the StartOfRound scene
    }
}
