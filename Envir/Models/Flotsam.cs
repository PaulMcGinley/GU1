// ! TODO: Refactor this class it's an absolute mess

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Flotsam : Actor {

    public Random random = new();
    Vector2 randomScreenPosition => new(random.Next(0-(1920/2), 1920+(1920/2)), random.Next(0-(1080/2), 1080+(1080/2)));

    /// <summary>
    /// This seed which with be used for starting offsets / random values so all flotsam objects are unique
    /// </summary>
    private int seed = 0;

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

    RenderTarget2D renderTarget;
    BlendState blendState = BlendState.AlphaBlend;

    /// <summary>
    /// This property is used to determine if the flotsam is a phantom object that will disappear after the player inspects it
    /// </summary>
    //private bool isPhantom = false; // TODO: Implement this
    private float opacity = 1.0f;

    public Flotsam(Sprite2D sprite) {

        this.sprite = sprite;

        Construction();
    }

    public Flotsam() {

        Construction();
    }

    public void Construction() {

        TargetPosition = randomScreenPosition;
        Velocity = TargetPosition - Position;

        colour = new Color(random.Int(0, 255), random.Int(0, 255), random.Int(0, 255));
        MoveSpeed = random.Float(0.5f, 2.0f);
        seed = random.Next();

    }

    public void Initialize(GraphicsDevice device) {

        renderTarget = new RenderTarget2D(device, 64, 64);
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

        // TODO: Implement a better way to move the flotsam
        if (Vector2.Distance(Position, TargetPosition) <= 10f || random.Next(0, 500) == 0) {

            TargetPosition = randomScreenPosition;
            Velocity = TargetPosition - Position;

            if (Velocity.X < 0)
                sprite.SetEffects(SpriteEffects.FlipHorizontally);
            else
                sprite.SetEffects(SpriteEffects.None);

            nextMoveTime = gameTime.TotalGameTime.TotalMilliseconds + random.Next(0, 3000);
            MoveSpeed = random.Float(0.5f, 2.0f);
        }

        Velocity.Normalize();

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

        Bob(gameTime);
        Move(gameTime);

        if (!isAlive)
            return;

        // TODO: Optimize this or remove it
        for (int i = ripples.Count-1; i == 0; i++)
            if (ripples[i].Expired)
                ripples.RemoveAt(i);

        if (gameTime.TotalGameTime.TotalMilliseconds > nextRippleTime && gameTime.TotalGameTime.TotalMilliseconds > nextMoveTime) {

            ripples.Add(new Ripple(Position, 10, gameTime.TotalGameTime.TotalMilliseconds + 2000));
            nextRippleTime = gameTime.TotalGameTime.TotalMilliseconds + (200 * MoveSpeed);
        }

        foreach (var ripple in ripples)
            ripple.Update(gameTime);

        if (isFadingOut && opacity > 0.0f)
            opacity -= 0.05f;
        else {

            isFadingOut = false;
            isAlive = false;
        }
    }

    /// <summary>
    /// This method is used to make the flotsam bob up and down with a slight rotation
    /// </summary>
    /// <param name="gameTime"></param>
    private void Bob(GameTime gameTime) {

        float rotation = ((float)Math.Sin((gameTime.TotalGameTime.TotalMilliseconds+seed) / 1000) / 10)*4;

        sprite.SetRotation(rotation);

        float bob = (float)Math.Sin((gameTime.TotalGameTime.TotalMilliseconds+seed) / 100)*2;
        cycloidYOffset.Y = bob;
    }

    public void FixedUpdate(GameTime gameTime) {

        if (!isAlive)
            return;
    }

    public void Draw(SpriteBatch spriteBatch) {

        // Guardian clause: if the flotsam is not alive, has been collected and is not fading out then don't draw it
        if (!isAlive && isCollected && !isFadingOut)
            return;

        // ? Why is sprite not drawing itself?
        spriteBatch.Draw(sprite.GetTexture(), sprite.Position + cycloidYOffset, sprite.GetSourceRectangle(), sprite.GetColour() * opacity, sprite.GetRotation(), sprite.GetOrigin(), sprite.GetScale(), sprite.GetEffects(), sprite.GetLayerDepth());

        // #region Boundary Box

        // DrawRectangle(new Rectangle((int)Position.X - (sprite.Width/4), (int)Position.Y - (sprite.Height/4), 64, 64), spriteBatch, colour);

        // #endregion

        // #region Travel Path

        // // Draw lines from the flotsam to the target position
        // //DrawLine(Position, TargetPosition, spriteBatch, colour);
        // DrawLine(Position + new Vector2(32,32), TargetPosition, spriteBatch, colour);
        // DrawLine(Position - new Vector2(32,32), TargetPosition, spriteBatch, colour);
        // DrawLine(Position + new Vector2(32,-32), TargetPosition, spriteBatch, colour);
        // DrawLine(Position - new Vector2(32,-32), TargetPosition, spriteBatch, colour);

        // #endregion

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