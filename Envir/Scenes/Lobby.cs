using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Lobby : IScene {

    Random rand = new();

    List<int> controllerIndexs = new();                                                                     // List of controller indexes that have joined the lobby

    float nessies => controllerIndexs.Count / 3.3f;                                                         // Nessies should be 33% of the players
    float tourists => controllerIndexs.Count - nessies;                                                     // Tourists should be the remaining players

    FancyNumber playerCount = new();                                                                        // Fancy number for the player count
    FancyNumber nessieCount = new();                                                                        // Fancy number for the nessie count
    FancyNumber touristCount = new();                                                                       // Fancy number for the tourist count

    float animationScale = 0;                                                                               // Scale animated text


    #region IScene Implementation

    public void Initialize(GraphicsDevice device) { }                                                       // Not Implemented

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        // Check for new controllers joining the game
        for (int i = 0; i < 16; i++)
            if (IsGamePadConnected(i) && !controllerIndexs.Contains(i) && IsGamePadButtonPressed(i, Buttons.A))
                controllerIndexs.Add(i);

        // Check for controllers leaving the game
        for (int i = 0; i < 16; i++)
            if (IsGamePadConnected(i) && controllerIndexs.Contains(i) && IsGamePadButtonPressed(i, Buttons.B))
                controllerIndexs.Remove(i);

        // Update the fancy numbers
        playerCount.Value = (uint)controllerIndexs.Count;                                                   // Update the player count based on the number of controllers that have joined
        nessieCount.Value = (uint)Math.Round(nessies);                                                      // Update the nessie count based on the lambda function 'nessies'
        touristCount.Value = (uint)Math.Round(tourists);                                                    // Update the tourist count based on the lambda function 'tourists'

        // Check for input to leave the lobby
        if (IsAnyInputPressed(Keys.B, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;

        // Check for input to start the game
        if (IsAnyInputPressed(Buttons.Start) && controllerIndexs.Count >= 2)
            StartGame(gameTime);
    }

    public void FixedUpdate(GameTime gameTime) {

       animationScale = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 0.1f + 1;                    // Update the animation scale using a sine wave function
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        // Background
        spriteBatch.Draw(TLib.LobbyBackground, new Vector2(0, 0), Color.White);

        // Instructions
        if (controllerIndexs.Count < 2)
            spriteBatch.Draw(TLib.Press_A_ToJoin, new Vector2(1920/2, 1080 - (TLib.Press_A_ToJoin.Height/2)), new Rectangle(0,0, TLib.Press_A_ToJoin.Width, TLib.Press_A_ToJoin.Height), Color.AliceBlue, 0, new Vector2(TLib.Press_A_ToJoin.Width/2, TLib.Press_A_ToJoin.Height/2), 0.5f, SpriteEffects.None, 0);
        else
            spriteBatch.Draw(TLib.Press_Start_ToBegin, new Vector2(1920/2, 1080 - (TLib.Press_Start_ToBegin.Height/2)), new Rectangle(0,0, TLib.Press_Start_ToBegin.Width, TLib.Press_Start_ToBegin.Height), Color.AliceBlue, 0, new Vector2(TLib.Press_Start_ToBegin.Width/2, TLib.Press_Start_ToBegin.Height/2), 0.75f*animationScale, SpriteEffects.None, 0);

        // Fancy numbers
        playerCount.Draw(spriteBatch, new Vector2(1920/2, 1080/2), playerCount < 2 ? Color.Red : Color.White);  // Draw red if there are less than 2 players (not enough to start the game)
        nessieCount.Draw(spriteBatch, new Vector2(1920/4, 1080/3), Color.White);
        touristCount.Draw(spriteBatch, new Vector2(1920/4*3, 1080/3), Color.White);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        if (GameState.Players.Count == 0)                                                                   // If there are no players in the list of players (The list gets cleared when a game completes)
            controllerIndexs.Clear();                                                                       // Clear the list of controller indexes list
    }

    public void OnSceneEnd() { }                                                                            // Not Implemented

    #endregion


    void StartGame(GameTime gameTime) {

        // Check if there are enough players to start the game
        if (playerCount < 2)
            return;

        // Check if the player count has changed
        if (GameState.Players.Count != playerCount.Value) {

            GameState.Players.Clear();                                                                      // Clear the list of players

            for (int i = 0; i < playerCount.Value; i++)                                                     // Loop through the number of players
                GameState.Players.Add(new Player(controllerIndexs[i]));                                     // Add a new player to the list of players with the controller index
        }

        // Check if all players have entered their name
        foreach (Player player in GameState.Players) {

            if (player.CameraView.playerName == string.Empty) {                                             // If the player has not entered their name

                GameState.CurrentScene = GameScene.NamePlayer;                                              // Move to the NamePlayer scene
                return;
            }
        }

        GameState.CurrentScene = GameScene.StartOfRound;                                                    // Move to the StartOfRound scene
    }
}
