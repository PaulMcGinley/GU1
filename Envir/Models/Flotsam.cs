// ! TODO: Refactor this class it's an absolute mess

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Flotsam : Actor {

    public Random random = new();
    int seed = 0;

    public bool PlayerControlled = false;
    public int PlayerIndex = -1;

    public Sprite2D sprite;
    Vector2 cycloidYOffset = Vector2.Zero;

    public Vector2 Position { get => sprite.Position; set => sprite.Position = value; }

    public Vector2 TargetPosition { get; set; }
    public float MoveSpeed { get; set; } = 1.0f;

    private bool isAlive = true;
    private bool isCollected = false;
    private bool isFadingOut = false;

    Color colour = Color.White;

    double nextMoveTime = 0;

    List<Ripple> ripples = new();
    double nextRippleTime = 0;

    /// <summary>
    /// This property is used to determine if the flotsam is a phantom object that will disappear after the player inspects it
    /// </summary>
    //private bool isPhantom = false; // TODO: Implement this
    private float opacity = 1.0f;

    public Flotsam(Sprite2D sprite) {

        this.sprite = sprite;
        TargetPosition = random.RandomVector2(0, 1920, 0, 1080);
        Velocity = TargetPosition - Position;

        colour = new Color(random.Int(0, 255), random.Int(0, 255), random.Int(0, 255));
        MoveSpeed = random.Float(0.5f, 2.0f);
        seed = random.Next();
    }

    public Flotsam() {

        TargetPosition = random.RandomVector2(0, 1920, 0, 1080);
        Velocity = TargetPosition - Position;

        colour = new Color(random.Int(0, 255), random.Int(0, 255), random.Int(0, 255));
        MoveSpeed = random.Float(0.5f, 2.0f);
        seed = random.Next();
    }

    #region IMove

    public Vector2 Velocity { get; set; }
    public float Acceleration { get; set; }
    public float Friction { get; set; }
    public float MaxSpeed { get; set; }
    public float MinSpeed { get; set; }

    public void Move(GameTime gameTime) {

        if (gameTime.TotalGameTime.TotalMilliseconds < nextMoveTime)
            return;

        //Position = TargetPosition;

        // if (random.Next(0, 100) == 0)
        //     TargetPosition = new Vector2(RandomFloat(1920), RandomFloat(1080));

        //System.Diagnostics.Debug.WriteLine("Moving Flotsam");
        if (Vector2.Distance(Position, TargetPosition) <= 1f || random.Next(0, 1000) == 0) {

            TargetPosition = new Vector2(RandomFloat(1920), RandomFloat(1080));
            Velocity = TargetPosition - Position;

            if (Velocity.X < 0)
                sprite.SetEffects(SpriteEffects.FlipHorizontally);
            else
                sprite.SetEffects(SpriteEffects.None);

            nextMoveTime = gameTime.TotalGameTime.TotalMilliseconds + random.Next(0, 3000);
            MoveSpeed = random.Float(0.5f, 2.0f);
        }

        // Velocity.Normalize();
        // Velocity *= 2f;

        Position += (Velocity/500) * MoveSpeed;
    }

    #endregion

    /// <summary>
    /// This method is called when a tourist moves their camera view over the floatsam
    /// </summary>
    public void Inspect() {

        if (!isAlive && isCollected && !isFadingOut)
            isFadingOut = true;
    }

    /// <summary>
    /// This method is called when Nessie collects the floatsam
    /// </summary>
    /// <returns></returns>
    public bool Collect() {

        if (PlayerControlled)
            return false;

        if (!isAlive)
            return false;

        if (isCollected)
            return false;

        isCollected = true;

        // TODO: Play a sound effect

        return true;
    }

    public void Update(GameTime gameTime) {

        for (int i = ripples.Count-1; i == 0; i++)
            if (ripples[i].Expired)
                ripples.RemoveAt(i);

        if (gameTime.TotalGameTime.TotalMilliseconds > nextRippleTime && gameTime.TotalGameTime.TotalMilliseconds > nextMoveTime) {

            ripples.Add(new Ripple(Position, 10, gameTime.TotalGameTime.TotalMilliseconds + 2000));
            nextRippleTime = gameTime.TotalGameTime.TotalMilliseconds + (200 * MoveSpeed);
        }

        foreach (var ripple in ripples)
            ripple.Update(gameTime);

        Move(gameTime);
        Bob(gameTime); // actually rotate the sprite
        Cycloid(gameTime);

        if (!isAlive)
            return;

        if (isFadingOut && opacity > 0.0f)
            opacity -= 0.05f;
        else {

            isFadingOut = false;
            isAlive = false;
        }

    }

    // TODO: Convert this back to a cycloid
    private void Cycloid(GameTime gameTime) {

        cycloidYOffset.Y = (float)Math.Sin((gameTime.TotalGameTime.TotalMilliseconds+seed) / 1000) * 10;
    }

    private void Bob(GameTime gameTime) {

        sprite.SetRotation(((float)Math.Sin((gameTime.TotalGameTime.TotalMilliseconds+seed) / 1000) / 10)*2);
    }

    public void FixedUpdate(GameTime gameTime) {

        if (!isAlive)
            return;
    }

    public void Draw(SpriteBatch spriteBatch) {

        if (!isAlive && isCollected && !isFadingOut)
            return;

        // ? Why is sprite not drawing itself?
        spriteBatch.Draw(sprite.GetTexture(), sprite.Position + cycloidYOffset, sprite.GetSourceRectangle(), sprite.GetColour() * opacity, sprite.GetRotation(), sprite.GetOrigin(), sprite.GetScale(), sprite.GetEffects(), sprite.GetLayerDepth());

        // Draw a square around the flotsam
        // DrawRectangle(new Rectangle((int)Position.X - (sprite.Width/4), (int)Position.Y - (sprite.Height/4), 64, 64), spriteBatch, colour);

        // Draw a line from the flotsam to the target position
        // DrawLine(Position, TargetPosition, spriteBatch, colour);
        // DrawLine(Position + Vector2.One, TargetPosition, spriteBatch, colour);
        // DrawLine(Position - Vector2.One, TargetPosition, spriteBatch, colour);

        // foreach (var ripple in ripples)
        //     ripple.Draw(spriteBatch);

    }

    public void DrawRipples(SpriteBatch spriteBatch) {

        foreach (var ripple in ripples)
            ripple.Draw(spriteBatch);
    }

    public void DrawShadowOverlay(SpriteBatch spriteBatch) {

        if (!isAlive && isCollected && !isFadingOut)
            return;

        spriteBatch.Draw(sprite.GetTexture(), sprite.Position, sprite.GetSourceRectangle(), (Color.Black * 0.7f ) * opacity, sprite.GetRotation(), sprite.GetOrigin(), sprite.GetScale(), sprite.GetEffects(), sprite.GetLayerDepth());
    }
}