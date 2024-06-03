using System;
using System.Data;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class NamePlayer : IScene {

    int playerIndex = 0;                                                                                    // The index of the player that is naming themselves
    Color colour;                                                                                           // The colour of the player
    bool controllerRumbled = false;                                                                         // Flag to check if the controller has rumbled

    int selectedIndex = 0;                                                                                  // The index of the selected letter (variable)
    int Index {                                                                                             // The index of the selected letter (property)
        get => selectedIndex;                                                                               // Get the selected letter index
        set {
            if (value < 0)                                                                                  // If the value is less than 0
                selectedIndex = 0;                                                                          // Set the selected index to 0 (prevent out of bounds)
            else if (value > 4)                                                                             // If the value is greater than 4
                selectedIndex = 4;                                                                          // Set the selected index to 4 (prevent out of bounds)
            else
                selectedIndex = value;                                                                      // Otherwise set the selected index to the value

            SLib.Click.Play(GameState.SFXVolume, 0, 0);
        }
    }

    readonly int firstLetter = 'A';                                                                         // The first letter in the alphabet
    readonly int lastLetter = 'Z';                                                                          // The last letter in the alphabet

    char[] playerName = new char[5] { 'A', 'A', 'A', 'A', 'A'};                                             // The name of the player in a char array
    string Name => new(playerName);                                                                         // Lambda function to convert the char array to a string

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) { }                                                       // Not Implemented

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        // Rumble player controller
        if (controllerRumbled == false) {

            RumbleQueue.AddRumble(playerIndex, gameTime.TotalGameTime.TotalMilliseconds, 250, 1f, 1f);      // Rumble the controller
            controllerRumbled = true;                                                                       // Set the flag to true
        }

        // Check for input (UP)
        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadUp)) {

            playerName[Index] = (char)(playerName[Index] + 1);                                              // Increment the selected letter
            playerName[Index] = (char)Math.Clamp(playerName[Index], firstLetter, lastLetter);               // Clamp the selected letter to the alphabet
        }

        // Check for input (DOWN)
        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadDown)) {

            playerName[Index] = (char)(playerName[Index] - 1);                                              // Decrement the selected letter
            playerName[Index] = (char)Math.Clamp(playerName[Index], firstLetter, lastLetter);               // Clamp the selected letter to the alphabet
        }

        // Check for input (LEFT)
        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadLeft))
            Index--;                                                                                        // Decrement the selected index

        // Check for input (RIGHT)
        if (IsGamePadButtonPressed(playerIndex, Buttons.DPadRight))
            Index++;                                                                                        // Increment the selected index

        // Check for input (START) to confirm
        if (IsGamePadButtonPressed(playerIndex, Buttons.Start))
            GameState.CurrentScene = GameScene.StartOfRound;                                                       // Move to the next scene

    }

    public void FixedUpdate(GameTime gameTime) { }                                                          // Not Implemented

    public void Draw(SpriteBatch spriteBatch) {

        // Guard clause to check if the current scene is the NamePlayer scene
        if (GameState.CurrentScene != GameScene.NamePlayer)
            return;

        spriteBatch.Begin();

        // Draw a black overlay with 85% opacity
        spriteBatch.Draw(TLib.Pixel, new Rectangle(0, 0, 1920, 1080), Color.Black*0.85f);

        // Window frame
        spriteBatch.Draw(TLib.NamePlayerBackground, new Vector2(1920 / 2, 1080 / 2), null, colour, 0, new Vector2(TLib.NamePlayerBackground.Width / 2, TLib.NamePlayerBackground.Height / 2), 1, SpriteEffects.None, 0);

        // Window title
        spriteBatch.Draw(TLib.NamePlayerTitle, new Vector2(1920 / 2, 1080 / 2 - 180), null, colour, 0, new Vector2(TLib.NamePlayerTitle.Width / 2, TLib.NamePlayerTitle.Height / 2), 1, SpriteEffects.None, 0);

        // Title text
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, $"Player {playerIndex + 1} - {Name}", 1080/2 - 190, new Vector2(1920, 1080), Color.White);

        // Index cursor
        spriteBatch.Draw(TLib.NamePlayerCursor, new Vector2(1920 / 2 + (Index * TLib.NamePlayerCursor.Width) - (2*TLib.NamePlayerCursor.Width), 1080 / 2 -5), null, colour, 0, new Vector2(TLib.NamePlayerCursor.Width / 2, TLib.NamePlayerCursor.Height / 2), 1, SpriteEffects.None, 0);

        // Letters
        for (int i = 0; i < playerName.Length; i++)
            spriteBatch.DrawString(FLib.DebugFont, playerName[i].ToString(), new Vector2(1920 / 2 + (i * TLib.NamePlayerCursor.Width) - (2*TLib.NamePlayerCursor.Width), (1080 / 2 )), Color.White);

        // Instructions
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "Press START to confirm", 1080/2 + 190, new Vector2(1920, 1080), Color.White);

        spriteBatch.End();

    }

    public void OnSceneStart() {

        foreach (Player player in GameState.Players)
            if (player.CameraView.playerName == string.Empty) {

                playerIndex = player.ControllerIndex;
                colour = player.CameraView.colour;
            }
    }

    public void OnSceneEnd() {

        // Save the player's name
        GameState.Players.Where(p => p.ControllerIndex == playerIndex).First().CameraView.playerName = Name;

        // Reset the scene
        selectedIndex = 0;
        colour = Color.White;
        playerName = new char[5] { 'A', 'A', 'A', 'A', 'A'};
        controllerRumbled = false;

        // Check if all players have entered their name
        foreach (Player player in GameState.Players)
            if (player.CameraView.playerName == string.Empty)
                GameState.CurrentScene = GameScene.NamePlayer;
    }

    #endregion

}
