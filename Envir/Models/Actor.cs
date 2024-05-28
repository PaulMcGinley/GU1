using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1;

public class Actor : IMove, IGameObject {

    #region IMove Interface

    public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Friction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MaxSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MinSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public BoundingSphere bSphere { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } //this is for detection of the camera and nessie 
    //easy for when you can do 
     //if (Camera[players].bSphere.Intersects(Nessie[player].bSphere) 
                //{
                   //tourist[player].score + 1000
                //} or whatever its going to work but this allows it to be set up 
    public int score { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } // holds score counter for each tourist player

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
