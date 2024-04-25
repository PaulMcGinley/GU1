using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Models;

public class Camera2D {

    readonly Random random = new ();

    /// <summary>
    /// The transformation matrix of the camera object
    /// </summary>
    public Matrix TransformMatrix { get; private set; }

    /// <summary>
    /// The game world position of the camera
    /// </summary>
    public Vector2 Position { get; private set; }

    /// <summary>
    /// The rotation of the camera
    /// </summary>
    public float Rotation { get; private set; }

    /// <summary>
    /// The zoom level of the camera
    /// 1+
    /// </summary>
    public float Zoom { get; private set; }

    /// <summary>
    /// The dimensions of the camera's view and its position
    /// </summary>
    public Viewport Boundaries { get; private set; }

    public Camera2D(Viewport viewport) {

        Boundaries = viewport;
        Position = Vector2.Zero;
        Rotation = 0f;
        Zoom = 1f;
        TransformMatrix = Matrix.Identity;
    }

    public void Update(GameTime gameTime) {

        TransformMatrix = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                          Matrix.CreateRotationZ(Rotation) *
                          Matrix.CreateScale(Zoom) *
                          Matrix.CreateTranslation(new Vector3(Boundaries.Width * 0.5f, Boundaries.Height * 0.5f, 0));

        #region Camera Shake - Update

        if (shakeTimer > 0) {                                                                               // There is still shake time left
            // Create a random offset
            // TODO: The random doubles should be changed to the extension method RandomFloat(0, 0.5f) in the future
            Vector2 shakeOffset = new(random.Float(0.5f) * 2 * shakeIntensity,              // X
                                      random.Float(0.5f) * 2 * shakeIntensity);             // Y

            // Apply the offset to the camera's position
            TransformMatrix *= Matrix.CreateTranslation(shakeOffset.ToVector3());                           // Multiply the current matrix by a translation matrix (shakeOffset) NOTE: Never adjust the Position directly as it will mess up the camera's matrix

            // Reduce the shake timer
            shakeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;                                     // Reduce the shake timer by the elapsed time since the last update
        }

        #endregion
    }

    #region Camera Shake

    private float shakeTimer = 0f;
    private float shakeIntensity = 0f;
    public void Shake(float intensity, float duration) {

        shakeIntensity = intensity;
        shakeTimer = duration;
    }

    #endregion

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
