using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Scenes;

public class StartOfRound : IScene {

    double sceneStartTime = 0;                                                                              // Time the scene started
    double currentTime = 0;                                                                                 // Current time in the scene

    int screenWaitTime = 3000;                                                                                // Time to wait on each screen

   // int playerCount = GameState.Players.Count;                                                              // Number of players in the game
    float nessieCount => GameState.Players.Count / 3.3f;                                                  // Number of nessies in the game
    float touristCount => GameState.Players.Count - nessieCount;                                                          // Number of tourists in the game

    bool rumbleControllers = false;                                                                         // Whether to rumble the controllers
    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (GameState.CurrentScene != GameScene.StartOfRound)                                               // If the current scene is not the StartOfRound scene
            return;                                                                                         // Exit the method

        if (sceneStartTime < 0.1D)                                                                          // If the scene has just started
            sceneStartTime = gameTime.TotalGameTime.TotalMilliseconds;                                           // Set the scene start time

        currentTime = gameTime.TotalGameTime.TotalMilliseconds;                                                  // Update the current time

        if (!rumbleControllers) {                                                                              // If the controllers should rumble
            RumbleControllers();                                                                            // Rumble the controllers
            rumbleControllers = true;                                                                      // Set rumbleControllers to false
        }

        if (currentTime - sceneStartTime > (screenWaitTime*2)/*+3000*/)                                                             // If the scene has been running for more than 10 seconds
            GameState.CurrentScene = GameScene.Playing;                                                     // Move to the Playing scene
    }

    public void FixedUpdate(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.Draw(TLib.mainMenuBackground, new Rectangle(0, 0, 1920, 1080), Color.White);              // Draw the main menu background


        if (currentTime - sceneStartTime < screenWaitTime)                                                              // If the scene has been running for less than 10 seconds
            DrawNessieScreen(spriteBatch);                                                                  // Draw the Nessie screen
        else /*if (currentTime - sceneStartTime < screenWaitTime*2)*/                                                         // If the scene has been running for less than 20 seconds
            DrawTouristScreen(spriteBatch);                                                                 // Otherwise, draw the Tourist screen
        //else
        //    DrawCountdownScreen(spriteBatch);                                                               // Otherwise, draw the countdown screen

        spriteBatch.End();

    }

    public void DrawNessieScreen(SpriteBatch spriteBatch) {

        // Avatar
        spriteBatch.Draw(TLib.NessieAvatar, new Vector2(1920 / 2, 1080 / 2), null, Color.White, 0, new Vector2(TLib.NessieAvatar.Width / 2, TLib.NessieAvatar.Height / 2), 0.5f, SpriteEffects.None, 0);

        // Title
        spriteBatch.Draw(TLib.NessieTitle, new Vector2(1920 / 2, 1080 / 2 - 200), null, Color.White, 0, new Vector2(TLib.NessieTitle.Width / 2, TLib.NessieTitle.Height / 2), 0.5f, SpriteEffects.None, 0);
    }

    public void DrawTouristScreen(SpriteBatch spriteBatch) {

        // Title
        spriteBatch.Draw(TLib.TouristAvatar, new Vector2(1920 / 2, 1080 / 2), null, Color.White, 0, new Vector2(TLib.TouristAvatar.Width / 2, TLib.TouristAvatar.Height / 2), 0.5f, SpriteEffects.None, 0);

        // Title
        spriteBatch.Draw(TLib.TouristTitle, new Vector2(1920 / 2, 1080 / 2 - 200), null, Color.White, 0, new Vector2(TLib.TouristTitle.Width / 2, TLib.TouristTitle.Height / 2), 0.5f, SpriteEffects.None, 0);
    }

    private void DrawCountdownScreen(SpriteBatch spriteBatch) {


       // spriteBatch.Draw(TLib.mainMenuBackground, new Rectangle(0, 0, 1920, 1080), Color.Black);              // Draw the main menu background
       DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, (((sceneStartTime + (screenWaitTime*2) +3000)- currentTime)/1000).ToString("0.0"), (1080/2), new Vector2(1920, 1080), Color.White);
    }

    public void AssignRoles() {

        // Default everyone to tourist
        foreach (Player player in GameState.Players)
            player.Role = ActorType.Tourist; // Set the role direct to prevent it being counted as a played role

        // Track the number of nessies that need to be assigned
        int remainingNessies = (int)Math.Round(nessieCount);

        // Assign nessies to players who have not played as Nessie before
        foreach (Player player in GameState.Players) {

            if (player.PreferredRole() == ActorType.Nessie) {

                player.SetPlayedAs(ActorType.Nessie);
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

        foreach (Player player in GameState.Players) {

            if (player.Role == ActorType.Nessie)
                RumbleQueue.AddRumble(player.ControllerIndex, currentTime, 2000, 1f, 1f);
            else
                RumbleQueue.AddRumble(player.ControllerIndex, currentTime + screenWaitTime, 2000, 1f, 1f);
        }
    }

    public void OnSceneStart() {

        bool endGame = true;                                                                                // Default to true and set to false if any player has not played both roles

        foreach (Player player in GameState.Players) {                                                       // Loop through all players

            if (player.playedBothRoles == false) {                                                          // If any player has not played both roles
                endGame = false;                                                                            // Set endGame to false
                break;                                                                                      // Exit the loop
            }
        }

        if (endGame) {

            // Do something
             GameState.CurrentScene = GameScene.MainMenu;
            return;
        } else {

        AssignRoles();

        sceneStartTime = currentTime = 0;                                                                    // Reset the scene start and current times
        }
    }

    public void OnSceneEnd() {

        rumbleControllers = false;                                                                           // Set rumbleControllers to true
    }

}
