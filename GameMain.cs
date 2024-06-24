using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1;

public class GameMain : FixedTimestampGame {

    #region Scenes

    // Create a new instance of each scene
    readonly Envir.Scenes.MainMenu mainMenu = new();
    readonly Envir.Scenes.Lobby lobby = new();
    readonly Envir.Scenes.Playing playing = new();
    readonly Envir.Scenes.PhotoBook photoBook = new();
    readonly Envir.Scenes.Credits credits = new();
    readonly Envir.Scenes.Settings settings = new();
    readonly Envir.Scenes.StartOfRound startOfRound = new();
    readonly Envir.Scenes.EndOfRound endOfRound = new();
    readonly Envir.Scenes.NamePlayer namePlayer = new();
    readonly Envir.Scenes.PhotoViewer photoViewer = new();
    readonly Envir.Scenes.EndOfGame endOfGame = new();
    readonly Envir.Scenes.Controls controls = new();
    readonly Envir.Scenes.PauseMenu pauseMenu = new();

    #endregion

    public GameMain() {

    } // End of GameMain constructor

    protected override void Initialize() {

        base.Initialize();                                                                                  // ! IMPORTANT: Keep this here

        photoBook.Set(spriteBatch);                                                                         // Set the sprite batch for the photo book scene so it can render the photos outwith the draw method

        DeviceState.Initialize();                                                                           // Initialize the input device state to get the initial state of the devices
        FLib.Initialize(Content);                                                                           // Initialize the fonts library
        RLib.Initialize();                                                                                  // Initialize the rumble library
        TLib.Initialize(GraphicsDevice, Content);                                                           // Initialize the textures library
        SLib.Initialize(Content);                                                                           // Initialize the sounds library

        // Initialize all the scenes
        mainMenu.Initialize(GraphicsDevice);
        lobby.Initialize(GraphicsDevice);
        playing.Initialize(GraphicsDevice);
        photoBook.Initialize(GraphicsDevice);
        credits.Initialize(GraphicsDevice);
        settings.Initialize(GraphicsDevice);
        startOfRound.Initialize(GraphicsDevice);
        endOfRound.Initialize(GraphicsDevice);
        namePlayer.Initialize(GraphicsDevice);
        photoViewer.Initialize(GraphicsDevice);
        endOfGame.Initialize(GraphicsDevice);
        controls.Initialize(GraphicsDevice);

        // Hide cursor
        IsMouseVisible = false;

        GameState.LoadAchievements();                                                                       // Load the achievements

        // Load the settings
        if (!File.Exists(GameState.SettingsFile)) {                                                         // Check if the settings file exists

            GameState.LoadSettings();                                                                       // Load the settings
            GameState.SaveSettings();                                                                       // Save the settings
        }
        else                                                                                                // Otherwise
            GameState.LoadSettings();                                                                       // Load the settings

        GameState.IsFullScreenChanged += GameState_IsFullScreenChanged;                                     // Subscribe to the full screen changed event


        // Set the window title based on the build configuration
#if DEBUG
        Window.Title = $"Graded Unit 1 - Albert, Alexander, Corey, Kieran, Paul";                           // Debug title, names of the team members in alphabetical order
#elif RELEASE
        Window.Title = $"Sightings";                                                                        // Release title
#endif

    } // End of Initialize method

    /// <summary>
    /// Event handler for the full screen changed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GameState_IsFullScreenChanged(object sender, EventArgs e) {

