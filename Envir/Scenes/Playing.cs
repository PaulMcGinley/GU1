using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Playing : IScene {

    private Random random = new();

    private Graphic2D background;                                                                           // The background image or 'map' of the game
    private Sprite2D background2;
    private Camera2D camera;                                                                                // The camera object used to control the view of the game

    private GameState gameState;

    Vector2 randomScreenPosition => new(random.Next(0-(1920/2), 1920+(1920/2)), random.Next(0-(1080/2), 1080+(1080/2)));

    public void Initialize(GraphicsDevice device) {

        background = new Graphic2D(TLib.Background[0], new Vector2(1920/2, 1080/2));                           // Create a new 2D graphic object for the background image
        background2 = new Sprite2D(TLib.Background[1], new Vector2(1920/2, 1080/2));
        background2.SetEffects(SpriteEffects.FlipHorizontally);

        camera = new Camera2D(new Viewport(new Rectangle(0, 0, 1920, 1080)));                               // Create a new orthographic camera
        camera.LookAt(new Vector2(1920/2, 1080/2));                                                         // Set the camera to look at the center of the screen
        camera.SetZoomLevel(0.5f);                                                                                 // Set the camera zoom level

        gameState = new GameState();                                                                        // Create a new game state object

        // Create a new flotsam object for each of the 100 flotsam objects
        for (int i = 0; i <= 100; i++) {
            int idx = random.Int(0, TLib.Flotsam.Length);
            gameState.Flotsam.Add(
                new Flotsam(idx,
                    new Sprite2D(
                        TLib.Flotsam[idx],                                   // Randomly select a flotsam sprite
                        randomScreenPosition)));                                          // Randomly position the flotsam object on the screen

            // Randomly flip the sprite horizontally
            if(RandomBoolean())
               gameState.Flotsam[i].sprite.SetEffects(SpriteEffects.FlipHorizontally);
        }

        foreach (var flotsam in gameState.Flotsam)
            flotsam.Initialize(device);
    }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    /// <summary>
    /// Uncapped update method, run every frame and used for checking collisions
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime) {

        camera.Update(gameTime);

#if DEBUG // This is just a test feature and probably wont be included in the final game
        if (IsKeyPressed(Keys.S))
            camera.Shake(10, 0.5f);

        if (IsKeyPressed(Keys.F2))
            XMLSerializer.Serialize<GameState>($"{DateTime.Now.ToBinary()}.xml", gameState);

        if (IsKeyPressed(Keys.F3)) {

          GameState _gameState = XMLSerializer.Deserialize<GameState>(Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml").Last());

          gameState.Flotsam = _gameState.Flotsam;
          gameState.Actors = _gameState.Actors;


        }
            //gameState = XMLSerializer.Deserialize<GameState>("-8584850269411123028.xml");
#endif
    }

    /// <summary>
    /// Fixed update method runs at a fixed rate and time limited, is used for non critical game logic
    /// </summary>
    /// <param name="gameTime"></param>
    public void FixedUpdate(GameTime gameTime) {

        foreach (var flotsam in gameState.Flotsam)
            flotsam.Update(gameTime);

        background2.SetOpacity( (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) / 2 + 0.5f);

        // Sort the flotsam by their X and Y positions to ensure they are drawn in the correct order
        gameState.Flotsam = gameState.Flotsam
                                .OrderBy(flotsam => flotsam.Position.Y)
                                .ThenBy(flotsam => flotsam.Position.X).ToList();
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        background.Draw(spriteBatch);
        background2.Draw(spriteBatch);

        spriteBatch.End();

        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);


        foreach (var flotsam in gameState.Flotsam)
            flotsam.DrawRipples(spriteBatch);

        spriteBatch.End();


        // Need to start a new batch to draw the flotsam sprites, as they are drawn on top of the ripples
        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        foreach (var flotsam in gameState.Flotsam)
            flotsam.Draw(spriteBatch);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        System.Diagnostics.Debug.WriteLine("Playing scene started");
    }

    public void OnSceneEnd() {

        System.Diagnostics.Debug.WriteLine("Playing scene ended");
    }
}
