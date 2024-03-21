using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Models;

public partial class FixedTimestampGame : Game
{
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;

    //public int TargetFrameRate { get; set; } = 120;                                                         // The target frame rate of the game
    public float fixedUpdateDelta = (int)(1000 / (float)30);                                                // The fixed update delta time of the game ( 30 fps )

    // helper variables for the fixed update
    private float previousTime = 0;
    private float accumulator = 0.0f;
    private float maxFrameTime = 250;
    public float AlphaTime => accumulator / fixedUpdateDelta;

    public E.Trackers.EventsPerSecond UpdateTracker = new();
    public E.Trackers.EventsPerSecond FixedUpdateTracker = new();
    public E.Trackers.EventsPerSecond DrawTracker = new();

    public FixedTimestampGame() {

        graphics = new GraphicsDeviceManager(this) {

            SynchronizeWithVerticalRetrace = false,                                                         // Disable V-Sync, this will allow the game to run as fast as the hardware allows
            PreferMultiSampling = E.Config.AntiAliasing,                                                    // Enable anti-aliasing (smoothing of the edges of the sprites), this comes at an additional fps cost
            PreferredBackBufferWidth = 1920,                                                                // Set the preferred back buffer width
            PreferredBackBufferHeight = 1080,                                                               // Set the preferred back buffer height          
            IsFullScreen = true,                                                                            // Set the game to run in full screen mode  
        };
        
        IsFixedTimeStep = false;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    } // End of FixedTimestampGame constructor

    protected override void Initialize() {

        base.Initialize();
    } // End of Initialize method

    protected override void LoadContent() {

        spriteBatch = new SpriteBatch(GraphicsDevice);
    } // End of LoadContent method

    #region Update

    protected override void Update(GameTime gameTime) {

        if(previousTime == 0)
            previousTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

        float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
        float frameTime = now - previousTime;

        if (frameTime > maxFrameTime)
            frameTime = maxFrameTime;
         
        previousTime = now;
 
        accumulator += frameTime;
 
        while (accumulator >= fixedUpdateDelta) {

            FixedUpdate(gameTime);
            accumulator -= fixedUpdateDelta;
        }


        base.Update(gameTime);

        // -------------------------------------------------------------------------------------------------
        // --- DEBUG CODE ----------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------

        if (E.Config.TrackUpdateRate)
            UpdateTracker.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        
    } // End of Update method

    #endregion


    #region FixedUpdate

    protected virtual void FixedUpdate(GameTime gameTime) {

        // -------------------------------------------------------------------------------------------------
        // --- DEBUG CODE ----------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------

        if(E.Config.TrackUpdateRate)
            FixedUpdateTracker.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

    } // End of UpdateFixed method

    #endregion


    #region Draw

    protected override void Draw(GameTime gameTime) {

        // -------------------------------------------------------------------------------------------------
        // --- DEBUG CODE ----------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------

        if(E.Config.TrackUpdateRate)
            DrawTracker.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            
    } // End of Draw method

    #endregion
}
