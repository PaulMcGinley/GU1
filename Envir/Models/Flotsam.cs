using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Models;

public class Flotsam : IMove {

    public bool PlayerControlled = false;
    public int PlayerIndex = -1;

    public Sprite2D sprite;

    public Vector2 Position { get => sprite.Position; set => sprite.Position = value; }

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

    #region IMove

    public Vector2 Velocity { get; set; }
    public float Acceleration { get; set; }
    public float Friction { get; set; }
    public float MaxSpeed { get; set; }
    public float MinSpeed { get; set; }

    public void Move(Vector2 direction)
    {
        // Calculate the new velocity based on the direction and acceleration
        Vector2 newVelocity = Velocity + direction * Acceleration;

        // Apply friction to the velocity
        newVelocity *= (1 - Friction);

        // Update the position based on the velocity
        sprite.Position += newVelocity;

        // Update the velocity
        Velocity = newVelocity;
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

        // TODO: Decide if the flotsam should find a new position

        // TODO: Move the floatsam
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