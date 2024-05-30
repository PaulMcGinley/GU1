// ! TODO: Refactor this class it's an absolute mess

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Models;

public class Flotsam : Actor {

    Random rand = new();
    /// <summary>
    /// This seed which with be used for starting offsets / random values so all flotsam objects are unique
    /// </summary>
    private int seed = 0;

    Vector2 RandomScreenPosition => new(rand.Next(0-(1920/2), 1920+(1920/2)), rand.Next(0-(1080/2), 1080+(1080/2)));

    public int PlayerIndex = -1;
    public bool PlayerControlled => PlayerIndex > -1;

    public Sprite2D sprite;
    Vector2 cycloidYOffset = Vector2.Zero;

    public Vector2 Position { get => sprite.Position; set => sprite.Position = value; }

    public Vector2 TargetPosition { get; set; }
    public float MoveSpeed { get; set; } = 1.0f;

    public bool isAlive = true;
    public bool isCollected = false;
    public bool isFadingOut = false;

    Color colour = Color.White;
    public Rectangle boundaryBox => new Rectangle((int)Position.X - (sprite.Width/4), (int)Position.Y - (sprite.Height/4), 64, 64);

    double nextMoveTime = 0;

    List<Ripple> ripples = new();
    double nextRippleTime = 0;

    RenderTarget2D renderTarget;
    BlendState blendState = BlendState.AlphaBlend;

    public int spriteIndex = 0;

    /// <summary>
    /// This property is used to determine if the flotsam is a phantom object that will disappear after the player inspects it
    /// </summary>
    //private bool isPhantom = false; // TODO: Implement this
    private float opacity = 1.0f;

    public Flotsam(int spriteID, Sprite2D sprite) {

        this.spriteIndex = spriteID;
        this.sprite = sprite;

        Construction();
    }

    public Flotsam() {

        Construction();
    }

    public void Construction() {

        TargetPosition = RandomScreenPosition;
        Velocity = TargetPosition - Position;

        colour = new Color(rand.Int(0, 255), rand.Int(0, 255), rand.Int(0, 255));
        MoveSpeed = rand.Float(0.5f, 2.0f);
        seed = rand.Next();
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

        if (PlayerControlled)
            Player_Move(gameTime);
        else
            AI_Move(gameTime);


        Velocity.Normalize();

        Position += (Velocity/500) * MoveSpeed;

        if (Position.X < -(1920/2))
            Position = new(1920*1.5f, Position.Y);

        if (Position.X > 1920*1.5f)
            Position = new(-(1920/2), Position.Y);

        if (Position.Y < -(1080/2))
            Position = new(Position.X, 1080*1.5f);

        if (Position.Y > 1080*1.5f)
            Position = new(Position.X, -(1080/2));
    }

    void Player_Move(GameTime gameTime) {

        Velocity = GamePadLeftStick(PlayerIndex) * 2500  ;
    }

    void AI_Move(GameTime gameTime) {

        if (!isAlive)
            return;

        if (gameTime.TotalGameTime.TotalMilliseconds < nextMoveTime)
            return;

        // TODO: Implement a better way to move the flotsam
        if (Vector2.Distance(Position, TargetPosition) <= 10f || rand.Next(0, 500) == 0) {

            TargetPosition = RandomScreenPosition;
            Velocity = TargetPosition - Position;

            if (Velocity.X < 0)
                sprite.SetEffects(SpriteEffects.FlipHorizontally);
            else
                sprite.SetEffects(SpriteEffects.None);

            nextMoveTime = gameTime.TotalGameTime.TotalMilliseconds + rand.Next(0, 3000);
            MoveSpeed = rand.Float(0.5f, 2.0f);
        }
    }

    #endregion

    /// <summary>
    /// This method is called when a tourist moves their camera view over the floatsam
    /// </summary>
    public void Inspect() {

       // System.Diagnostics.Debug.WriteLine("Flotsam inspected pre-check");

        if (isCollected) {
            isFadingOut = true;
            System.Diagnostics.Debug.WriteLine("Flotsam inspected");
        }
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

        System.Diagnostics.Debug.WriteLine("Flotsam collected");
        isCollected = true;

        // TODO: Play a sound effect

        return true;
    }

    public void Update(GameTime gameTime) {

        if (!isAlive)
            return;

        if (isFadingOut)
            opacity -= 0.05f;

         if (opacity <= 0.0f) {

            isFadingOut = false;
            isAlive = false;
        }

        if (sprite.GetTexture() == null )
            sprite.SetTexture(TLib.Flotsam[spriteIndex]);

        Bob(gameTime);
        Move(gameTime);


        // // TODO: Optimize this or remove it
        // for (int i = ripples.Count-1; i == 0; i++)
        //     if (ripples[i].Expired)
        //         ripples.RemoveAt(i);

        // if (gameTime.TotalGameTime.TotalMilliseconds > nextRippleTime && gameTime.TotalGameTime.TotalMilliseconds > nextMoveTime) {

        //     ripples.Add(new Ripple(Position, 10, gameTime.TotalGameTime.TotalMilliseconds + 2000));
        //     nextRippleTime = gameTime.TotalGameTime.TotalMilliseconds + (200 * MoveSpeed);
        // }

        // foreach (var ripple in ripples)
        //     ripple.Update(gameTime);

        // if (isFadingOut && opacity > 0.0f)
        //     opacity -= 0.05f;
        // else {

        //     isFadingOut = false;
        //     isAlive = false;
        // }
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
        if (!isAlive)
            return;

        // ? Why is sprite not drawing itself?
        spriteBatch.Draw(TLib.Flotsam[spriteIndex], sprite.Position + cycloidYOffset, new Rectangle(0,0,128,128), (PlayerControlled ? Color.Black : sprite.GetColour()) * opacity, sprite.GetRotation(), new(64,64), sprite.GetScale(), sprite.GetEffects(), sprite.GetLayerDepth());

        // #region Boundary Box

        // Draw a box around the flotsam if it is player controlled
        if (PlayerControlled)
            DrawRectangle(new Rectangle((int)Position.X - (sprite.Width/4), (int)Position.Y - (sprite.Height/4), 64, 64), spriteBatch, colour);

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

        spriteBatch.Draw(TLib.Flotsam[spriteIndex], sprite.Position, sprite.GetSourceRectangle(), (Color.Black * 0.7f ) * opacity, sprite.GetRotation(), sprite.GetOrigin(), sprite.GetScale() * (PlayerControlled ? 2f : 1f), sprite.GetEffects(), sprite.GetLayerDepth());
    }
}
