using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Interfaces;

public interface IScene {

    /// <summary>
    /// Setup the scene
    /// </summary>
    /// <param name="device"></param>
    public void Initialize(GraphicsDevice device);

    /// <summary>
    /// Load the content for the scene
    /// </summary>
    /// <param name="content"></param>
    public void LoadContent(ContentManager content);

    /// <summary>
    /// Unload the content for the scene, save to local storage if needed
    /// </summary>
    public void UnloadContent();

    /// <summary>
    /// Uncapped update method, run every frame and used for checking collisions and other critical game logic
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime);

    /// <summary>
    /// Fixed update method runs at a fixed rate and time limited, is used for non critical game logic
    /// </summary>
    /// <param name="gameTime"></param>
    public void FixedUpdate(GameTime gameTime);

    /// <summary>
    /// Draw the scene
    /// </summary>
    /// <param name="spriteBatch"></param>
    public void Draw(SpriteBatch spriteBatch);

    /// <summary>
    /// Called when the scene is started
    /// </summary>
    public void OnSceneStart();

    /// <summary>
    /// Called when the scene is ended
    /// </summary>
    public void OnSceneEnd();
}
