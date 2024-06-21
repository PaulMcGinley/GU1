using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GU1.Envir.Scenes;

public class Playing : IScene {

    private Random random = new();

    private Graphic2D background;                                                                           // The background image or 'map' of the game
    private Sprite2D background2;
    private Camera2D camera;                                                                                // The camera object used to control the view of the game
    GraphicsDevice device;

    Vector2 RandomScreenPosition => new(random.Next(0-(1920/2), 1920+(1920/2)), random.Next(0-(1080/2), 1080+(1080/2)));

    int nessieScoreWidth = 250;                                                                             // The longest row of Nessie flotsam
    int nessiesScoreHeight => GameState.Players.Where(p=>p.Role == ActorType.Nessie).Count()*25;            // The height of the Nessie score
    Rectangle nessieScoreBounds => new(-(1920/2), 0, nessieScoreWidth, nessiesScoreHeight);                 // The bounds of the screen for the Nessie score
    float nessieScoreOpacity = 1f;                                                                          // The opacity of the score bounds
    float nessieMinScoreOpacity = 0.3f;                                                                     // The minimum opacity of the score bounds

    int touristScoreWidth = 250;                                                                            // The longest row of Tourists
    int touristsScoreHeight => GameState.Players.Where(p=>p.Role == ActorType.Tourist).Count()*25;          // The height of the Tourist score
    Rectangle touristScoreBounds => new((int)(1920*1.5f) - touristScoreWidth, 0, touristScoreWidth, touristsScoreHeight);   // The bounds of the screen for the Tourist score
    float touristScoreOpacity = 1f;                                                                         // The opacity of the score bounds
    float touristMinScoreOpacity = 0.3f;                                                                    // The minimum opacity of the score bounds

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) {

        this.device = device;

        background = new Graphic2D(TLib.PlayingBackground[0], new Vector2(1920/2, 1080/2));
        background2 = new Sprite2D(TLib.PlayingBackground[1], new Vector2(1920/2, 1080/2));
        background2.SetEffects(SpriteEffects.FlipHorizontally);

        camera = new Camera2D(new Viewport(new Rectangle(0, 0, 1920, 1080)));                               // Create a new orthographic camera
        camera.LookAt(new Vector2(1920/2, 1080/2));                                                         // Set the camera to look at the center of the screen
        camera.SetZoomLevel(0.5f);                                                                          // Set the camera zoom level
    }

    public void LoadContent(ContentManager content) { }

    public void UnloadContent() { }

    public void Update(GameTime gameTime) {

        if (IsAnyInputPressed(Keys.P, Buttons.Start))
            GameState.CurrentScene = GameScene.PauseMenu;

        camera.Update(gameTime);

        foreach (Player player in GameState.Players)
            player.Update(gameTime);

        // Check if Nessie has collected any flotsam
        CheckForCollection();

        // Check if the tourist has taken a photo
        CheckForPhotoTaken();

        // Check if the tourist has revealed any collected flotsam
        CheckForReveal();

        // Check if Nessie has collected all the flotsam
        CheckForNessieWin();

        // Check if the tourist has photographed all the nessies
        CheckForTouristWin();

        // Check if the tourist has run out of photos
        CheckForTouristLose();
    }

    public void FixedUpdate(GameTime gameTime) {

        bool nessieScoreFadeOut = false;
        bool touristScoreFadeOut = false;                                                                   // Assume the flotsam is not fading out

        // Check if flotsam intersects with the score bounds
        foreach (Flotsam flotsam in GameState.Flotsam)
            if (flotsam.boundaryBox.Intersects(nessieScoreBounds)) {
                nessieScoreOpacity = MathHelper.Lerp(nessieScoreOpacity, nessieMinScoreOpacity, 0.1f);      // Set the score bounds opacity to the minimum score bounds opacity
                nessieScoreFadeOut = true;                                                                  // The flotsam is fading out
                break;                                                                                      // Exit the loop
            }
            else if (flotsam.boundaryBox.Intersects(touristScoreBounds)) {
                touristScoreOpacity = MathHelper.Lerp(touristScoreOpacity, touristMinScoreOpacity, 0.1f);   // Set the score bounds opacity to the minimum score bounds opacity
                touristScoreFadeOut = true;                                                                 // The flotsam is fading out
                break;                                                                                      // Exit the loop
            }

        if (!nessieScoreFadeOut)                                                                            // If the flotsam is not fading out
            nessieScoreOpacity = MathHelper.Lerp(nessieScoreOpacity, 1f, 0.1f);                             // Set the score bounds opacity to 1

        if (!touristScoreFadeOut)                                                                           // If the flotsam is not fading out
            touristScoreOpacity = MathHelper.Lerp(touristScoreOpacity, 1f, 0.1f);                           // Set the score bounds opacity to 1

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


        #region Game

        spriteBatch.Begin(transformMatrix: camera.TransformMatrix, samplerState: SamplerState.PointClamp);

        // Ripples
        foreach (var flotsam in GameState.Flotsam)
            flotsam.DrawRipples(spriteBatch);

        spriteBatch.End();


        spriteBatch.Begin(transformMatrix: camera.TransformMatrix);

        // Flotsam
        foreach (var flotsam in GameState.Flotsam)
            flotsam.Draw(spriteBatch);

        // Board
        GameState.Boat.Draw(spriteBatch);

        // Players
        foreach (Player player in GameState.Players)
            player.Draw(spriteBatch);

        // Scores

        // Draw the nessieScoreBounds
        //spriteBatch.Draw(TLib.Pixel, nessieScoreBounds, Color.White * scoreOpacity);
        int nessieCount = 0;
        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Nessie)) {

            spriteBatch.DrawString(FLib.LeaderboardFont, player.CameraView.playerName + " : " + player.Score.ToString(), new Vector2(nessieScoreBounds.Left, 20*nessieCount) + Vector2.One, Color.Black * nessieScoreOpacity);
            spriteBatch.DrawString(FLib.LeaderboardFont, player.CameraView.playerName + " : " + player.Score.ToString(), new Vector2(nessieScoreBounds.Left, 20*nessieCount), Color.White * nessieScoreOpacity);
            nessieCount++;
        }

        // Draw the touristScoreBounds
        //spriteBatch.Draw(TLib.Pixel, touristScoreBounds, Color.White * scoreOpacity);
        int touristCount = 0;
        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Tourist)) {

            spriteBatch.DrawString(FLib.LeaderboardFont, player.CameraView.playerName + " : " +  player.Score.ToString(), new Vector2(touristScoreBounds.Left, 20*touristCount) + Vector2.One, Color.Black * touristScoreOpacity);
            spriteBatch.DrawString(FLib.LeaderboardFont, player.CameraView.playerName + " : " +  player.Score.ToString(), new Vector2(touristScoreBounds.Left, 20*touristCount), Color.White * touristScoreOpacity);
            touristCount++;
        }

        spriteBatch.End();

        #endregion

    }

    public void OnSceneStart() {

        if (GameState.PreviousScene == GameScene.PauseMenu)
            return;

        if (Settings.returnScene == GameScene.Settings)
            return;

        StartNewRound();

        SetupBackgroundMusic();
    }

    public void OnSceneEnd() {

    }

    #endregion


    #region Game Logic

    private void CheckForTouristLose() {

        int remainingPhotos = 0;                                                                            // The number of photos remaining overall

        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Tourist))
            remainingPhotos += player.CameraView.remainingPhotos;                                           // Add the player's remaining photos to the overall remaining photos

        if (remainingPhotos == 0)                                                                           // If there are no photos remaining
            GameState.CurrentScene = GameScene.EndOfRound;                                                  // Move to the end of round scene
    }

    private void CheckForPhotoTaken() {

        int _score = 0;                                                                                     // The score tracker

        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Tourist))
            if (GamePadRightTriggerPressed(player.ControllerIndex) && player.CameraView.TakePhoto(background2.GetOpacity())) {

                // check if nessie is in the photo
                foreach (Player nessie in GameState.Players.Where(player => player.Role == ActorType.Nessie))       // Loop through all players that are playing as Nessie
                    if (GameState.Flotsam.Where(f=>f.PlayerIndex == nessie.ControllerIndex).First().isAlive) {      // Check if the flotsam is still alive
                        _score = Engine.Graphics.Calculate.SpriteInRectanglePercentage(                             // Calculate the percentage of the flotsam that is in the photo
                            GameState.Flotsam.Where(f=>f.PlayerIndex == nessie.ControllerIndex).First().boundaryBox,// Get the flotsam boundary box
                            player.CameraView.boundaryBox);                                                         // Within the camera view boundary box

                        // Check to see if we got a score
                        if (_score > 0) {
                            player.Score += _score;                                                         // Add score to player
                            GameState.Flotsam.Where(f=>f.PlayerIndex == nessie.ControllerIndex).First().isFadingOut = true;    // Set the nessie flotsam to not alive
                            _score = 0;                                                                     // Reset the score tracker

                            SLib.Camera[0].Play(GameState.SFXVolume, 0, 0);                                 // Play the camera sound effect
                        }
                        else
                            SLib.Camera[1].Play(GameState.SFXVolume, 0, 0);                                 // Play the camera sound effect
                    }
            }
    }

    private void CheckForNessieWin() {

        bool roundOver = true;                                                                              // Assume the round is over

        foreach (Flotsam flotsam in GameState.Flotsam.Where(f=>!f.PlayerControlled))
            if (!flotsam.isCollected) {                                                                     // If the flotsam is not collected

                roundOver = false;                                                                          // The round is not over
                break;                                                                                      // Exit the loop
            }

        if (roundOver)                                                                                      // If the round is over
            GameState.CurrentScene = GameScene.EndOfRound;                                                  // Move to the end of round scene
    }

    private void CheckForTouristWin() {

        bool roundOver = true;                                                                              // Assume the round is over

        foreach (Flotsam flotsam in GameState.Flotsam.Where(f=>f.PlayerControlled))
            if (flotsam.isAlive) {                                                                          // If the flotsam is still alive

                roundOver = false;                                                                          // The round is not over
                break;                                                                                      // Exit the loop
            }

        if (roundOver)                                                                                      // If the round is over
            GameState.CurrentScene = GameScene.EndOfRound;                                                  // Move to the end of round scene
    }

    private void CheckForCollection() {

        if (GameState.Flotsam.Count == 0)                                                                    // If there are no flotsam objects, exit the method
            return;

        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Nessie))       // Loop through all players that are playing as Nessie
            foreach (Flotsam flotsam in GameState.Flotsam)                                                  // Loop through all flotsam objects
                if(GameState.Flotsam                                                                        // If, from the list of flotsam
                    .Where(p=>p.PlayerIndex == player.ControllerIndex)                                      // Get a list of flotsam that have the same player index as the player
                    .First()                                                                                // Get the first flotsam object from the list, because we only need one and only have one xD
                    .boundaryBox.Intersects(flotsam.boundaryBox)                                            // Check if the player is colliding with the flotsam
                && (IsGamePadButtonPressed(player.ControllerIndex, Buttons.A) || GamePadRightTriggerPressed(player.ControllerIndex)|| GamePadLeftTriggerPressed(player.ControllerIndex)))  // Check if the player is pressing the A button or the trigger
                        // We ask the flotsam to collect the object, if it returns true, the player has collected the flotsam, if false, the player has not collected the flotsam
                    if (flotsam.Collect()) {

                        player.Score += 100;                                                                // Add 100 to the player's score
                        return;                                                                             // Exit the method to prevent the player from collecting multiple flotsam in one frame
                    }
    }

    private void CheckForReveal() {

        foreach (Player player in GameState.Players.Where(player => player.Role == ActorType.Tourist))
            foreach (Flotsam flotsam in GameState.Flotsam)
                if (flotsam.boundaryBox.Intersects(player.CameraView.boundaryBox))                          // Check if the flotsam is colliding with the player
                        if (flotsam.Inspect())
                            player.Score += 100;                                                            // Add 100 to the player's score
    }

    #endregion

    public void StartNewRound() {

        GameState.Flotsam.Clear();                                                                          // Clear the list of flotsam objects

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

    private void SetupBackgroundMusic() {

        MediaPlayer.Volume = GameState.MusicVolume;                                                         // Set the volume of the music player
        MediaPlayer.Play(SLib.GameMusic[random.Int(0, SLib.GameMusic.Length)]);                             // Play a random game music track
        MediaPlayer.ActiveSongChanged += (s, e) => MediaPlayer.Play(SLib.GameMusic[random.Int(0, SLib.GameMusic.Length)]); // When the song ends, play another random game music track
    }
}
