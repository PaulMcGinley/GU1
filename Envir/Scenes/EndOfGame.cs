using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class EndOfGame : IScene {

    Random rand = new();

    Player winner;

    float scale = 0.25f;

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (IsGamePadButtonDown(0, Buttons.Back)) {

            GameState.CurrentScene = GameScene.MainMenu;

            // This should be done in the OnSceneEnd method but it is not working
            scale = 0.25f;
            winner = null;
            GameState.Players.Clear();
        }

    }

    public void FixedUpdate(GameTime gameTime) {

        // This should be done in the OnSceneStart method but it is not working
        if (winner == null) {

            winner = GameState.Players[0];

            for (int i = 1; i < GameState.Players.Count; i++)
                if (GameState.Players[i].Score > winner.Score)
                    winner = GameState.Players[i];
        }

        if (scale < 1)
            scale += 0.01f;
    }

    public void Draw(SpriteBatch spriteBatch) {

        if (GameState.CurrentScene != GameScene.EndOfGame)
            return;

        if (winner == null)
            return;


        spriteBatch.Begin();

        spriteBatch.Draw(
            TLib.WinnerTitle,                                                                               // Texture
            new Vector2(1920/2, (1080/2) - 200),                                                            // Position
            null,                                                                                           // Source Rectangle
            new Color ((byte)rand.Next(64,250),(byte)rand.Next(64,250),(byte)rand.Next(64,250)),            // Colour
            0,                                                                                              // Rotation
            new Vector2(TLib.WinnerTitle.Width/2, TLib.WinnerTitle.Height/2),                               // Origin
            scale,                                                                                          // Scale
            SpriteEffects.None,                                                                             // Effects
            0);                                                                                             // Layer

        // Draw the player's row background
        DrawFilledRectangle(new Rectangle((1920/2)-400, (1080/2)+200, 800, 50), spriteBatch, winner.CameraView.colour);

        spriteBatch.DrawString(FLib.DebugFont, $"Player: {winner.ControllerIndex+1} - {winner.CameraView.playerName}", new Vector2((1920/2)-400+30+64, (1080/2)+200+14), Color.White);             // Draw the player's name
        spriteBatch.DrawString(FLib.DebugFont, $"{winner.Score:#,##0}", new Vector2((1920/2)+400-30-100, (1080/2)+200+14), Color.White);                                                            // Draw the player's score

        // spriteBatch.DrawString(
        //     FLib.DebugFont,                                                                                 // Font
        //     $"{winner.CameraView.playerName} wins!",                                                        // Text
        //     new Vector2(1920/2, (1080/2)+200),                                                              // Position
        //     Color.White,                                                                                    // Colour
        //     0,                                                                                              // Rotation
        //     FLib.DebugFont.MeasureString($"{winner.CameraView.playerName} wins!")/2,                        // Origin
        //     1,                                                                                              // Scale
        //     SpriteEffects.None,                                                                             // Effects
        //     0);                                                                                             // Layer

        spriteBatch.End();
    }

    public void OnSceneStart() {

        System.Diagnostics.Debug.WriteLine("EndOfGame.OnSceneStart");
        SLib.Victory.Play();                                                                                // Play the victory sound

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
