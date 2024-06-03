using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Scenes;

public class StartOfRound : IScene {

    double sceneStartTime = 0;                                                                              // Time the scene started
    double currentTime = 0;                                                                                 // Current time in the scene
    int screenWaitTime = 3000;                                                                              // Time to wait on each screen

    float nessieCount => GameState.Players.Count / 3.3f;                                                    // Number of nessies in the game

    bool controllersRumbled = false;                                                                        // Whether the controllers have rumbled

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) { }                                                       // Not implemented in this scene

    public void LoadContent(ContentManager content) { }                                                     // Not implemented in this scene

    public void UnloadContent() { }                                                                         // Not implemented in this scene

    public void Update(GameTime gameTime) {

        if (GameState.CurrentScene != GameScene.StartOfRound)                                               // If the current scene is not the StartOfRound scene
            return;                                                                                         // Exit the method

        if (sceneStartTime < 0.1D)                                                                          // If the scene has just started
            sceneStartTime = gameTime.TotalGameTime.TotalMilliseconds;                                      // Set the scene start time

        currentTime = gameTime.TotalGameTime.TotalMilliseconds;                                             // Update the current time

        // Rumple sequence added to queue OnSceneStart
        if (!controllersRumbled) {                                                                          // If the controllers have not rumbled
            RumbleControllers();                                                                            // Rumble the controllers
            controllersRumbled = true;                                                                      // Set flag to false to prevent rumbling again
        }

        // Once enough time has passed, move to the next scene
        if (currentTime - sceneStartTime > (screenWaitTime*2))                                              // Check if the users have had enough time to read the screen
            GameState.CurrentScene = GameScene.Playing;                                                     // Move to the Playing scene
    }

    public void FixedUpdate(GameTime gameTime) { }                                                          // Not implemented in this scene

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        // Background
        spriteBatch.Draw(TLib.mainMenuBackground, new Rectangle(0, 0, 1920, 1080), Color.White);            // Draw the main menu background

        // Draw the correct screen to show role information
        if (currentTime - sceneStartTime < screenWaitTime)                                                  // Check if we are in Nessies time window
            DrawNessieScreen(spriteBatch);                                                                  // Draw the Nessie screen
        else                                                                                                // Otherwise
            DrawTouristScreen(spriteBatch);                                                                 // Draw the Tourist screen

        spriteBatch.End();
    }

    public void OnSceneStart() {

        bool endGame = true;                                                                                // Default to true and set to false if any player has not played both roles

        foreach (Player player in GameState.Players)                                                        // Loop through all players
            if (player.playedBothRoles == false) {                                                          // If any player has not played both roles
                endGame = false;                                                                            // Set endGame to false
                break;                                                                                      // Exit the loop
            }

        if (endGame) {                                                                                      // Check if the game should end

            GameState.CurrentScene = GameScene.EndOfGame;                                                   // Move to the EndOfGame scene
            return;                                                                                         // Exit the method

        } else {                                                                                            // Otherwise

            AssignRoles();                                                                                  // Assign the roles to the players
            sceneStartTime = currentTime = 0;                                                               // Reset the scene start and current times
        }
    }

    public void OnSceneEnd() {

        controllersRumbled = false;                                                                         // Set rumbleControllers to true
    }

    #endregion


    #region Draw Methods

    public void DrawNessieScreen(SpriteBatch spriteBatch) {

        // Avatar
        spriteBatch.Draw(TLib.NessieAvatar, new Vector2(1920 / 2, 1080 / 2), null, Color.White, 0, new Vector2(TLib.NessieAvatar.Width / 2, TLib.NessieAvatar.Height / 2), 0.5f, SpriteEffects.None, 0);

        // Title
        spriteBatch.Draw(TLib.NessieTitle, new Vector2(1920 / 2, 1080 / 2 - 200), null, Color.White, 0, new Vector2(TLib.NessieTitle.Width / 2, TLib.NessieTitle.Height / 2), 0.5f, SpriteEffects.None, 0);
    }

    public void DrawTouristScreen(SpriteBatch spriteBatch) {

        // Avatar
        spriteBatch.Draw(TLib.TouristAvatar, new Vector2(1920 / 2, 1080 / 2), null, Color.White, 0, new Vector2(TLib.TouristAvatar.Width / 2, TLib.TouristAvatar.Height / 2), 0.5f, SpriteEffects.None, 0);

        // Title
        spriteBatch.Draw(TLib.TouristTitle, new Vector2(1920 / 2, 1080 / 2 - 200), null, Color.White, 0, new Vector2(TLib.TouristTitle.Width / 2, TLib.TouristTitle.Height / 2), 0.5f, SpriteEffects.None, 0);
    }

    #endregion


    public void AssignRoles() {

        // Default everyone to tourist
        foreach (Player player in GameState.Players)
            player.Role = ActorType.Tourist;                                                                // Set the role direct to prevent it being counted as a played role

        // Track the number of nessies that need to be assigned
        int remainingNessies = (int)Math.Round(nessieCount);                                                // Round the number of nessies to the nearest whole number

        // Assign nessies to players who have not played as Nessie before
        foreach (Player player in GameState.Players) {

            if (player.PreferredRole() == ActorType.Nessie) {

                player.SetPlayedAs(ActorType.Nessie);                                                       // Set the player as Nessie
                remainingNessies--;                                                                         // Decrement the number of nessies that need to be assigned
            }

            if (remainingNessies == 0)                                                                      // If all nessies have been assigned
                break;                                                                                      // Exit the loop
        }

        // If there are still nessies to assign, assign them to players who have played as Nessie before
        if (remainingNessies > 0) {

            // Assign nessies to players who have played as Nessie before
            foreach (Player player in GameState.Players) {

                if (player.Role == ActorType.Tourist) {

                    player.SetPlayedAs(ActorType.Nessie);
                    remainingNessies--;
                }

                if (remainingNessies == 0)
                    break;
            }
        }

        // Actually set the remaining players as defined tourists
        foreach (Player player in GameState.Players.Where(p => p.Role == ActorType.Tourist))
            player.SetPlayedAs(ActorType.Tourist);
    }

    private void RumbleControllers() {

        foreach (Player player in GameState.Players)
            if (player.Role == ActorType.Nessie)
                RumbleQueue.AddRumble(player.ControllerIndex, currentTime+200, 1700, 1f, 1f);
            else
                RumbleQueue.AddRumble(player.ControllerIndex, currentTime+200 + screenWaitTime, 1700, 1f, 1f);
    }
}
