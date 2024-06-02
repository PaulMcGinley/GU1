using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1;

public class Cursor : IGameObject {

    public Vector2 Position = Vector2.One;
    public Rectangle Bounds => new ((int)Position.X, (int)Position.Y, 32, 32);

    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void Update(GameTime gameTime) { }

    public void FixedTimestampUpdate(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Draw(TLib.Cursor, Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
    }
}
