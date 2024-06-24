using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class PhotoViewer : IScene {

    public static Photo Photo;                                                                              // The photo to view

    Vector2 location => new Vector2(1920, 1080) + new Vector2(Photo.location.X, Photo.location.Y);          // Location of the photo
    Vector2 correctLocation => Vector2.Multiply(location, 0.5f) - new Vector2(1920/4, 1080/4) - new Vector2(64 + 12.5f, 64 + 12.5f);    // Corrected location of the photo from 4K to 1080p

    bool displayPressYMessage = true;                                                                       // Display the press Y message

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) { }                                                       // Not Implemented

    public void LoadContent(ContentManager content) { }                                                     // Not Implemented

    public void UnloadContent() { }                                                                         // Not Implemented

    public void Update(GameTime gameTime) {

        // Check for input to go back to the photo book
        if (IsAnyInputPressed(Keys.B, Buttons.B, Buttons.Back))
            GameState.CurrentScene = GameScene.PhotoBook;

        if (IsAnyInputDown(Keys.Y, Buttons.Y))
            displayPressYMessage = false;
    }

    public void FixedUpdate(GameTime gameTime) { }                                                          // Not Implemented

    public void Draw(SpriteBatch spriteBatch) {

        // Guard clause to check if there is a photo
        // if (Photo == null)
        //     return;

        // Render the full picture if it is not rendered yet
        // if (Photo.fullPicture == null)
             Photo.RenderFullPicture(spriteBatch);


        spriteBatch.Begin();

        // Background (Full Picture)
        spriteBatch.Draw(Photo.fullPicture, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);

        // Framed Picture
        if (IsAnyInputDown(Keys.Y, Buttons.Y))
            spriteBatch.Draw(Photo.framedPicture, correctLocation , null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);

        if (displayPressYMessage)
            spriteBatch.DrawString(FLib.LeaderboardFont, "Press (Y) to view framed picture.", new Vector2(15, 15), Color.White * 0.75f);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        displayPressYMessage = true;                                                                         // Display the press Y message
    }

    public void OnSceneEnd() {

        Photo.Dispose();                                                                                    // Dispose of the photo
    }

    #endregion


    public void SetPhoto(ref Photo photo) {

        Photo = photo;
    }
}
