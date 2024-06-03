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

    public static Texture2D NessieAvatar;
    public static Texture2D TouristAvatar;

    public static Texture2D NessieTitle;
    public static Texture2D TouristTitle;

    public static Texture2D[] NessieGuide;
    public static Texture2D[] TouristGuide;

    public static Texture2D ButtonGuide;

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
    public static Texture2D ControlsBackground;

    /***********************************************************************
     *** UI Elements *******************************************************
     ***********************************************************************/

     public static Texture2D Arrow;
     public static Texture2D ScoreRow;

     public static Texture2D[] Numbers = new Texture2D[10];

     public static Texture2D NamePlayerBackground;
     public static Texture2D NamePlayerTitle;
     public static Texture2D NamePlayerCursor;

     public static Texture2D Press_A_ToJoin;
     public static Texture2D Press_A_ToContinue;
     public static Texture2D Press_Start_ToBegin;
     public static Texture2D Press_Start_ToConfirm;

     public static Texture2D WinnerTitle;
     public static Texture2D LeaderboardTitle;

     public static Texture2D Cursor;

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

        NessieAvatar = content.Load<Texture2D>("Graphics/nessieAvatar");
        TouristAvatar = content.Load<Texture2D>("Graphics/touristAvatar");

        NessieTitle = content.Load<Texture2D>("Graphics/nessieTitle");
        TouristTitle = content.Load<Texture2D>("Graphics/touristTitle");

        NessieGuide = new Texture2D[3];
        NessieGuide[0] = content.Load<Texture2D>("Graphics/Guide/nessieGuide1");
        NessieGuide[1] = content.Load<Texture2D>("Graphics/Guide/nessieGuide2");
        NessieGuide[2] = content.Load<Texture2D>("Graphics/Guide/nessieGuide3");

        TouristGuide = new Texture2D[3];
        TouristGuide[0] = content.Load<Texture2D>("Graphics/Guide/touristGuide1");
        TouristGuide[1] = content.Load<Texture2D>("Graphics/Guide/touristGuide2");
        TouristGuide[2] = content.Load<Texture2D>("Graphics/Guide/touristGuide3");

        ButtonGuide = content.Load<Texture2D>("Graphics/Guide/buttons");

        //CameraCutout = content.Load<Texture2D>("Textures/Effects/CameraCutout");

        mainMenuBackground = content.Load<Texture2D>("Graphics/MainMenuScene-BG");
        PlayingBackground = new Texture2D[2];
        PlayingBackground[0] = content.Load<Texture2D>("Graphics/bg1");
        PlayingBackground[1] = content.Load<Texture2D>("Graphics/bg2");
        LobbyBackground = content.Load<Texture2D>("Graphics/LobbyScene-BG");
        ControlsBackground = content.Load<Texture2D>("Graphics/controls-BG");

        // UI Elements
        Arrow = content.Load<Texture2D>("Graphics/UI/arrow");
        ScoreRow = content.Load<Texture2D>("Graphics/UI/ScoreRow");

        for (int i = 0; i < Numbers.Length; i++)
            Numbers[i] = content.Load<Texture2D>($"Numbers/{i}");

        NamePlayerBackground = content.Load<Texture2D>("Graphics/UI/npBox");
        NamePlayerTitle = content.Load<Texture2D>("Graphics/UI/npTitle");
        NamePlayerCursor = content.Load<Texture2D>("Graphics/UI/npSelector");

        Press_A_ToJoin = content.Load<Texture2D>("Graphics/UI/Press-A-to-join");
        Press_A_ToContinue = content.Load<Texture2D>("Graphics/UI/Press-A-to-continue");
        Press_Start_ToBegin = content.Load<Texture2D>("Graphics/UI/Press-Start-to-begin");
        Press_Start_ToConfirm = content.Load<Texture2D>("Graphics/UI/Press-Start-to-confirm");

        WinnerTitle = content.Load<Texture2D>("Graphics/WinnerTitle2");
        LeaderboardTitle = content.Load<Texture2D>("Graphics/LeaderboardTitle");

        Cursor = content.Load<Texture2D>("Graphics/cursor");
    }
}
