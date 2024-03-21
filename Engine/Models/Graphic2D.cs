using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1;

/// <summary>
/// Graphic2D is a structure that represents a 2D graphic which is not animated and does not have any physics
/// This is used for static images in the game world such as the background or UI elements
/// </summary>
public struct Graphic2D {

    /// <summary>
    /// The texture of the graphic
    /// </summary>
    readonly Texture2D texture;

    /// <summary>
    /// The position of the graphic in the game world
    /// </summary>
    Vector2 position;

    /// <summary>
    /// The origin point of the graphic
    /// This is the center of the texture
    /// </summary>
    readonly Vector2 Origin => new(texture.Width / 2, texture.Height / 2);

    /// <summary>
    /// Create a new 2D graphic
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="position"></param>
    public Graphic2D(Texture2D texture, Vector2 position) {
        
        this.texture = texture;
        this.position = position;
    }

    public readonly void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Draw(texture, position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
    }
}
