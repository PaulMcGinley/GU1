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

    FancyNumber playerCount = new();
    FancyNumber nessieCount = new();
    FancyNumber touristCount = new();

    float animationScale = 0;                                                                                // Scale animated text

    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

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
        playerCount.Value = (uint)controllerIndexs.Count;
        nessieCount.Value = (uint)Math.Round(nessies);
        touristCount.Value = (uint)Math.Round(tourists);

        // Check for input to leave the lobby
        if (IsAnyInputDown(Keys.Back, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;

        // Check for input to start the game
        if (IsAnyInputDown(Buttons.Start) && controllerIndexs.Count > 1)
            StartGame(gameTime);
    }

    public void FixedUpdate(GameTime gameTime) {

       animationScale = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 0.1f + 1;
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.Draw(TLib.LobbyBackground, new Vector2(0, 0), Color.White);

        if (controllerIndexs.Count < 2)
            spriteBatch.Draw(TLib.Press_A_ToJoin, new Vector2(1920/2, 1080 - (TLib.Press_A_ToJoin.Height/2)), new Rectangle(0,0, TLib.Press_A_ToJoin.Width, TLib.Press_A_ToJoin.Height), Color.AliceBlue, 0, new Vector2(TLib.Press_A_ToJoin.Width/2, TLib.Press_A_ToJoin.Height/2), 0.5f, SpriteEffects.None, 0);
        else
            spriteBatch.Draw(TLib.Press_Start_ToBegin, new Vector2(1920/2, 1080 - (TLib.Press_Start_ToBegin.Height/2)), new Rectangle(0,0, TLib.Press_Start_ToBegin.Width, TLib.Press_Start_ToBegin.Height), Color.AliceBlue, 0, new Vector2(TLib.Press_Start_ToBegin.Width/2, TLib.Press_Start_ToBegin.Height/2), 0.75f*animationScale, SpriteEffects.None, 0);

        playerCount.Draw(spriteBatch, new Vector2(1920/2, 1080/2), playerCount < 2 ? Color.Red : Color.White);
        nessieCount.Draw(spriteBatch, new Vector2(1920/4, 1080/3), Color.White);
        touristCount.Draw(spriteBatch, new Vector2(1920/4*3, 1080/3), Color.White);

        spriteBatch.End();
    }

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

            if (player.CameraView.playerName == string.Empty) {                                            // If the player has not entered their name

                GameState.CurrentScene = GameScene.NamePlayer;                                              // Move to the NamePlayer scene
                return;
            }
        }

        GameState.CurrentScene = GameScene.StartOfRound;
    }


    public void OnSceneStart() {

        for (int i = 0; i < 16; i++)
            GamePad.SetVibration(i, 0, 0);                                                                     // Stop any controller vibration

        // bool endGame = true;                                                                                // Default to true and set to false if any player has not played both roles

        // foreach (Player player in GameState.Players)                                                        // Loop through all players
        //     if (player.playedBothRoles == false) {                                                          // If any player has not played both roles
        //         endGame = false;                                                                            // Set endGame to false
        //         break;                                                                                      // Exit the loop
        //     }

        // if (endGame) {

        //     // Do something
        //     // GameState.CurrentScene = GameScene.EndGame;
        //     //return;
        // }
    }

    public void OnSceneEnd() {

    }
}
