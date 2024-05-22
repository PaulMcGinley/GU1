using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Playing : IScene {

    private Random random = new();

    private Graphic2D background;                                                                           // The background image or 'map' of the game
    private Camera2D camera;                                                                                // The camera object used to control the view of the game

    private GameState gameState;

    public void Initialize(GraphicsDevice device) {

        background = new Graphic2D(TLib.Background, new Vector2(1920/2, 1080/2));                           // Create a new 2D graphic object for the background image

        camera = new Camera2D(new Viewport(new Rectangle(0, 0, 1920, 1080)));                               // Create a new orthographic camera
        camera.LookAt(new Vector2(1920/2, 1080/2));                                                         // Set the camera to look at the center of the screen

        gameState = new GameState();                                                                        // Create a new game state object

        // Create a new flotsam object for each of the 100 flotsam objects
        for (int i = 0; i <= 100; i++) {

            gameState.Flotsam.Add(
                new Flotsam(
                    new Sprite2D(
                        TLib.Flotsam[random.Int(0, TLib.Flotsam.Length)],                                                     // Randomly select a flotsam sprite
                        random.RandomVector2(0, 1920, 0, 1080))));                                          // Randomly position the flotsam object on the screen

            // Randomly flip the sprite horizontally
            if(RandomBoolean())
               gameState.Flotsam[i].sprite.SetEffects(SpriteEffects.FlipHorizontally);
        }
    }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    /// <summary>
    /// Uncapped update method, run every frame and used for checking collisions
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime) {

        camera.Update(gameTime);

#if DEBUG
        if (IsKeyDown(Keys.S))
            camera.Shake(10, 0.5f);
#endif

    }

    /// <summary>
    /// Fixed update method runs at a fixed rate and time limited, is used for non critical game logic
    /// </summary>
    /// <param name="gameTime"></param>
    public void FixedUpdate(GameTime gameTime) {

        System.Diagnostics.Debug.WriteLine("Fixed Update");

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

        foreach (var flotsam in gameState.Flotsam) {

            flotsam.DrawRipples(spriteBatch);
          //  flotsam.Draw(spriteBatch);
        }

        spriteBatch.End();


        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        foreach (var flotsam in gameState.Flotsam)
            flotsam.Draw(spriteBatch);

        spriteBatch.End();
    }
}
