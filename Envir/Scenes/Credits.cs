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
        { "Game Concept",               "Corey Connolly"    },
        { "Game Design",                "Paul McGinley"     },
        { "Inspiration",                "Hidden in plain sight" },
        { "Project Manager",            "Kieran Bett"       },
        { "Lead Programmer",            "Paul McGinely"     },
        { "Additional Programer" ,      "Alexander Tuffy"   },
        { "Audio by",                   "Albert Bugheanu"   },
        { "Sourced Audio from",         "Pixabay.com"       },
        { "Game Play Music",            "Nesrality"         },
        { "Lead Art Designer",          "Alexander Tuffy"   },
        { "Additional Art by",          "Corey Connolly"    },
        { "",                           "Paul McGinley"     },
        { "Sourced Art by",             "greatdocbrown (Itch.io)" },
        { "Tester 1",                   "Paul McGinley"     },
        { "Tester 2",                   "Bash"              },
        { "Tester 3",                   "Nat"               },
        { "Tester 4",                   "Alex Bolton"       },
        { "Tester 5",                   "Amy Tyrrell"       },
    };

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) {

        viewport = device.Viewport;
        camera = new Camera2D(viewport);
        camera.LookAt(new Vector2(viewport.Width/2, -(1080/2) + 100));
    }

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        camera.Update(gameTime);                                                                            // Update the camera

        // Check for input to go back to the main menu
        if (IsAnyInputPressed(Keys.B, Buttons.B, Buttons.Start, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;
    }

    public void FixedUpdate(GameTime gameTime) {

        if (camera.Position.Y > credits.GetLength(0) * lineSpacing + 250 + (1080/2))                        // If the camera has scrolled past the credits
            return;                                                                                         // Return

        // // allow user to control the camera
        // if (IsAnyInputDown(Keys.Down, Buttons.DPadDown, Buttons.LeftThumbstickDown))
        //     camera.LookAt(camera.Position + new Vector2(0, 1f));

        // if (IsAnyInputDown(Keys.Up, Buttons.DPadUp, Buttons.LeftThumbstickUp))
        //     camera.LookAt(camera.Position - new Vector2(0, 1f));

        // Move camera
        camera.LookAt(new Vector2(viewport.Width/2, camera.Position.Y + 1f));                               // Move the camera to scroll the credits
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        // Draw the title
        DrawTextCenteredScreen(spriteBatch, FLib.MainMenuFont, "Sightings", yPosition: 100f, screenDimensions, Color.White);

        // Draw the credits
        for (int i = 0; i < credits.GetLength(0); i++)

            DrawTextCredits(spriteBatch,                                                                    // SpriteBatch
                            FLib.LeaderboardFont,                                                           // Font
                            credits[i, 0],                                                                  // What text
                            credits[i, 1],                                                                  // Who text
                            yPosition: 200f + (i * lineSpacing),                                            // Y position
                            screenDimensions,                                                               // Screen dimensions
                            Color.White);                                                                   // Colour

        DrawTextCenteredScreen(spriteBatch, FLib.LeaderboardFont, "Thanks for playing!", yPosition: (credits.GetLength(0)*lineSpacing)+220 + (1080/2), screenDimensions, Color.White);
        DrawTextCenteredScreen(spriteBatch, FLib.LeaderboardFont, "Press BACK to return to the main menu", yPosition: (credits.GetLength(0)*lineSpacing)+250 + (1080/2), screenDimensions, Color.White);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        // Reset the view
        camera.LookAt(new Vector2(viewport.Width/2, -(1080/2) + 100));

        SLib.Whistle.Play(GameState.SFXVolume, 0, 0);
    }

    public void OnSceneEnd() { }

    #endregion

}
