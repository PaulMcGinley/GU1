using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Scenes;

public class PhotoBook : IScene {

    Photo photo;
    SpriteBatch spriteBatch;

    public void Set(SpriteBatch spriteBatch) {

            this.spriteBatch = spriteBatch;
    }

    public void Initialize(GraphicsDevice device) {

        photo = new Photo();
        photo = photo.Load("-8584844245441848118");

        photo.Render(spriteBatch);
    }

    public void LoadContent(ContentManager content) {

    }

    public void UnloadContent() {

    }

    public void Update(GameTime gameTime) {

    }

    public void FixedUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.Draw(TLib.Pixel , new Rectangle((1920/2)-150, (1080/2) - 150, 300, 350), Color.White); // TODO: Make the photo return a polaroid or just the picture
        spriteBatch.Draw(photo.fullPicture, new Vector2(1920/2,1080/2), new Rectangle((int)photo.location.X -128, (int)photo.location.Y -128, 256, 256), Color.White, 0, new Vector2(128, 128), 1, SpriteEffects.None, 0);

        spriteBatch.Draw(photo.framedPicture, new Vector2(10,10), Color.White);



        spriteBatch.End();
    }

    public void OnSceneStart() {

    }

    public void OnSceneEnd() {

    }
}