        graphics.IsFullScreen = GameState.IsFullScreen;                                                     // Set the full screen mode to the game state full screen mode
        graphics.ApplyChanges();                                                                            // Apply the changes
    }

    protected override void Update(GameTime gameTime) {

        DeviceState.Update();                                                                               // Update the state of the input devices

        if (IsKeyPressed(Keys.F2)) {                                                                       // Check if the F11 key is pressed

            graphics.IsFullScreen = !graphics.IsFullScreen;                                                 // Toggle the full screen mode
            graphics.ApplyChanges();                                                                        // Apply the changes
        }

        RumbleQueue.Update(gameTime);                                                                       // Update the rumble queue

#if DEBUG // Debug only controls
        if (IsKeyPressed(Keys.F1))
            E.Config.ShowDebugInfo = !E.Config.ShowDebugInfo;                                               // Toggle the debug info

        if(IsAnyInputDown(Keys.Escape))                                                                     // Check if the escape key or any controller back button is pressed
            Exit();                                                                                         // Exit the game
#endif

        // Check if the scene has changed
        if (GameState.CurrentScene != GameState.PreviousScene) {

            // Call the end methods of the scenes
            switch(GameState.PreviousScene) {

                case GameScene.MainMenu:
                    mainMenu.OnSceneEnd();
                    break;
                case GameScene.Lobby:
                    lobby.OnSceneEnd();
                    break;
                case GameScene.Playing:
                    playing.OnSceneEnd();
                    break;
                case GameScene.PhotoBook:
                    photoBook.OnSceneEnd();
                    break;
                case GameScene.Credits:
                    credits.OnSceneEnd();
                    break;
                case GameScene.Settings:
                    settings.OnSceneEnd();
                    break;
                case GameScene.StartOfRound:
                    startOfRound.OnSceneEnd();
                    break;
                case GameScene.EndOfRound:
                    endOfRound.OnSceneEnd();
                    break;
                case GameScene.NamePlayer:
                    namePlayer.OnSceneEnd();
                    break;
                case GameScene.PhotoViewer:
                    photoViewer.OnSceneEnd();
                    break;
                case GameScene.EndOfGame:
                    endOfGame.OnSceneEnd();
                    break;
                case GameScene.Controls:
                    controls.OnSceneEnd();
                    break;
            }

            // Call the start methods of the scenes
            switch (GameState.CurrentScene) {

                case GameScene.MainMenu:
                    mainMenu.OnSceneStart();
                    break;
                case GameScene.Lobby:
                    lobby.OnSceneStart();
                    break;
                case GameScene.Playing:
                    playing.OnSceneStart();
                    break;
                case GameScene.PhotoBook:
                    photoBook.OnSceneStart();
                    break;
                case GameScene.Credits:
                    credits.OnSceneStart();
                    break;
                case GameScene.Settings:
                    settings.OnSceneStart();
                    break;
                case GameScene.StartOfRound:
                    startOfRound.OnSceneStart();
                    break;
                case GameScene.EndOfRound:
                    endOfRound.OnSceneStart();
                    break;
                case GameScene.NamePlayer:
                    namePlayer.OnSceneStart();
                    break;
                case GameScene.PhotoViewer:
                    photoViewer.OnSceneStart();
                    break;
                case GameScene.EndOfGame:
                    endOfGame.OnSceneStart();
                    break;
                case GameScene.Controls:
                    controls.OnSceneStart();
                    break;
            }
        }

        GameState.PreviousScene = GameState.CurrentScene;                                                   // Set the previous scene to the current scene

        // Only update the current scene
        switch (GameState.CurrentScene) {

            case GameScene.MainMenu:
                mainMenu.Update(gameTime);
                break;
            case GameScene.Lobby:
                lobby.Update(gameTime);
                break;
            case GameScene.Playing:
                playing.Update(gameTime);
                break;
            case GameScene.PhotoBook:
                photoBook.Update(gameTime);
                break;
            case GameScene.Credits:
                credits.Update(gameTime);
                break;
            case GameScene.Settings:
                settings.Update(gameTime);
                break;
            case GameScene.StartOfRound:
                startOfRound.Update(gameTime);
                break;
            case GameScene.EndOfRound:
                 endOfRound.Update(gameTime);
                break;
            case GameScene.NamePlayer:
                namePlayer.Update(gameTime);
                break;
            case GameScene.PhotoViewer:
                photoViewer.Update(gameTime);
                break;
            case GameScene.EndOfGame:
                endOfGame.Update(gameTime);
                break;
            case GameScene.Controls:
                controls.Update(gameTime);
                break;
            case GameScene.PauseMenu:
                pauseMenu.Update(gameTime);
                break;
        }

        GameState.Achievements.Update(gameTime);                                                            // Update the achievements

        base.Update(gameTime);                                                                              // ! IMPORTANT: Keep this here
    } // End of Update method

    protected override void FixedUpdate(GameTime gameTime) {

        // Only update the current scene
        switch (GameState.CurrentScene) {

            case GameScene.MainMenu:
                mainMenu.FixedUpdate(gameTime);
                break;
            case GameScene.Lobby:
                lobby.FixedUpdate(gameTime);
                break;
            case GameScene.Playing:
                playing.FixedUpdate(gameTime);
                break;
            case GameScene.PhotoBook:
                photoBook.FixedUpdate(gameTime);
                break;
            case GameScene.Credits:
                credits.FixedUpdate(gameTime);
                break;
            case GameScene.Settings:
                settings.FixedUpdate(gameTime);
                break;
            case GameScene.StartOfRound:
                startOfRound.FixedUpdate(gameTime);
                break;
            case GameScene.EndOfRound:
                endOfRound.FixedUpdate(gameTime);
                break;
            case GameScene.NamePlayer:
                namePlayer.FixedUpdate(gameTime);
                break;
            case GameScene.PhotoViewer:
                photoViewer.FixedUpdate(gameTime);
                break;
            case GameScene.EndOfGame:
                endOfGame.FixedUpdate(gameTime);
                break;
            case GameScene.Controls:
                controls.FixedUpdate(gameTime);
                break;
            case GameScene.PauseMenu:
                pauseMenu.FixedUpdate(gameTime);
                break;
        }

        base.FixedUpdate(gameTime);                                                                         // ! IMPORTANT: Keep this here
    } // End of FixedUpdate method

    protected override void Draw(GameTime gameTime) {

        GraphicsDevice.Clear(Color.Black);

        // Only draw the current scene
        switch (GameState.CurrentScene) {

            case GameScene.MainMenu:
                mainMenu.Draw(spriteBatch);
                break;
            case GameScene.Lobby:
            case GameScene.NamePlayer:
                lobby.Draw(spriteBatch);
                namePlayer.Draw(spriteBatch);
                break;
            case GameScene.Playing:
            case GameScene.PauseMenu:
            case GameScene.EndOfRound:
            case GameScene.EndOfGame:
                playing.Draw(spriteBatch);
                pauseMenu.Draw(spriteBatch);
                endOfRound.Draw(spriteBatch);
                endOfGame.Draw(spriteBatch);
                break;
            case GameScene.PhotoBook:
                photoBook.Draw(spriteBatch);
                break;
            case GameScene.Credits:
                credits.Draw(spriteBatch);
                break;
            case GameScene.Settings:
                settings.Draw(spriteBatch);
                break;
            case GameScene.StartOfRound:
                startOfRound.Draw(spriteBatch);
                break;
            case GameScene.PhotoViewer:
                photoViewer.Draw(spriteBatch);
                break;
            case GameScene.Controls:
                controls.Draw(spriteBatch);
                break;
        }

        spriteBatch.Begin();                                                                                 // Begin the sprite batch
        GameState.Achievements.Draw(spriteBatch);                                                           // Draw the achievements
        spriteBatch.End();                                                                                   // End the sprite batch

        base.Draw(gameTime);                                                                                // ! IMPORTANT: Keep this here

        // -------------------------------------------------------------------------------------------------
        // --- DEBUG / DIAGNOSTIC CODE ---------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------

#if DEBUG
        if(E.Config.ShowDebugInfo) {

            spriteBatch.Begin();

            // ----- Update Trackers -----
            DrawShadowedText(spriteBatch, FLib.DebugFont, $"FPS: {DrawTracker.AverageEPS:0.00} / {DrawTracker.Counter}", new Vector2(11, 11), Color.White, new Vector2(1, 1), Color.Black);

            // ----- Operating System Info -----
            DrawShadowedText(spriteBatch, FLib.DebugFont, $"OS Description: {RuntimeInformation.OSDescription}", new Vector2(11, 71), Color.White, new Vector2(1, 1), Color.Black);

            // ----- Garbage Collection Info -----
            DrawShadowedText(spriteBatch, FLib.DebugFont, $"GC Total Memory: {GC.GetTotalMemory(false) / 1024 / 1024} MB", new Vector2(11, 91), Color.White, new Vector2(1, 1), Color.Black);
            DrawShadowedText(spriteBatch, FLib.DebugFont, $"GC Collection Count: Gen1({GC.CollectionCount(0)}) / Gen2({GC.CollectionCount(1)}) / Gen3({GC.CollectionCount(2)})", new Vector2(11, 111), Color.White, new Vector2(1, 1), Color.Black);

            spriteBatch.End();
        }
#endif
    } // End of Draw method
} // End of GameMain class
