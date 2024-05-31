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

    Vector2 RandomScreenPosition => new(random.Next(0-(1920/2), 1920+(1920/2)), random.Next(0-(1080/2), 1080+(1080/2)));

    public void Initialize(GraphicsDevice device) {

        this.device = device;

        background = new Graphic2D(TLib.PlayingBackground[0], new Vector2(1920/2, 1080/2));
        background2 = new Sprite2D(TLib.PlayingBackground[1], new Vector2(1920/2, 1080/2));
        background2.SetEffects(SpriteEffects.FlipHorizontally);

        camera = new Camera2D(new Viewport(new Rectangle(0, 0, 1920, 1080)));                               // Create a new orthographic camera
        camera.LookAt(new Vector2(1920/2, 1080/2));                                                         // Set the camera to look at the center of the screen
        camera.SetZoomLevel(0.5f);                                                                                 // Set the camera zoom level
    }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    /// <summary>
    /// Uncapped update method, run every frame and used for checking collisions
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime) {

        camera.Update(gameTime);

        foreach (Player player in GameState.Players)
            player.Update(gameTime);

        CheckForCollection();

        CheckForPhotoTaken();

        CheckForReveal();

        CheckForNessieWin();

        CheckForTouristWin();

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


    private void CheckForPhotoTaken() {

        int _score = 0;

        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Tourist))
            if (GamePadRightTriggerPressed(player.ControllerIndex)) {
                player.CameraView.TakePhoto();  // TODO: Make sure this work

                // check if nessie is in the photo
                foreach (Player nessie in GameState.Players.Where(player => player.Role == ActorType.Nessie))       // Loop through all players that are playing as Nessie
                    if (GameState.Flotsam.Where(f=>f.PlayerIndex == nessie.ControllerIndex).First().isAlive) {      // Check if the flotsam is still alive
                        _score = Engine.Graphics.Calculate.SpriteInRectanglePercentage(                             // Calculate the percentage of the flotsam that is in the photo
                            GameState.Flotsam.Where(f=>f.PlayerIndex == nessie.ControllerIndex).First().boundaryBox,// Get the flotsam boundary box
                            player.CameraView.boundaryBox);                                                         // Within the camera view boundary box

                        // Check to see if we got a score
                        if (_score > 0) {
                            player.Score += _score;                                                                 // Add score to player
                            GameState.Flotsam.Where(f=>f.PlayerIndex == nessie.ControllerIndex).First().isFadingOut = true;    // Set the nessie flotsam to not alive
                            _score = 0;                                                                            // Reset the score tracker
                        }
                    }


                    //    if (player.CameraView.boundaryBox.Intersects(GameState.Flotsam.Where(f=>f.PlayerIndex == nessie.ControllerIndex).First().boundaryBox))
                    //        player.Score += 1000;

                System.Diagnostics.Debug.WriteLine(player.Score);
            }

    }

    private void CheckForNessieWin() {

        bool gameOver = true;

        foreach (Flotsam flotsam in GameState.Flotsam.Where(f=>!f.PlayerControlled))
            if (!flotsam.isCollected) {

                gameOver = false;
                break;
            }

        if (gameOver)
            GameState.CurrentScene = GameScene.EndOfRound;
    }

    private void CheckForTouristWin() {

        bool gameOver = true;

        foreach (Flotsam flotsam in GameState.Flotsam.Where(f=>f.PlayerControlled))
            if (flotsam.isAlive) {

                gameOver = false;
                break;
            }

        if (gameOver)
            GameState.CurrentScene = GameScene.EndOfRound;
    }

    /// <summary>
    /// Check for nessie collecting flotsam
    /// </summary>
    void CheckForCollection() {

        /* Note from Paul:
         * For who ever is looking at the code below, I apologies.
         * I had planned on implementing player control differently, but ran out of time.
         * This works, and is full of educational value... And a whole 'what not to do's' xD
        */

        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Nessie))       // Loop through all players that are playing as Nessie
                foreach (Flotsam flotsam in GameState.Flotsam)                                              // Loop through all flotsam objects
                    if(GameState.Flotsam                                                                    // If, from the list of flotsam
                                        .Where(p=>p.PlayerIndex == player.ControllerIndex)                  // Get a list of flotsam that have the same player index as the player
                                        .First()                                                            // Get the first flotsam object from the list, because we only need one and only have one xD
                                        .boundaryBox.Intersects(flotsam.boundaryBox) &&                     // Check if the player is colliding with the flotsam
                        IsGamePadButtonPressed(player.ControllerIndex, Buttons.A))                          // Check if the player is pressing the A button
                        // We ask the flotsam to collect the object, if it returns true, the player has collected the flotsam, if false, the player has not collected the flotsam
                        if (flotsam.Collect()) {

                            player.Score += 100;                                                            // Add 100 to the player's score
                            return;                                                                         // Exit the method to prevent the player from collecting multiple flotsam in one frame
                        }
    }

    /// <summary>
    /// Check for tourist revealing flotsam
    /// </summary>
    void CheckForReveal() {

        // No complaints here, would ideally be optimized but it works without a major performance hit

        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Tourist))
            foreach (Flotsam flotsam in GameState.Flotsam)
                if (flotsam.boundaryBox.Intersects(player.CameraView.boundaryBox))  // Check if the flotsam is colliding with the player
                    flotsam.Inspect();
    }

    /// <summary>
    /// Fixed update method runs at a fixed rate and time limited, is used for non critical game logic
    /// </summary>
    /// <param name="gameTime"></param>
    public void FixedUpdate(GameTime gameTime) {

        foreach (var flotsam in GameState.Flotsam)
            flotsam.Update(gameTime);

        GameState.Boat.Update(gameTime);

        background2.SetOpacity((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) / 2 + 0.5f);

        // Sort the flotsam by their X and Y positions to ensure they are drawn in the correct order
        GameState.Flotsam = GameState.Flotsam
                                .OrderBy(flotsam => flotsam.Position.Y)
                                .ThenBy(flotsam => flotsam.Position.X)
                                .ToList();
    }

    public void Draw(SpriteBatch spriteBatch) {

        #region Background

        spriteBatch.Begin();

        background.Draw(spriteBatch);
        background2.Draw(spriteBatch);

        spriteBatch.End();

        #endregion


        #region Gameplay

        spriteBatch.Begin(transformMatrix: camera.TransformMatrix, samplerState: SamplerState.PointClamp);


        foreach (var flotsam in GameState.Flotsam)
            flotsam.DrawRipples(spriteBatch);

        spriteBatch.End();


        // Need to start a new batch to draw the flotsam sprites, as they are drawn on top of the ripples
        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        foreach (var flotsam in GameState.Flotsam)
            flotsam.Draw(spriteBatch);

        GameState.Boat.Draw(spriteBatch);

        foreach (Player player in GameState.Players)
            player.Draw(spriteBatch);

        spriteBatch.End();

        #endregion


        #region UI

        spriteBatch.Begin();

        spriteBatch.End();

        #endregion
    }

    public void OnSceneStart() {

        StartNewRound();
    }

    public void OnSceneEnd() { }

    public void StartNewRound() {

        // ? Shouldn't need to clear the flotsam, just maybe a reset method
        GameState.Flotsam.Clear();

        for (int i = 0; i <= 5; i++) {

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
