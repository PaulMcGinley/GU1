using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Models;

public partial class Scene {

    public virtual void Initialize(GraphicsDevice device) { }
    public virtual void LoadContent(ContentManager content) { }
    public virtual void Update(GameTime gameTime) { }
    public virtual void FixedUpdate(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
}
