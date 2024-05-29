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
    private Sprite2D background2;
    private Camera2D camera;                                                                                // The camera object used to control the view of the game
    GraphicsDevice device;

    //private GameState gameState;

    Vector2 RandomScreenPosition => new(random.Next(0-(1920/2), 1920+(1920/2)), random.Next(0-(1080/2), 1080+(1080/2)));

    public void Initialize(GraphicsDevice device) {

        this.device = device;

        background = new Graphic2D(TLib.Background[0], new Vector2(1920/2, 1080/2));                           // Create a new 2D graphic object for the background image
        background2 = new Sprite2D(TLib.Background[1], new Vector2(1920/2, 1080/2));
        background2.SetEffects(SpriteEffects.FlipHorizontally);

        camera = new Camera2D(new Viewport(new Rectangle(0, 0, 1920, 1080)));                               // Create a new orthographic camera
        camera.LookAt(new Vector2(1920/2, 1080/2));                                                         // Set the camera to look at the center of the screen
        camera.SetZoomLevel(0.5f);                                                                                 // Set the camera zoom level

        //gameState = new GameState();                                                                        // Create a new game state object

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

        if (IsKeyPressed(Keys.S))
            camera.Shake(10, 0.5f);

        // if (IsKeyPressed(Keys.F2))
        //     XMLSerializer.Serialize<GameState>($"{DateTime.Now.ToBinary()}.xml", gameState);

        if (IsKeyPressed(Keys.F3)) {

            // TODO: This should have its own renderer
            // GameState _gameState = XMLSerializer.Deserialize<GameState>(Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml").Last());

            // gameState.Flotsam = _gameState.Flotsam;
            // gameState.Players = _gameState.Players;
        }
#endif
    }

    /// <summary>
    /// Fixed update method runs at a fixed rate and time limited, is used for non critical game logic
    /// </summary>
    /// <param name="gameTime"></param>
    public void FixedUpdate(GameTime gameTime) {

        foreach (var flotsam in GameState.Flotsam)
            flotsam.Update(gameTime);

        GameState.Boat.Update(gameTime);

        background2.SetOpacity( (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) / 2 + 0.5f);

        // Sort the flotsam by their X and Y positions to ensure they are drawn in the correct order
        GameState.Flotsam = GameState.Flotsam
                                .OrderBy(flotsam => flotsam.Position.Y)
                                .ThenBy(flotsam => flotsam.Position.X)
                                .ToList();
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        background.Draw(spriteBatch);
        background2.Draw(spriteBatch);

        spriteBatch.End();

        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);


        foreach (var flotsam in GameState.Flotsam)
            flotsam.DrawRipples(spriteBatch);

        spriteBatch.End();


        // Need to start a new batch to draw the flotsam sprites, as they are drawn on top of the ripples
        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        foreach (var flotsam in GameState.Flotsam)
            flotsam.Draw(spriteBatch);

        GameState.Boat.Draw(spriteBatch);

        spriteBatch.End();
    }

    public void OnSceneStart() {

        StartNewRound();
    }

    public void OnSceneEnd() { }

    public void StartNewRound() {

        GameState.Flotsam.Clear();

        for (int i = 0; i <= 100; i++) {

            int idx = random.Int(0, TLib.Flotsam.Length);                                                   // Randomly select a flotsam sprite

            GameState.Flotsam.Add(
                new Flotsam(
                    idx,                                                                                    // Set the sprite index
                    new Sprite2D(
                        TLib.Flotsam[idx],                                                                  // Set the sprite texture
                        RandomScreenPosition)));                                                            // Randomly position the flotsam object on the screen
        }

        foreach (var flotsam in GameState.Flotsam)
            flotsam.Initialize(device);                                                                     // Initialize the flotsam object

        foreach (Player player in GameState.Players)                                                        // Loop through all players
            if (player.Role == ActorType.Nessie)                                                            // If the player is Nessie
                GameState.Flotsam[player.ControllerIndex].PlayerIndex = player.ControllerIndex;             // Set the flotsam object's player index to the player's controller index
    }
}
