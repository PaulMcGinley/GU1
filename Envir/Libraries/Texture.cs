using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Libraries;

public class Texture {

    /// <summary>
    /// Private constructor to prevent instantiation
    /// </summary>
    private Texture() { }

    /***********************************************************************
     *** Sprites ***********************************************************
     ***********************************************************************/

    public static readonly Texture2D[] Flotsam = new Texture2D[14];
    public static Texture2D[] Boat;
    public static Texture2D CameraView;

    /***********************************************************************
     *** Miscellaneous *****************************************************
     ***********************************************************************/

    public static Texture2D Pixel;

    public static Texture2D TheNessie;

    /***********************************************************************
     *** Effects ***********************************************************
     ***********************************************************************/

    public static Texture2D CameraCutout;

    /***********************************************************************
     *** GameGraphics ******************************************************
     ***********************************************************************/

    public static Texture2D mainMenuBackground;
    public static Texture2D[] PlayingBackground;
    public static Texture2D LobbyBackground;

    /***********************************************************************
     *** UI Elements *******************************************************
     ***********************************************************************/

     public static Texture2D Arrow;
     public static Texture2D ScoreRow;

     public static Texture2D[] Numbers = new Texture2D[10];

     public static Texture2D NamePlayerBackground;
     public static Texture2D NamePlayerTitle;
     public static Texture2D NamePlayerCursor;

    /// <summary>
    /// Load all the textures
    /// </summary>
    /// <param name="graphicsDevice"></param>
    public static void Initialize(GraphicsDevice graphicsDevice, ContentManager content) {

        for (int i = 0; i < Flotsam.Length; i++)
            Flotsam[i] = content.Load<Texture2D>($"Graphics/Flotsam/sprite{i}");

        Boat = new Texture2D[2];
        Boat[0] = content.Load<Texture2D>("Graphics/boat");
        Boat[1] = content.Load<Texture2D>("Graphics/boat2");

        CameraView = content.Load<Texture2D>("Graphics/Actor/CameraView");

        Pixel = new Texture2D(graphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });

        TheNessie = content.Load<Texture2D>("Graphics/Flotsam/TheNessie");

        //CameraCutout = content.Load<Texture2D>("Textures/Effects/CameraCutout");

        mainMenuBackground = content.Load<Texture2D>("Graphics/MainMenuScene-BG");
        PlayingBackground = new Texture2D[2];
        PlayingBackground[0] = content.Load<Texture2D>("Graphics/bg1");
        PlayingBackground[1] = content.Load<Texture2D>("Graphics/bg2");
        LobbyBackground = content.Load<Texture2D>("Graphics/LobbyScene-BG");

        // UI Elements
        Arrow = content.Load<Texture2D>("Graphics/UI/arrow");
        ScoreRow = content.Load<Texture2D>("Graphics/UI/ScoreRow");

        for (int i = 0; i < Numbers.Length; i++)
            Numbers[i] = content.Load<Texture2D>($"Numbers/{i}");

        NamePlayerBackground = content.Load<Texture2D>("Graphics/UI/npBox");
        NamePlayerTitle = content.Load<Texture2D>("Graphics/UI/npTitle");
        NamePlayerCursor = content.Load<Texture2D>("Graphics/UI/npSelector");
    }
}
