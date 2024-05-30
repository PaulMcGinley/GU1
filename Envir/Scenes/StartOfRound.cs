using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Scenes;

public class StartOfRound : IScene {

    double sceneStartTime = 0;                                                                              // Time the scene started
    double currentTime = 0;                                                                                 // Current time in the scene

    int screenWaitTime = 1;                                                                                // Time to wait on each screen

   // int playerCount = GameState.Players.Count;                                                              // Number of players in the game
    float nessieCount => GameState.Players.Count / 3.3f;                                                  // Number of nessies in the game
    float touristCount => GameState.Players.Count - nessieCount;                                                          // Number of tourists in the game

    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (sceneStartTime < 0.1f)                                                                          // If the scene has just started
            sceneStartTime = gameTime.TotalGameTime.TotalSeconds;                                           // Set the scene start time

        currentTime = gameTime.TotalGameTime.TotalSeconds;                                                  // Update the current time

        if (currentTime - sceneStartTime > (screenWaitTime*2)+3)                                                             // If the scene has been running for more than 10 seconds
            GameState.CurrentScene = GameScene.Playing;                                                     // Move to the Playing scene
    }

    public void FixedUpdate(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        if (currentTime - sceneStartTime < screenWaitTime)                                                              // If the scene has been running for less than 10 seconds
            DrawNessieScreen(spriteBatch);                                                                  // Draw the Nessie screen
        else if (currentTime - sceneStartTime < screenWaitTime*2)                                                         // If the scene has been running for less than 20 seconds
            DrawTouristScreen(spriteBatch);                                                                 // Otherwise, draw the Tourist screen
        else
            DrawCountdownScreen(spriteBatch);                                                               // Otherwise, draw the countdown screen

        spriteBatch.End();

    }

    public void DrawNessieScreen(SpriteBatch spriteBatch) {

        spriteBatch.Draw(TLib.mainMenuBackground, new Rectangle(0, 0, 1920, 1080), Color.Blue);              // Draw the main menu background
    }

    public void DrawTouristScreen(SpriteBatch spriteBatch) {


        spriteBatch.Draw(TLib.mainMenuBackground, new Rectangle(0, 0, 1920, 1080), Color.Red);              // Draw the main menu background
    }

    private void DrawCountdownScreen(SpriteBatch spriteBatch) {


       // spriteBatch.Draw(TLib.mainMenuBackground, new Rectangle(0, 0, 1920, 1080), Color.Black);              // Draw the main menu background
       DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "", (1080/2), new Vector2(1920/2, 1080/2), Color.White);
    }

    public void AssignRoles() {

        // Default everyone to tourist
        foreach (Player player in GameState.Players)
            player.Role = ActorType.Tourist;

        // Track the number of nessies that need to be assigned
        int remainingNessies = (int)Math.Round(nessieCount);

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

        sceneStartTime = currentTime = 0;                                                                    // Reset the scene start and current times

        AssignRoles();

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

    public void OnSceneEnd() { }

}
