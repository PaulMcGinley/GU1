using System;
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

    #endregion

    public GameMain() {

    } // End of GameMain constructor

    protected override void Initialize() {

        base.Initialize();                                                                                  // ! IMPORTANT: Keep this here

        photoBook.Set(spriteBatch);                                                                         // Set the sprite batch for the photo book scene

        DeviceState.Initialize();                                                                           // Initialize the input device state to get the initial state of the devices
        FLib.Initialize(Content);                                                                           // Initialize the fonts library
        RLib.Initialize();                                                                                  // Initialize the rumble library
        TLib.Initialize(GraphicsDevice, Content);                                                           // Initialize the textures library

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

        // Set the window title based on the build configuration
#if DEBUG
        Window.Title = $"Graded Unit 1 - Albert, Alexander, Corey, Kieran, Paul";                           // Debug title, names of the team members in alphabetical order
#elif RELEASE
        Window.Title = $"Sightings";                                                                        // Release title
#endif

    } // End of Initialize method

    protected override void Update(GameTime gameTime) {

        DeviceState.Update();                                                                               // Update the state of the input devices
        RumbleQueue.Update(gameTime);                                                                       // Update the rumble queue

#if DEBUG // Debug only controls
        if (IsKeyPressed(Keys.F1))
            E.Config.ShowDebugInfo = !E.Config.ShowDebugInfo;                                               // Toggle the debug info

        if(IsAnyInputDown(Keys.Escape, Buttons.Back))                                                       // Check if the escape key or any controller back button is pressed
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
        }

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
            case GameScene.EndOfRound:
                playing.Draw(spriteBatch);
                endOfRound.Draw(spriteBatch);
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
        }

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
