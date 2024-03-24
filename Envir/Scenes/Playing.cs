using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Scenes;

public class Playing : IScene {

    private Graphic2D background;                                                                           // The background image or 'map' of the game

    public void Initialize(GraphicsDevice device) {
        
        background = new Graphic2D(TLib.Background, new Vector2(1920/2, 1080/2));                           // Create a new 2D graphic object for the background image
    }

    public void LoadContent(ContentManager content) {

    }

    public void UnloadContent() {
        
    }

    /// <summary>
    /// Uncapped update method, run every frame and used for checking collisions
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime) {
        
    }
    
    /// <summary>
    /// Fixed update method runs at a fixed rate and time limited, is used for non critical game logic
    /// </summary>
    /// <param name="gameTime"></param>
    public void FixedUpdate(GameTime gameTime) {
        
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        background.Draw(spriteBatch);

        spriteBatch.End();
    }
}
