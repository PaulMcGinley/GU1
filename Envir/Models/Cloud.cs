using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Cloud : IGameObject{

    Random random = new ();

    public Vector2 position;
    public float scale;
    public float speed;
    public byte index;
    public SpriteEffects spriteEffect;

    public Cloud(Vector2 position, float scale, float speed, byte index, SpriteEffects spriteEffect = SpriteEffects.None, bool useSigEffect = true) {
        this.position = position;
        this.scale = scale;
        this.speed = speed;
        this.index = index;

        this.spriteEffect = spriteEffect;

        if (useSigEffect) return;

        switch (random.Next(4)) {
            case 0:
                spriteEffect = SpriteEffects.None;
                break;
            case 1:
                spriteEffect = SpriteEffects.FlipHorizontally;
                break;
            case 2:
                spriteEffect = SpriteEffects.FlipVertically;
                break;
            case 3:
                spriteEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                break;
        }
    }

    public Cloud() { } // For serialization

    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void Update(GameTime gameTime) {

        position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (position.X > 1920*10)
            position.X = -1920*10;
    }

    public void FixedTimestampUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(TLib.Clouds[index], position, null, Color.Black*0.1f, 0,new Vector2( TLib.Clouds[index].Width/2, TLib.Clouds[index].Height/2) , scale, spriteEffect, 0);
}
