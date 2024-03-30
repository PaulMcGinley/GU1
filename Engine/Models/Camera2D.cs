using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Models;

public class Camera2D {

    public Matrix TransformMatrix { get; private set; }
    public Vector2 Position { get; private set; }
    public float Rotation { get; private set; }
    public float Zoom { get; private set; }
    public Viewport Boundries { get; private set; }

    public Camera2D(Viewport viewport) {

        Position = Vector2.Zero;
        Rotation = 0f;
        Zoom = 1f;
        TransformMatrix = Matrix.Identity;
    }

    public void Update() {

        TransformMatrix = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                          Matrix.CreateRotationZ(Rotation) *
                          Matrix.CreateScale(Zoom) *
                          Matrix.CreateTranslation(new Vector3(Boundries.Width * 0.5f, Boundries.Height * 0.5f, 0));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    public void LookAt(Vector2 position) => Position = position;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    public void Move(Vector2 amount) => Position += amount;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="angle"></param>
    public void Rotate(float angle) => Rotation += angle;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="angle"></param>
    public void SetRotation(float angle) => Rotation = angle;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    public void ZoomIn(float amount) => Zoom += amount;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    public void ZoomOut(float amount) => Zoom -= amount;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    public void SetZoomLevel(float amount) => Zoom = amount;
    
}
