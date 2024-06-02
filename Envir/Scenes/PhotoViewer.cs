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

        spriteBatch.End();
    }

    public void OnSceneStart() {}

    public void OnSceneEnd() {}

    public void SetPhoto(Photo photo) {

        Photo = photo;
    }
}
