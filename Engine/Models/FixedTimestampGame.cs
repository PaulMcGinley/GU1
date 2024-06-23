using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Models;

public partial class FixedTimestampGame : Game
{
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;

    //public int TargetFrameRate { get; set; } = 120;                                                         // The target frame rate of the game // TODO: Add this
    public float fixedUpdateDelta = (int)(1000 / (float)60);                                                // The fixed update delta time of the game ( 60 fps )

    // helper variables for the fixed update
    private float previousTime = 0;
    private float accumulator = 0.0f;
    private readonly float maxFrameTime = 250;
    public float AlphaTime => accumulator / fixedUpdateDelta;

    public Trackers.EventsPerSecond UpdateTracker = new();
    public Trackers.EventsPerSecond FixedUpdateTracker = new();
    public Trackers.EventsPerSecond DrawTracker = new();

    public FixedTimestampGame() {

        graphics = new GraphicsDeviceManager(this) {

            SynchronizeWithVerticalRetrace = false,                                                         // Disable V-Sync, this will allow the game to run as fast as the hardware allows
            PreferMultiSampling = E.Config.AntiAliasing,                                                    // Enable anti-aliasing (smoothing of the edges of the sprites), this comes at an additional fps cost
            PreferredBackBufferWidth = 1920,                                                                // Set the preferred back buffer width
            PreferredBackBufferHeight = 1080,                                                               // Set the preferred back buffer height
            IsFullScreen = false,                                                                           // Set the game to run in full screen mode
            GraphicsProfile = GraphicsProfile.HiDef,                                                        // Set the graphics profile to HiDef
            HardwareModeSwitch = true,                                                                      // Disable the hardware mode switch
            PreferHalfPixelOffset = true,                                                                   // Enable the half pixel offset
        };

        IsFixedTimeStep = false;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    } // End of FixedTimestampGame constructor

    protected override void Initialize() {

        spriteBatch = new SpriteBatch(GraphicsDevice);

        base.Initialize();
    } // End of Initialize method

    protected override void LoadContent() {

        // Method intentionally left empty.
    } // End of LoadContent method

    #region Update

    protected override void Update(GameTime gameTime) {

        if (previousTime == 0)
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
