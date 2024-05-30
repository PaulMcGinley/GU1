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

    public void FixedUpdate(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.Draw(TLib.LobbyBackground, new Vector2(0, 0), Color.White);

        // TODO: Use TextStudio to generate a graphic to replace this text
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "Press A to join, B to leave. Press START to begin.", 1080-100, new Vector2(1920,1080), Color.Black);
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "Press BACK to go back to the main menu.", 1080-70, new Vector2(1920,1080), Color.Black);

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

        // Assign roles to the players
        AssignRoles();

        // vibrate the controllers to indicate the start of the game / role assignment
        VibrateControllers(gameTime);

        // Set the scene to playing
        // ! This may need to be moved to an area after where the roles are assigned
        GameState.CurrentScene = GameScene.Playing;
    }

    private void VibrateControllers(GameTime gameTime) {

        foreach (Player player in GameState.Players) {

            if (player.Role == ActorType.Nessie)
                RumbleQueue.AddRumble(player.ControllerIndex, 2000, gameTime.ElapsedGameTime.TotalMilliseconds, 1, 1);
            else
                RumbleQueue.AddRumble(player.ControllerIndex, 2000, gameTime.ElapsedGameTime.TotalMilliseconds + 2500, 1f, 1f);
        }

    }

    // ! There is a bug where sometimes not all roles are assigned correctly
    // ? This might be resolved now
    public void AssignRoles() {

        // Default everyone to tourist
        foreach (Player player in GameState.Players)
            player.Role = ActorType.Tourist;

        // Track the number of nessies that need to be assigned
        int remainingNessies = (int)nessieCount.Value;

        // Assign nessies to players who have not played as Nessie before
        foreach (Player player in GameState.Players) {

            if (player.PreferredRole() == ActorType.Nessie) {

                player.Role = ActorType.Nessie;
                remainingNessies--;
            }

            if (remainingNessies == 0)
                break;
        }

        // If there are still nessies to assign, assign them to players who have played as Nessie before
        if (remainingNessies > 0) {

            // Assign nessies to players who have played as Nessie before
            foreach (Player player in GameState.Players) {

                if (player.Role == ActorType.Tourist) {

                    player.Role = ActorType.Nessie;
                    remainingNessies--;
                }

                if (remainingNessies == 0)
                    break;
            }
        }
    }

    public void OnSceneStart() {

        bool endGame = true;                                                                                // Default to true and set to false if any player has not played both roles

        foreach (Player player in GameState.Players)                                                        // Loop through all players
            if (player.playedBothRoles == false) {                                                          // If any player has not played both roles
                endGame = false;                                                                            // Set endGame to false
                break;                                                                                      // Exit the loop
            }

        if (endGame) {

            // Do something
            // GameState.CurrentScene = GameScene.EndGame;
            //return;
        }
    }

    public void OnSceneEnd() {

    }
}
