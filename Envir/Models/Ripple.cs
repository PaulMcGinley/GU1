using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Ripple {

    public Vector2 Position;
    public float Radius;
    public double ExpireTime;
    public float Opacity = 0.3f;

    public bool Expired => Opacity <= 0.1f;

    public Ripple(Vector2 position, float radius, double expireTime) {

        Position = position;
        Radius = radius;
        ExpireTime = expireTime;
    }

    public void Update(GameTime gameTime) {

        Radius+=5;


        Opacity -= 0.01f;
        if (Opacity <= 0.05f)
            Opacity = 0;

    }

    public void Draw(SpriteBatch spriteBatch){

        if (Expired)
            return;

        DrawCircle(Position, Radius, spriteBatch, (Color.White * (Opacity/2)));
    }
}
