using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Libraries;

public class Texture {
    
    /***********************************************************************
     *** Miscellanous ******************************************************
     ***********************************************************************/

    public static Texture2D Pixel;

    /***********************************************************************
     *** Effects ***********************************************************
     ***********************************************************************/

    public static Texture2D CameraCutout;

    /***********************************************************************
     *** GameGraphics ******************************************************
     ***********************************************************************/

     public static Texture2D Background;

    /// <summary>
    /// Load all the textures
    /// </summary>
    /// <param name="graphicsDevice"></param>
    public static void Initialize(GraphicsDevice graphicsDevice, ContentManager content) {

        Pixel = new Texture2D(graphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });

        //CameraCutout = content.Load<Texture2D>("Textures/Effects/CameraCutout");

        Background = content.Load<Texture2D>("Graphics/map");
    }
}
