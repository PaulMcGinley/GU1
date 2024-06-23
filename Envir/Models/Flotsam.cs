// ! TODO: Refactor this class it's an absolute mess

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Flotsam : Actor {

    Random rand = new();
    private int seed = 0;

    Vector2 RandomScreenPosition => new(rand.Next(0-(1920/2), 1920+(1920/2)), rand.Next(0-(1080/2), 1080+(1080/2)));

    //public int Index = -1;
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
    //BlendState blendState = BlendState.AlphaBlend;

    public int spriteIndex = 0;

    public float scale = 1.0f;

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


        switch (spriteIndex) {

            case 0:     // Tennent's Lager
            case 1:     // Tennent's Lager -45o
            case 2:     // Tennent's Lager back
                scale = 1.0f;
                break;

            case 4:     // Irn Bru -45o
            case 5:     // Irn Bru
            case 6:     // Fish
            case 9:     // Highland cow plushie
            case 12:    // Plank 2x4
            case 13:    // Branch (Gun)
            case 14:    // Branch (Sword)
            case 3:    // Barrel
            case 7:    // Box
                scale = 1.5f;
                break;


            case 8:     // Tunnux Tea Cake
            case 10:    // Nessie plushie
                scale = 2.0f;
                break;

            case 11:    // Seaweed
                scale = 2.5f;
                break;
        }
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

        if (!isAlive)
            return;

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

        Velocity = (GamePadLeftStick(PlayerIndex) + GamePadRightStick(PlayerIndex)) * 2500  ;
    }

    void AI_Move(GameTime gameTime) {

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
    public bool Inspect() {

        if (!isAlive)
            return false;

        if (isFadingOut)
            return false;

        if (isCollected) {
            isFadingOut = true;
            return true;
        }

        return false;
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

        if (isFadingOut)
            return false;

        isCollected = true;

        SLib.Collect[rand.Next(0, 2)].Play(GameState.SFXVolume, 0, 0);

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

    RenderTarget2D targetFinal;
    RenderTarget2D targetTop;
    RenderTarget2D targetBottom;

    public void renderFrame(SpriteBatch spriteBatch) {

        if (!isAlive)
            return;

        int cellSize = 128;
        Vector2 imageSize = new(128, 128);

        // set render target
        targetTop = new RenderTarget2D(spriteBatch.GraphicsDevice, cellSize, cellSize);
        targetBottom = new RenderTarget2D(spriteBatch.GraphicsDevice, cellSize, cellSize);
        targetFinal = new RenderTarget2D(spriteBatch.GraphicsDevice, cellSize, cellSize);

        spriteBatch.GraphicsDevice.SetRenderTarget(targetTop);
        spriteBatch.GraphicsDevice.Clear(Color.Transparent);
        spriteBatch.Begin();
        spriteBatch.Draw(TLib.Flotsam[spriteIndex], cycloidYOffset + new Vector2(cellSize/2,cellSize/2), null, Color.White,  sprite.GetRotation(), imageSize/2, 1f, sprite.GetEffects(), 0);
        spriteBatch.End();


        spriteBatch.GraphicsDevice.SetRenderTarget(targetBottom);
        spriteBatch.GraphicsDevice.Clear(Color.Transparent);
        spriteBatch.Begin();
        spriteBatch.Draw(TLib.Flotsam[spriteIndex], cycloidYOffset+ new Vector2(cellSize/2,cellSize/2), null, Color.White*0.25f,  sprite.GetRotation(), imageSize/2, 1f, sprite.GetEffects(), 0);
        spriteBatch.End();


        spriteBatch.GraphicsDevice.SetRenderTarget(targetFinal);
        spriteBatch.GraphicsDevice.Clear(Color.Transparent);
        spriteBatch.Begin();
        spriteBatch.Draw(targetBottom, Vector2.Zero, null, Color.White, 0f, new(0,0), 1f, SpriteEffects.None, 0);
        spriteBatch.Draw(targetTop,  Vector2.Zero, new Rectangle(0,0, cellSize,cellSize/8*5), Color.White, 0f, new(0,0), 1f, SpriteEffects.None, 0);
        spriteBatch.End();

    }

    public void Dispose() {

        targetBottom?.Dispose();
        targetTop?.Dispose();
        targetFinal?.Dispose();
    }

    public void Draw(SpriteBatch spriteBatch) {

        if (!isAlive)       // Don't draw if the flotsam is not alive
            return;

        if (GameState.EnableSubmersionEffect)
            spriteBatch.Draw(targetFinal, sprite.Position, new Rectangle(0,0,128,128),  Color.White * opacity, 0f, new(64,64), scale, sprite.GetEffects(), sprite.GetLayerDepth());
        else
            spriteBatch.Draw(TLib.Flotsam[spriteIndex], sprite.Position + cycloidYOffset, new Rectangle(0,0,128,128),  Color.White * opacity, sprite.GetRotation(), new(64,64), scale, sprite.GetEffects(), sprite.GetLayerDepth());
    }

    public void DrawRipples(SpriteBatch spriteBatch) {

        foreach (var ripple in ripples)
            ripple.Draw(spriteBatch);
    }
}
