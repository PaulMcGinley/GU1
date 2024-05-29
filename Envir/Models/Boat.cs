using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Boat : Player {

    Vector2 position = new(1920/2, 1080/2);
    float rotation = 0;
    Vector2 cycloidYOffset = Vector2.Zero;

    SpriteEffects spriteEffect = SpriteEffects.None;

    public Boat(int controllerIndex) : base(controllerIndex)
    {
    }

    public void Initialize(GraphicsDevice device) {

    }

    public void LoadContent(ContentManager content) {

    }

    public void Update(GameTime gameTime) {

        Bob(gameTime);

        // combine all the gamepad left thumbstick values from all tourists in GameScene

        if (Velocity.X < 0)
            spriteEffect = SpriteEffects.FlipHorizontally;
        else
            spriteEffect = SpriteEffects.None;

        Velocity = Vector2.Zero;

        for (int i = 0; i < GameState.Players.Count; i++)
            if (GameState.Players[i].Role == ActorType.Tourist)
                Velocity += GamePadLeftStick(GameState.Players[i].ControllerIndex);

        position += (Velocity) * 4;

        position.X = Math.Clamp(position.X, -(1920/2), 1920*1.5f);
        position.Y = Math.Clamp(position.Y, -(1080/2), 1080*1.5f);





    }

    public void FixedTimestampUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Draw(TLib.Boat, position + cycloidYOffset, null, Color.White, rotation, new Vector2(TLib.Boat.Width/2, TLib.Boat.Height/2), 1, spriteEffect, 0);
    }

        private void Bob(GameTime gameTime) {

        rotation = ((float)Math.Sin((gameTime.TotalGameTime.TotalMilliseconds) / 1000) / 10)*2;


        float bob = (float)Math.Sin((gameTime.TotalGameTime.TotalMilliseconds) / 100)*2;
        cycloidYOffset.Y = bob;
    }
}