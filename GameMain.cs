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

    #endregion

    public GameMain() {

    } // End of GameMain constructor

    protected override void Initialize() {

        base.Initialize();                                                                                  // ! IMPORTANT: Keep this here

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

        // Set the window title based on the build configuration
#if DEBUG
        Window.Title = $"Graded Unit 1 - Albert, Alexander, Corey, Kieran, Paul";                           // Debug title, names of the team members in alphabetical order
#elif RELEASE
        Window.Title = $"Sightings";                                                                        // Release title
#endif

    } // End of Initialize method

    protected override void Update(GameTime gameTime) {

        DeviceState.Update();                                                                               // Update the state of the input devices

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
            }
        }

        GameState.PreviousScene = GameState.CurrentScene;                                                    // Set the previous scene to the current scene

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
                lobby.Draw(spriteBatch);
                break;
            case GameScene.Playing:
                playing.Draw(spriteBatch);
                break;
            case GameScene.PhotoBook:
                photoBook.Draw(spriteBatch);
                break;
            case GameScene.Credits:
                credits.Draw(spriteBatch);
                break;
        }

        base.Draw(gameTime);                                                                                 // ! IMPORTANT: Keep this here

        // -------------------------------------------------------------------------------------------------
        // --- DEBUG / DIAGNOSTIC CODE ---------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------

#if DEBUG
        if(E.Config.ShowDebugInfo) {

            spriteBatch.Begin();

            // ----- Update Trackers -----
            spriteBatch.DrawString(FLib.DebugFont, $"Update: {UpdateTracker.AverageEPS:0.00} / {UpdateTracker.Counter}", new Vector2(11, 11), Color.Black);
            spriteBatch.DrawString(FLib.DebugFont, $"Update: {UpdateTracker.AverageEPS:0.00} / {UpdateTracker.Counter}", new Vector2(10, 10), Color.White);

            spriteBatch.DrawString(FLib.DebugFont, $"FixedUpdates: {FixedUpdateTracker.AverageEPS:0.00} / {FixedUpdateTracker.Counter}", new Vector2(11, 31), Color.Black);
            spriteBatch.DrawString(FLib.DebugFont, $"FixedUpdates: {FixedUpdateTracker.AverageEPS:0.00} / {FixedUpdateTracker.Counter}", new Vector2(10, 30), Color.White);

            spriteBatch.DrawString(FLib.DebugFont, $"Frames: {DrawTracker.AverageEPS:0.00} / {DrawTracker.Counter}", new Vector2(11, 51), Color.Black);
            spriteBatch.DrawString(FLib.DebugFont, $"Frames: {DrawTracker.AverageEPS:0.00} / {DrawTracker.Counter}", new Vector2(10, 50), Color.White);

            // ----- Operating System Info -----
            spriteBatch.DrawString(FLib.DebugFont, $"OS Description: {RuntimeInformation.OSDescription}", new Vector2(11, 71), Color.Black);
            spriteBatch.DrawString(FLib.DebugFont, $"OS Description: {RuntimeInformation.OSDescription}", new Vector2(10, 70), Color.White);

            // ----- Garbage Collection Info -----
            spriteBatch.DrawString(FLib.DebugFont, $"GC Total Memory: {GC.GetTotalMemory(false) / 1024 / 1024} MB", new Vector2(11, 91), Color.Black);
            spriteBatch.DrawString(FLib.DebugFont, $"GC Total Memory: {GC.GetTotalMemory(false) / 1024 / 1024} MB", new Vector2(10, 90), Color.White);

            spriteBatch.DrawString(FLib.DebugFont, $"GC Collection Count: Gen1({GC.CollectionCount(0)}) / Gen2({GC.CollectionCount(1)}) / Gen3({GC.CollectionCount(2)})", new Vector2(11, 111), Color.Black);
            spriteBatch.DrawString(FLib.DebugFont, $"GC Collection Count: Gen1({GC.CollectionCount(0)}) / Gen2({GC.CollectionCount(1)}) / Gen3({GC.CollectionCount(2)})", new Vector2(10, 110), Color.White);

            spriteBatch.End();
        }
#endif
    } // End of Draw method
} // End of GameMain class
