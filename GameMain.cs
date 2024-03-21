using GU1.Engine.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1;

public class GameMain : FixedTimestampGame {

    GameScene currentScene = GameScene.MainMenu;                                                            // The currently active scene (TODO: Move to Game Global)

    // Create an instance of each scene
    Envir.Scenes.MainMenu mainMenu = new();
    Envir.Scenes.Lobby lobby = new();
    Envir.Scenes.Playing playing = new();
    Envir.Scenes.PhotoBook photoBook = new();

    public GameMain() {

    } // End of GameMain constructor

    protected override void Initialize() {

        DeviceState.Initialize();                                                                           // Initialize the input device state to get the initial state of the devices
        Libs.Fonts.Initialize(Content);                                                                     // Initialize the fonts library

        // Initialize all the scenes
        mainMenu.Initialize(GraphicsDevice);
        lobby.Initialize(GraphicsDevice);
        playing.Initialize(GraphicsDevice);
        photoBook.Initialize(GraphicsDevice);

        base.Initialize();
    } // End of Initialize method

    protected override void LoadContent() {

        base.LoadContent();                                                                                 // Call the base load content method to load the content manager

        // Load the content for all the scenes
        mainMenu.LoadContent(Content);
        lobby.LoadContent(Content);
        playing.LoadContent(Content);
        photoBook.LoadContent(Content);
    } // End of LoadContent method


    protected override void Update(GameTime gameTime) {

        base.Update(gameTime);                                                                              // Call this at the start of the update method to trigger the fixed update

        DeviceState.Update();                                                                               // Update the state of the input devices

        if (IsKeyPressed(Keys.P))
            E.Config.ShowDebugInfo = !E.Config.ShowDebugInfo;                                               // Toggle the debug info

#if DEBUG   // Quick exit for debugging
        // TODO: This always seems to close, figure out why!
        //IsAnyInputDown(Keys.Escape, Buttons.Back);                                                          // Check if the escape key or the back button is pressed
        //    Exit();                                                                                         // Exit the game
#endif

        // Only update the current scene
        switch (currentScene) {

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
        }

        Rumble(0, 5f, 1f);

    } // End of Update method

    protected override void FixedUpdate(GameTime gameTime) {

        base.FixedUpdate(gameTime);                                                                         // Call the base fixed update method

        // Only update the current scene
        switch (currentScene) {

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
        }

    } // End of FixedUpdate method


    protected override void Draw(GameTime gameTime) {

        GraphicsDevice.Clear(Color.Black);

        // Only draw the current scene
        switch (currentScene) {

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
        }

        base.Draw(gameTime);
        
        if(E.Config.ShowDebugInfo) {

            spriteBatch.Begin();
            spriteBatch.DrawString(Libs.Fonts.DebugFont, $"Update: {UpdateTracker.AverageEPS:0.00} / {UpdateTracker.Counter}", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(Libs.Fonts.DebugFont, $"FixedUpdates: {FixedUpdateTracker.AverageEPS:0.00} / {FixedUpdateTracker.Counter}", new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(Libs.Fonts.DebugFont, $"Frames: {DrawTracker.AverageEPS:0.00} / {DrawTracker.Counter}", new Vector2(10, 50), Color.White);
            spriteBatch.End();
        }

    } // End of Draw method
} // End of GameMain class
