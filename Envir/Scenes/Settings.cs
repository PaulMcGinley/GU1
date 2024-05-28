using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Settings : IScene {

    Camera2D camera;                                                                                        // Orthographic camera for the scene

    Viewport viewport;                                                                                      // The viewport of the scene
    Vector2 screenDimensions => new(viewport.Width, viewport.Height);                                       // The dimensions of the screen from the viewport

    const int lineSpacing = 50;                                                                             // The space between each line of text
    readonly string[,] Settings = new string[,] {
        //displaying controls
        //for Tourist
        {"Tourist Controls~"},
        { "Left Thumstick:","Move Player" },
        { "Right Thumbstick:","Move Camera" },
        { "Right trigger:","take photo" },
        { "Left Trigger:" ,"Hold Camera steady" },
        //for Nessie
        {"Nessie Controls~"},
        { "Left Thumstick:",      "Move Player" },

              
    };

    public void Initialize(GraphicsDevice device) {

        viewport = device.Viewport;
        camera = new Camera2D(viewport);
    }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (IsAnyInputDown(Buttons.Back, Keys.Space))
            GameState.CurrentScene = GameScene.MainMenu;
    }

    public void FixedUpdate(GameTime gameTime) {

        // Move camera
        camera.Update(gameTime);
        camera.LookAt(new Vector2(camera.Boundaries.Width/2, camera.Position.Y + 1f));                      // Move the camera to scroll the Settings
    }

    public void Draw(SpriteBatch spriteBatch) {

        // There is no transform matrix for the background, it will always be drawn at the same position
        spriteBatch.Begin();

        // TODO: Draw the background

        spriteBatch.End();

        // We use the cameras transform matrix to draw the scene to move the Settings based on the camera position
        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        // Draw the title
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "Sightings", yPosition: 100f, screenDimensions, Color.White);

        // Draw the Settings
        for (int i = 0; i < Settings.GetLength(0); i++)
            DrawTextSettings(spriteBatch,                                                                    // SpriteBatch
                            FLib.DebugFont,                                                                 // Font
                            Settings[i, 0],                                                                  // What text
                            Settings[i, 1],                                                                  // Who text
                            yPosition: 200f + (i * lineSpacing),                                            // Y position
                            screenDimensions,                                                               // Screen dimensions
                            Color.White);                                                                   // Colour


        // TODO: Draw random images around the scene to make it more interesting
        // Was thinking of like a sticker bomb effect or maybe the pictures taken in the game?
        // The images should be drawn at pseudo-random positions around the screen and because the camera is moving, we do not need to update the positions of the images =]
        // think this would be good to have in this menu aswell

        spriteBatch.End();
    }

    public void OnSceneStart() {

        // Reset the view
        camera.LookAt(new Vector2(viewport.Width/2, 0));

        // TODO: Play the some music
    }

    public void OnSceneEnd() { }

}
