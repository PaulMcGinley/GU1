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
    readonly string[,] credits = new string[,] {                                                            // The list of credits, job and name

        //JOB                           NAME
        { "Game Concept / Design",      "Corey Connolly" },
        { "Project Manager",            "Kieran Bett" },
        { "Lead Programmer",            "Paul Mcginely" },
        { "Additional Programer" ,      "Alexander Tuffy" },
        { "Audio Creator",              "Albert Bugheanu" },
        { "Floatsam Art designer",      "Alexander Tuffy" },
        { "Nessie Art Designer",        "Corey Connolly" },
    };

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) {

        viewport = device.Viewport;
        camera = new Camera2D(viewport);
    }

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        // Check for input to go back to the main menu
        if (IsAnyInputPressed(Keys.B, Buttons.B, Buttons.Start, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;

    }

    public void FixedUpdate(GameTime gameTime) {

        if (camera.Position.Y > credits.GetLength(0) * lineSpacing + 250)                                   // If the camera has scrolled past the credits
            return;                                                                                         // Return

        // Move camera
        camera.LookAt(new Vector2(camera.Boundaries.Width/2, camera.Position.Y + 1f));                      // Move the camera to scroll the credits
        camera.Update(gameTime);                                                                            // Update the camera
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        // Draw the title
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Sightings", yPosition: 100f, screenDimensions, Color.White);

        // Draw the credits
        for (int i = 0; i < credits.GetLength(0); i++)

            DrawTextCredits(spriteBatch,                                                                    // SpriteBatch
                            FLib.LeaderboardFont,                                                                 // Font
                            credits[i, 0],                                                                  // What text
                            credits[i, 1],                                                                  // Who text
                            yPosition: 200f + (i * lineSpacing),                                            // Y position
                            screenDimensions,                                                               // Screen dimensions
                            Color.White);                                                                   // Colour

        DrawTextCenteredScreen(spriteBatch, FLib.LeaderboardFont, "Press BACK to exit", yPosition: (credits.GetLength(0)*lineSpacing)+250, screenDimensions, Color.White);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        // Reset the view
        camera.LookAt(new Vector2(viewport.Width/2, -(1080/2) + 100));

        // TODO: Play the credits music

        SLib.Whistle.Play(GameState.SFXVolume, 0, 0);
    }

    public void OnSceneEnd() { }

    #endregion

}
