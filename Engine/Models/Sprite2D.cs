using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Models;

public struct Sprite2D {

    #region Texture

    private Texture2D Texture;

    /// <summary>
    /// Returns the size of the sprite.
    /// </summary>
    public readonly Vector2 Size => new(Texture.Width, Texture.Height);

    /// <summary>
    /// Returns the width and height of the sprite.
    /// </summary>
    public readonly int Width => Texture.Width;

    /// <summary>
    /// Returns the height of the sprite.
    /// </summary>
    public readonly int Height => Texture.Height;

    #endregion


    #region Position

    private Vector2 Position;

    /// <summary>
    /// Returns the left bound of the sprite.
    /// </summary>
    public readonly float LeftBound => Position.X - Origin.X;

    /// <summary>
    /// Returns the right bound of the sprite.
    /// </summary>
    public readonly float RightBound => Position.X + Origin.X;

    /// <summary>
    /// Returns the top bound of the sprite.
    /// </summary>
    public readonly float TopBound => Position.Y - Origin.Y;

    /// <summary>
    /// Returns the bottom bound of the sprite.
    /// </summary>
    public readonly float BottomBound => Position.Y + Origin.Y;

    /// <summary>
    /// Returns the bounds of the sprite as a rectangle.
    /// </summary>
    public readonly Rectangle Bounds => new((int)LeftBound, (int)TopBound, (int)(RightBound - LeftBound), (int)(BottomBound - TopBound));

    /// <summary>
    /// Returns the centre of the sprite.
    /// </summary>
    public readonly Vector2 Centre => Position;

    /// <summary>
    /// Returns the top left position of the sprite.
    /// </summary>
    public readonly Vector2 TopLeft => new(LeftBound, TopBound);

    /// <summary>
    /// Returns the top right position of the sprite.
    /// </summary>
    public readonly Vector2 TopRight => new(RightBound, TopBound);

    /// <summary>
    /// Returns the bottom left position of the sprite.
    /// </summary>
    public readonly Vector2 BottomLeft => new(LeftBound, BottomBound);

    /// <summary>
    /// Returns the bottom right position of the sprite.
    /// </summary>
    public readonly Vector2 BottomRight => new(RightBound, BottomBound);

    /// <summary>
    /// Returns a boolean indicating whether the sprite intersects with the specified rectangle.
    /// </summary>
    /// <param name="rectangle"></param>
    /// <returns></returns>
    public readonly bool Intersects(Rectangle rectangle) => Bounds.Intersects(rectangle);

    /// <summary>
    /// Returns a boolean indicating whether the sprite contains the specified point.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public readonly bool Contains(Vector2 point) => Bounds.Contains(point);

    #endregion

    private Vector2 Origin;
    private float Rotation;
    private float Scale;
    private float LayerDepth;
    private Color Colour;
    private SpriteEffects Effects;
    private Rectangle SourceRectangle;
    private Rectangle DestinationRectangle;

    #region Setters

    public void SetTexture(Texture2D texture) => Texture = texture;
    public void SetPosition(Vector2 position) => Position = position;
    public void SetOrigin(Vector2 origin) => Origin = origin;
    public void SetRotation(float rotation) => Rotation = rotation;
    public void SetScale(float scale) => Scale = scale;
    public void SetLayerDepth(float layerDepth) => LayerDepth = layerDepth;
    public void SetColour(Color colour) => Colour = colour;
    public void SetEffects(SpriteEffects effects) => Effects = effects;
    public void SetSourceRectangle(Rectangle sourceRectangle) => SourceRectangle = sourceRectangle;
    public void SetDestinationRectangle(Rectangle destinationRectangle) => DestinationRectangle = destinationRectangle;

    #endregion


    #region Getters

    public readonly Texture2D GetTexture() => Texture;
    public readonly Vector2 GetPosition() => Position;
    public readonly Vector2 GetOrigin() => Origin;
    public readonly float GetRotation() => Rotation;
    public readonly float GetScale() => Scale;
    public readonly float GetLayerDepth() => LayerDepth;
    public readonly Color GetColour() => Colour;
    public readonly SpriteEffects GetEffects() => Effects;
    public readonly Rectangle GetSourceRectangle() => SourceRectangle;
    public readonly Rectangle GetDestinationRectangle() => DestinationRectangle;

    #endregion


    #region Constructors

    /// <summary>
    ///
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="position"></param>
    public Sprite2D(Texture2D texture, Vector2 position) {

        Texture = texture;
        Position = position;
        Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        Rotation = 0;
        Scale = 1;
        LayerDepth = 0;
        Colour = Color.White;
        Effects = SpriteEffects.None;
        SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="position"></param>
    /// <param name="origin"></param>
    /// <param name="rotation"></param>
    /// <param name="scale"></param>
    /// <param name="layerDepth"></param>
    /// <param name="colour"></param>
    /// <param name="effects"></param>
    /// <param name="sourceRectangle"></param>
    /// <param name="destinationRectangle"></param>
    public Sprite2D(Texture2D texture, Vector2 position, Vector2 origin, float rotation, float scale, float layerDepth, Color colour, SpriteEffects effects, Rectangle sourceRectangle, Rectangle destinationRectangle) {

        Texture = texture;
        Position = position;
        Origin = origin;
        Rotation = rotation;
        Scale = scale;
        LayerDepth = layerDepth;
        Colour = colour;
        Effects = effects;
        SourceRectangle = sourceRectangle;
        DestinationRectangle = destinationRectangle;
    }

    #endregion


    #region Draw

    /// <summary>
    /// Draw the sprite on the screen at the specified position.
    /// </summary>
    /// <param name="spriteBatch"></param>
    public readonly void DrawToPosition(SpriteBatch spriteBatch) => spriteBatch.Draw(Texture, Position, SourceRectangle, Colour, Rotation, Origin, Scale, Effects, LayerDepth);

    /// <summary>
    /// Draw the sprite on the screen at the specified rectangle.
    /// </summary>
    /// <param name="spriteBatch"></param>
    public readonly void DrawToRectangle(SpriteBatch spriteBatch) => spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Colour, Rotation, Origin, Effects, LayerDepth);

    /// <summary>
    /// Draw the sprite.
    /// </summary>
    /// <param name="spriteBatch"></param>
    public readonly void Draw(SpriteBatch spriteBatch) => DrawToPosition(spriteBatch);

    #endregion

}
