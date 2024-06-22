using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Credits : IScene {

    Random random = new();                                                                                  // Random number generator

    Camera2D camera;                                                                                        // Orthographic camera for the scene

    Viewport viewport;                                                                                      // The viewport of the scene
    Vector2 screenDimensions => new(viewport.Width, viewport.Height);                                       // The dimensions of the screen from the viewport

    bool photoLoaded = false;                                                                               // If the photo has been loaded
    Photo[] photos;                                                                                         // The photos to display
    string[] photoNames;                                                                                    // The names of the photos
    Vector2[] photoPositions = new Vector2[6] {
        new (150, 200),
        new (150, 500),
        new (150, 800),
        new ((1920)-150, 250),
        new ((1920)-150, 550),
        new ((1920)-150, 850)
    };
    float[] photoRotations = new float[6] { 0.18f, -0.22f, 0.21f, -0.17f, 0.15f, -0.22f };

    const int lineSpacing = 55;                                                                             // The space between each line of text
    readonly string[,] credits = new string[,] {                                                            // The list of credits, job and name

        //JOB                           NAME
        { "Concept",                    "Corey Connolly"    },
        { "Design",                     "Paul McGinley"     },
        { "Inspiration",                "Hidden in plain sight" },
        { "Project Manager",            "Kieran Bett"       },
        { "Lead Programmer",            "Paul McGinely"     },
        { "Secondary Programer" ,       "Alexander Tuffy"   },
        { "Sound Effects by",           "Albert Bugheanu"   },
        { "Sourced Audio from",         "Pixabay.com"       },
        { "Game Play Music by",         "Nesrality"         },
        { "Lead Artist",                "Alexander Tuffy"   },
        { "Secondary Artist",           "Corey Connolly"    },
        { "Graphic touchup",            "Paul McGinley"     },
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

        if (!photoLoaded) {

            LoadPhotos(spriteBatch);                                                                        // Load the photos
            photoLoaded = true;                                                                             // Set the photo loaded flag
        }

        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        // if (photos.Length > 0)                                                                              // If there are photos
        //     //spriteBatch.Draw(photos[0].framedPicture, new Vector2(leftX, startY ), Color.White);                // Draw the photo
        //     spriteBatch.Draw(photos[0].framedPicture, new Vector2(leftX, startY), null, Color.White, rot, new Vector2(photos[0].framedPicture.Width/2, 15), 1f, SpriteEffects.None, 0f);

        for (int i = 0; i < photos.Length; i++) {

            int ran = random.Next(photos.Length-1);                                                            // Get a random photo
            spriteBatch.Draw(photos[i].framedPicture, photoPositions[i], null, Color.White, photoRotations[i], new Vector2(photos[ran].framedPicture.Width/2, 15), 1f, SpriteEffects.None, 0f);
        }

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

    public void OnSceneEnd() {

        photoLoaded = false;                                                                                // Reset the photo loaded flag
    }

    #endregion


    public void LoadPhotos(SpriteBatch spriteBatch) {

        string directoryPath = Photo.SaveDir;                                                               // The directory path to the photos
        var regex = new Regex(@"^-\d+\.xml$");                                                              // Regex to match the file names ( -[number].xml)

        // Get all the files in the directory that match the regex pattern
        photoNames = Directory.EnumerateFiles(Photo.SaveDir, "*.xml")
                              .Where(path => regex.IsMatch(Path.GetFileName(path)))                         // Filter the files
                              .Select(path => Path.GetFileNameWithoutExtension(path))                       // Get the file names
                              .ToArray();                                                                   // Convert to an array

        int photosToLoad = photoNames.Length < 6 ? photoNames.Length : 6;                                   // The number of photos to load

        photos = new Photo[photosToLoad];                                                                    // Create a new array of photos

        for (int i = 0; i < photosToLoad; i++) {

            photos[i] = new Photo();                                                                        // Create a new photo
            photos[i] = photos[i].Load(photoNames[i]);                                                      // Load the photo
            photos[i].Render(spriteBatch);                                                                  // Render the full picture
        }
    }
}
