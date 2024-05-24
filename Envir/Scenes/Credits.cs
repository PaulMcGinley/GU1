using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Credits : IScene {

    Camera2D camera;                                                                                        // Orthographic camera for the scene

    Viewport viewport;                                                                                      // The viewport of the scene
    Vector2 screenDimensions => new(viewport.Width, viewport.Height);                                       // The dimensions of the screen from the viewport

    const int lineSpacing = 50;                                                                             // The space between each line of text
    string[,] credits = new string[,] {

        { "Game Concept / Design",      "Corey Connolly" },
        { "Project Manager",            "Kieran Bett" },
        { "Lead Programmer",            "..." },
        { "Additional Programers" ,     "..." },
        // ...
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
        camera.LookAt(new Vector2(camera.Boundaries.Width/2, camera.Position.Y + 1f));                    // Move the camera to scroll the credits
    }

    public void Draw(SpriteBatch spriteBatch) {

        // There is no transform matrix for the background, it will always be drawn at the same position
        spriteBatch.Begin();

        // TODO: Draw the background

        spriteBatch.End();

        // We use the cameras transform matrix to draw the scene to move the credits based on the camera position
        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        // Draw the title
        DrawTextCenteredScreen(spriteBatch, FLib.DebugFont, "Sightings", 100f, screenDimensions, Color.White);

        // Draw the credits
        for (int i = 0; i < credits.GetLength(0); i++)
            DrawTextCredits(spriteBatch,                                                                    // SpriteBatch
                            FLib.DebugFont,                                                                 // Font
                            credits[i, 0],                                                                  // What text
                            credits[i, 1],                                                                  // Who text
                            200f + (i * lineSpacing),                                                       // Y position
                            screenDimensions,                                                               // Screen dimensions
                            Color.White);                                                                   // Colour


        // TODO: Draw random images around the scene to make it more interesting
        // Was thinking of like a sticker bomb effect or maybe the pictures taken in the game?
        // The images should be drawn at pseudo-random positions around the screen and because the camera is moving, we do not need to update the positions of the images =]

        spriteBatch.End();
    }

    public void OnSceneStart() {

        // Reset the view
        camera.LookAt(new Vector2(viewport.Width/2, 0));

        // TODO: Play the credits music
    }

    public void OnSceneEnd() { }

}
