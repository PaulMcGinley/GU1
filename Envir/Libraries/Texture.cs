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

    public static readonly Texture2D[] Flotsam = new Texture2D[8];

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
    public static Texture2D Background;

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
            Flotsam[i] = content.Load<Texture2D>($"Graphics/Flotsam/FlotsamJetsam{i+1}");

        Pixel = new Texture2D(graphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });

        //CameraCutout = content.Load<Texture2D>("Textures/Effects/CameraCutout");

        mainMenuBackground = content.Load<Texture2D>("Graphics/menuBG");
        Background = content.Load<Texture2D>("Graphics/map");

        // UI Elements
        Arrow = content.Load<Texture2D>("Graphics/UI/arrow");
    }
}
