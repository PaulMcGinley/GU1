using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Photo {

    public string SaveDir => $"{Directory.GetCurrentDirectory()}/Photos";

    #region Data

    public Vector2 location;                                                                                // On the map
    public string fileName = string.Empty;                                                                  // Save file name
    public string photographer;                                                                             // The name of the player who took the photo
    public DateTime timeStamp;                                                                              // The time the photo was taken
    public string Date => timeStamp.ToString("yyyy-MM-dd");                                                 // The date the photo was taken (Extracted from the timeStamp)
    public string Time => timeStamp.ToString("HH:mm");                                                      // The time the photo was taken (Extracted from the timeStamp)

    public List<Content> content = new();                                                                           // The content of the photo

    #endregion

    #region Texture

    [XmlIgnore]
    public RenderTarget2D fullPicture;                                                                            // The render target for the photo

    [XmlIgnore]
    public RenderTarget2D croppedPicture;                                                                        // The cropped picture

    [XmlIgnore]
    public RenderTarget2D framedPicture;

    #endregion

    public Photo Load(string fileName) {

        string _path = $"{SaveDir}/{fileName}.xml";

        if (!File.Exists(_path))
            throw new FileNotFoundException($"File not found: {_path}");

        this.fileName = fileName;

        // Load the photo from the path
        return XMLSerializer.Deserialize<Photo>(_path);
    }

    public void Save() {

        if (fileName == string.Empty)
            fileName = $"{DateTime.Now.ToBinary()}";

        string _path = $"{SaveDir}/{fileName}.xml";

        if (!Directory.Exists(SaveDir))
            Directory.CreateDirectory(SaveDir);

        // Save the photo to the path
        XMLSerializer.Serialize<Photo>(_path, this);
    }

    public void Render(SpriteBatch spriteBatch) {

        #region Render Picture

        // set render target
        fullPicture = new RenderTarget2D(spriteBatch.GraphicsDevice, 1920, 1080);
        spriteBatch.GraphicsDevice.SetRenderTarget(fullPicture);
        spriteBatch.GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

        // Draw the background
        spriteBatch.Draw(TLib.PlayingBackground[0], new Vector2(0, 0), Color.White);

        // Sort the sprites so they are drawn in the correct order
        content.Sort((a, b) => a.position.X.CompareTo(b.position.X));
        content.Sort((a, b) => a.position.Y.CompareTo(b.position.Y));

        // Draw the content
        foreach (var item in content) {

            if (item.isBoat)
                spriteBatch.Draw(TLib.Boat[item.isFlipped ? 1 : 0], item.position, null, Color.White, item.rotation, new Vector2(128, 128), 0.5f, item.isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            else if (item.isNessie)
                spriteBatch.Draw(TLib.TheNessie, item.position, null, Color.White, item.rotation, new Vector2(128, 128), 0.5f, item.isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            else
                spriteBatch.Draw(TLib.Flotsam[item.spriteID], item.position, null, Color.White, item.rotation, new Vector2(128, 128), 0.5f, item.isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        spriteBatch.End();

        #endregion


        #region Cropped Picture

        croppedPicture = new RenderTarget2D(spriteBatch.GraphicsDevice, 256, 256);
        spriteBatch.GraphicsDevice.SetRenderTarget(croppedPicture);
        spriteBatch.GraphicsDevice.Clear(Color.White);

        spriteBatch.Begin();

        spriteBatch.Draw(fullPicture, new Rectangle(0, 0, 256, 256), new Rectangle((int)location.X - 128, (int)location.Y - 128, 256, 256), Color.White);

        spriteBatch.End();

        #endregion


        #region Framed Picture

        framedPicture = new RenderTarget2D(spriteBatch.GraphicsDevice, croppedPicture.Width + 50, croppedPicture.Height + 125);
        spriteBatch.GraphicsDevice.SetRenderTarget(framedPicture);
        spriteBatch.GraphicsDevice.Clear(Color.White);

        spriteBatch.Begin();

        DrawRectangle(new Rectangle(1, 1, framedPicture.Width-1, framedPicture.Height-1), spriteBatch, Color.Lime, 0);

        spriteBatch.Draw(croppedPicture, new Vector2(25, 25), Color.White);
        DrawRectangle(new Rectangle(20, 20, croppedPicture.Width+10, croppedPicture.Height+10), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(21, 21, croppedPicture.Width+8, croppedPicture.Height+8), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(22, 22, croppedPicture.Width+6, croppedPicture.Height+6), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(23, 23, croppedPicture.Width+4, croppedPicture.Height+4), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(24, 24, croppedPicture.Width+2, croppedPicture.Height+2), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(25, 25, croppedPicture.Width, croppedPicture.Height), spriteBatch, Color.Black, 0);

        spriteBatch.DrawString(FLib.DebugFont, $"Captured by {photographer}", new Vector2(25, 25 + croppedPicture.Height + 15), Color.DarkRed);
        spriteBatch.DrawString(FLib.DebugFont, $"Date: {Date}", new Vector2(25, 25 + croppedPicture.Height + 35), Color.DarkRed);
        spriteBatch.DrawString(FLib.DebugFont, $"Time: {Time}", new Vector2(25, 25 + croppedPicture.Height + 55), Color.DarkRed);

        spriteBatch.End();

        #endregion

        spriteBatch.GraphicsDevice.SetRenderTarget(null);

    }

    // public void Optimize() {

    //     foreach (var item in content) {

    //         if (item.isBoat) continue;
    //         if (item.isNessie) continue;

    //         if(item.position.X < location.X - 128) content.Remove(item);
    //         else if(item.position.X > location.X + 128) content.Remove(item);
    //         else if(item.position.Y < location.Y - 128) content.Remove(item);
    //         else if(item.position.Y > location.Y + 128) content.Remove(item);
    //     }
    // }

    public struct Content {

        public Vector2 position;
        public float rotation;
        public int spriteID;
        public bool isFlipped;
        public bool isNessie;
        public bool isBoat;
    }
}
