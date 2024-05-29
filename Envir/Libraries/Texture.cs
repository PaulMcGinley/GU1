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

    /***********************************************************************
     *** Miscellaneous *****************************************************
     ***********************************************************************/

    public static Texture2D Pixel;

    /***********************************************************************
     *** Effects ***********************************************************
     ***********************************************************************/

    public static Texture2D CameraCutout;

    /***********************************************************************
     *** GameGraphics ******************************************************
     ***********************************************************************/

    public static Texture2D mainMenuBackground;
    public static Texture2D[] Background;
    public static Texture2D LobbyBackground;

    /***********************************************************************
     *** UI Elements *******************************************************
     ***********************************************************************/

     public static Texture2D Arrow;

    /// <summary>
    /// Load all the textures
    /// </summary>
    /// <param name="graphicsDevice"></param>
    public static void Initialize(GraphicsDevice graphicsDevice, ContentManager content) {

        for (int i = 0; i < Flotsam.Length; i++)
            Flotsam[i] = content.Load<Texture2D>($"Graphics/Flotsam/sprite{i}");

        Pixel = new Texture2D(graphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });

        //CameraCutout = content.Load<Texture2D>("Textures/Effects/CameraCutout");

        mainMenuBackground = content.Load<Texture2D>("Graphics/menuBG");
        Background = new Texture2D[2];
        Background[0] = content.Load<Texture2D>("Graphics/bg1");
        Background[1] = content.Load<Texture2D>("Graphics/bg2");
        LobbyBackground = content.Load<Texture2D>("Graphics/LobbyScene-BG");

        // UI Elements
        Arrow = content.Load<Texture2D>("Graphics/UI/arrow");
    }
}
