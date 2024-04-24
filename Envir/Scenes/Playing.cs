using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Scenes;

public class Playing : IScene {

    private Graphic2D background;                                                                           // The background image or 'map' of the game
    Camera2D camera;

    GameState gameState;

    public void Initialize(GraphicsDevice device) {

        background = new Graphic2D(TLib.Background, new Vector2(1920/2, 1080/2));                           // Create a new 2D graphic object for the background image
        camera = new Camera2D(new Viewport(new Rectangle(0, 0, 1920, 1080)));
        camera.LookAt(new Vector2(1920/2, 1080/2));
        gameState = new GameState();

        for (int i = 0; i < 101; i++) {

            gameState.Flotsam.Add(
                new Models.Flotsam(
                    new Sprite2D(
                        TLib.Flotsam[RandomInteger(0, 7)],
                        RandomVector2(100, 1820, 100, 980))));

            if(RandomBoolean())
                gameState.Flotsam[i].sprite.SetEffects(SpriteEffects.FlipHorizontally);
        }
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

        camera.Update();

      //  foreach (var flotsam in gameState.Flotsam)
      //      flotsam.Update(gameTime);
    }

    /// <summary>
    /// Fixed update method runs at a fixed rate and time limited, is used for non critical game logic
    /// </summary>
    /// <param name="gameTime"></param>
    public void FixedUpdate(GameTime gameTime) {

        foreach (var flotsam in gameState.Flotsam)
            flotsam.Update(gameTime);

        // Sort the flotsam by their X and Y positions to ensure they are drawn in the correct order
        gameState.Flotsam = gameState.Flotsam
                                .OrderBy(flotsam => flotsam.Position.Y)
                                .ThenBy(flotsam => flotsam.Position.X).ToList();
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        background.Draw(spriteBatch);

        foreach (var flotsam in gameState.Flotsam)
            flotsam.Draw(spriteBatch);

        spriteBatch.End();
    }
}
