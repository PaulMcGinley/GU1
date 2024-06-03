using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class PhotoBook : IScene {

    Random  rand = new();

    SpriteBatch spriteBatch;                                                                                // The sprite batch to render the photos

    Photo[] photos;                                                                                         // Array of photos
    Vector2[] photoLocations;                                                                               // Array of onscreen photo locations
    Rectangle[] photoBounds;                                                                                // Array of photo bounds for selection

    const int mod = 5;                                                                                      // The modulo value (number of photos per row)

    Camera2D camera;                                                                                        // The camera to render the photos
    Cursor cursor;                                                                                          // The cursor to select the photos
    Vector2 cursorWorldPos => Vector2.Transform(cursor.Position, Matrix.Invert(camera.TransformMatrix));    // Transform the cursor position to world position

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) {

        camera = new Camera2D(new Viewport(0, 0, 1920, 1080));                                              // Create a new camera
        camera.LookAt(new Vector2((1920/2) - 150, 1080/2));                                                 // Set the camera to look at the center of the screen

        cursor = new Cursor();                                                                              // Create a new cursor
    }

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        camera.Update(gameTime);                                                                            // Update the camera

        // Check for input to go back to the main menu
        if (IsAnyInputPressed(Buttons.B, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;

        // Guard clause to check if there are any photos
        if (photos == null)
            return;


        // Check for input to view a photo
        if (IsAnyInputPressed(Buttons.A))
            for (int i = 0; i < photos.Length; i++)
                if (photoBounds[i].Contains(cursorWorldPos)) {

                    PhotoViewer.Photo = photos[i];                                                          // Set the photo to view
                    GameState.CurrentScene = GameScene.PhotoViewer;                                         // Change the scene to the photo viewer
                }

        // Check for input to reset the cursor position
        if (IsAnyInputPressed(Buttons.Y))
            cursor.Position = new Vector2(1920/2,1080/2);                                                              // Set the cursor position to the camera position
    }

    public void FixedUpdate(GameTime gameTime) {

        cursor.Position += GamePadRightStick(0)*10f;                                                        // Move the cursor with the right stick

        // Lock the cursor to the screen
        cursor.Position = new Vector2(
            MathHelper.Clamp(cursor.Position.X, 0, 1920 - TLib.Cursor.Width),
            MathHelper.Clamp(cursor.Position.Y, 0, 1080 - TLib.Cursor.Height));

        // Guard clause to check if there are any photos
        if (photos == null)
            return;

        if (camera.Position.Y < 0 && GamePadLeftStick(0).Y < 0) return;                                     // Limit the camera movement on the Y axis (Top)
        if (camera.Position.Y > 420 * (photos.Length/mod) && GamePadLeftStick(0).Y > 0) return;             // Limit the camera movement on the Y axis (Bottom)

        camera.LookAt(camera.Position + new Vector2(0, GamePadLeftStickY(0)*10f));                          // Move the camera on the Y axis
    }

    public void Draw(SpriteBatch spriteBatch) {

        // Static drawing
        spriteBatch.Begin();

        // Background
        spriteBatch.Draw(TLib.mainMenuBackground, new Rectangle(0,0,1920,1080), Color.White);

        // Overlay
        spriteBatch.Draw(TLib.Pixel, new Rectangle(0,0,1920,1080), Color.Black * 0.5f);

        spriteBatch.End();

        if (photos == null)
            return;


        // Dynamic drawing
        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        // We only want to draw the photos that will be on screen
        for (int i = 0; i < photos.Length; i++) {

            Vector2 worldPos = photoLocations[i];
            Vector2 screenPos = Vector2.Transform(worldPos, camera.TransformMatrix);                        // Transform the world position to screen position

            bool selected = photoBounds[i].Contains(cursorWorldPos);                                       // Check if the cursor is over the photo

            if (screenPos.Y > -400 && screenPos.Y < 1080)                                                   // Only draw the photos that are on screen
                spriteBatch.Draw(photos[i].framedPicture, worldPos, null, selected ? Color.LightBlue : Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
                                                                   // Draw the cursor

        spriteBatch.End();

        spriteBatch.Begin();

        cursor.Draw(spriteBatch);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        LoadPhotos();
    }

    public void OnSceneEnd() {

    }

    #endregion


    /// <summary>
    /// Set the sprite batch to allow the photos to be rendered prior to being drawn
    /// </summary>
    public void Set(SpriteBatch spriteBatch) {

            this.spriteBatch = spriteBatch;                                                                 // Set the sprite batch
    }

    public void LoadPhotos() {

        string directoryPath = Photo.SaveDir;                                                               // The directory path to the photos
        var regex = new Regex(@"^-\d+\.xml$");                                                              // Regex to match the file names ( -[number].xml)

        // Get all the files in the directory that match the regex pattern
        string[] files = Directory.EnumerateFiles(directoryPath, "*.xml")
                                .Where(path => regex.IsMatch(Path.GetFileName(path)))                       // Filter the files
                                .Select(path => Path.GetFileNameWithoutExtension(path))                     // Get the file names
                                .ToArray();                                                                 // Convert to an array

        photos = new Photo[files.Length];                                                                   // Create a new array of photos with the length of the file count
        for (int i = 0; i < files.Length; i++) {

            photos[i] = new Photo();
            photos[i] = photos[i].Load(files[i]);
            photos[i].Render(spriteBatch);
        }

        // Sort the photos by the date they were taken
        photos = photos.OrderByDescending(photo => photo.timeStamp).ToArray();

        photoLocations = new Vector2[files.Length];                                                         // Create a new array of photo locations with the length of the file count

        // Using modulo to create a grid of photos
        for (int i = 0; i < files.Length; i++)
            photoLocations[i] = new Vector2(320 * (i % mod), 420 * (i / mod));                              // Set the photo location based on the modulo value

        photoBounds = new Rectangle[files.Length];                                                          // Create a new array of photo bounds with the length of the file count
        for (int i = 0; i < files.Length; i++)
            photoBounds[i] = new Rectangle((int)photoLocations[i].X, (int)photoLocations[i].Y, 320, 420);   // Set the photo bounds based on the photo location

        GC.Collect();
    }

    public void Dispose() {

        // TODO: Dispose of the photos to free up memory
        foreach (var photo in photos)
            photo.Dispose();

            GC.Collect();
    }
}
