using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1;

public class Actor : IMove, IGameObject {

    #region Properties

    virtual public ActorType Type { get; set; } = ActorType.AI;

    #endregion

    #region IMove Interface

    public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Friction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MaxSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MinSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Move(GameTime gameTime) { }

    #endregion

    #region IGameObject Interface

    public void Initialize(GraphicsDevice device) { }
    public void LoadContent(ContentManager content) { }
    public void Update(GameTime gameTime) { }
    public void FixedTimestampUpdate(GameTime gameTime) { }
    public void Draw(SpriteBatch spriteBatch) { }

    #endregion

}
