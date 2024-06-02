using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class PhotoViewer : IScene {

    public static Photo Photo;

    public void Initialize(GraphicsDevice device) {}

    public void LoadContent(ContentManager content) {}

    public void UnloadContent() {}

    public void Update(GameTime gameTime) {

        if (IsGamePadButtonPressed(0, Buttons.B))
            GameState.CurrentScene = GameScene.PhotoBook;
    }

    public void FixedUpdate(GameTime gameTime) {}

    public void Draw(SpriteBatch spriteBatch) {

        if (Photo == null)
            return;

        spriteBatch.Begin();

        spriteBatch.Draw(Photo.fullPicture, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);

        if (IsGamePadButtonDown(0, Buttons.Y)) {

            Vector2 location = new Vector2(1920, 1080) + new Vector2(Photo.location.X, Photo.location.Y);
            Vector2 scale = new Vector2(0.5f, 0.5f); // scale factor for 4K to 1080p
            Vector2 location1080p = Vector2.Multiply(location, scale);

            spriteBatch.Draw(Photo.framedPicture, location1080p - new Vector2(1920/4,1080/4) - new Vector2(64+12.5f,64+12.5f), null, Color.White*0.5f, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
        }

        spriteBatch.End();
    }

    public void OnSceneStart() {}

    public void OnSceneEnd() {}

    public void SetPhoto(Photo photo) {

        Photo = photo;
    }
}
