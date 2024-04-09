﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Models;

public class Flotsam : IMove {

    public bool PlayerControlled = false;
    public int PlayerIndex = -1;

    public Sprite2D sprite;

    public Vector2 Position { get => sprite.Position; set => sprite.Position = value; }

    public Vector2 TargetPosition { get; set; }

    private bool isAlive = true;
    private bool isCollected = false;
    private bool isFadingOut = false;

    /// <summary>
    /// This property is used to determine if the flotsam is a phantom object that will disappear after the player inspects it
    /// </summary>
    //private bool isPhantom = false; // TODO: Implement this
    private float opacity = 1.0f;

    public Flotsam(Sprite2D sprite)
    {
        this.sprite = sprite;
    }

    public Flotsam()
    {
    }

    #region IMove

    public Vector2 Velocity { get; set; }
    public float Acceleration { get; set; }
    public float Friction { get; set; }
    public float MaxSpeed { get; set; }
    public float MinSpeed { get; set; }

    public void Move(GameTime gameTime) {

        // Lerp the position of the flotsam towards the target position over a period of 1 second
        Position = Vector2.Lerp(Position, TargetPosition, 0.01f);


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

        if (!isAlive)
            return false;

        if (isCollected)
            return false;

        isCollected = true;

        // TODO: Play a sound effect

        return true;
    }

    public void Update(GameTime gameTime) {

        if (!isAlive)
            return;

        if (isFadingOut) {

            opacity -= 0.05f;

            if (opacity <= 0.0f) {

                isFadingOut = false;
                isAlive = false;
            }
        }

        // TODO: Check of the flotsam is within a certain distance of the target position
        if (Vector2.Distance(Position, TargetPosition) <= 1f)
            TargetPosition = new Vector2(RandomFloat(1920, RandomFloat(1080)));

        // TODO: Move the floatsam
            Move(gameTime);
    }

    public void FixedUpdate(GameTime gameTime) {

        if (!isAlive)
            return;
    }

    public void Draw(SpriteBatch spriteBatch) {

        if (!isAlive && isCollected && !isFadingOut)
            return;

        spriteBatch.Draw(sprite.GetTexture(), sprite.Position, sprite.GetSourceRectangle(), sprite.GetColour() * opacity, sprite.GetRotation(), sprite.GetOrigin(), sprite.GetScale(), sprite.GetEffects(), sprite.GetLayerDepth());
    }

    public void DrawShadowOverlay(SpriteBatch spriteBatch) {

        if (!isAlive && isCollected && !isFadingOut)
            return;

        spriteBatch.Draw(sprite.GetTexture(), sprite.Position, sprite.GetSourceRectangle(), (Color.Black * 0.7f ) * opacity, sprite.GetRotation(), sprite.GetOrigin(), sprite.GetScale(), sprite.GetEffects(), sprite.GetLayerDepth());
    }
}