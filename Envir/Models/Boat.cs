using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Boat : Player {

    Vector2 position;
    public Vector2 Position => position;                                                                    // Property to get position of boat (Readonly)

    public float rotation = 0;                                                                              // Rotation of boat

    Vector2 cycloidYOffset = Vector2.Zero;                                                                  // Offset for bobbing effect

    // TODO: Change this to something else
    // This is legacy
    public SpriteEffects spriteEffect = SpriteEffects.None;

    public Boat(int controllerIndex) : base(controllerIndex) {

        position = new(1920/2, 1080/2);                                                                     // Set spawn position to center of screen
    }

    public void Initialize(GraphicsDevice device) { }

    public void LoadContent(ContentManager content) { }

    public void Update(GameTime gameTime) {

        Bob(gameTime);                                                                                      // Calculate the bobbing effect

        // Determine the direction of the boat
        if (Velocity.X < 0)
            spriteEffect = SpriteEffects.FlipHorizontally;
        else
            spriteEffect = SpriteEffects.None;

        Velocity = Vector2.Zero;                                                                            // Reset velocity

        // Add the velocity of the players to the boat
        for (int i = 0; i < GameState.Players.Count; i++)                                                   // Loop through all players
            if (GameState.Players[i].Role == ActorType.Tourist)                                             // If the player is a tourist
                Velocity += GamePadLeftStick(GameState.Players[i].ControllerIndex);                         // Add the left stick value of the player to the velocity

        position += Velocity * 4;                                                                           // Move the boat

        position.X = Math.Clamp(position.X, -(1920/2), 1920*1.5f);                                          // Clamp the position of the boat to the screen
        position.Y = Math.Clamp(position.Y, -(1080/2), 1080*1.5f);                                          // Clamp the position of the boat to the screen
    }

    public void FixedTimestampUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

        // ! OMFG FIX THIS XD
        spriteBatch.Draw((spriteEffect == SpriteEffects.None ? TLib.Boat[0] : TLib.Boat[1]), position + cycloidYOffset, null, Color.White, rotation, new Vector2(TLib.Boat[0].Width/2, TLib.Boat[0].Height/2), 1, SpriteEffects.None, 0);
    }


    private void Bob(GameTime gameTime) {

        rotation = ((float)Math.Sin((gameTime.TotalGameTime.TotalMilliseconds) / 1000) / 10)*2;

        float bob = (float)Math.Sin((gameTime.TotalGameTime.TotalMilliseconds) / 100)*2;
        cycloidYOffset.Y = bob;
    }
}