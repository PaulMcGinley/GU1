using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1;

public interface IGameObject {

    public void Initialize(GraphicsDevice device);
    public void LoadContent(ContentManager content);
    public void Update(GameTime gameTime);
    public void FixedTimestampUpdate(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch);
}
