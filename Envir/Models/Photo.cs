using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Photo {

    string SaveDir => $"{Directory.GetCurrentDirectory()}/Photos";

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

    [System.Xml.Serialization.XmlIgnore]
    public RenderTarget2D renderTarget;                                                                            // The render target for the photo

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

        if (fileName == null)
            fileName = $"{DateTime.Now.ToBinary()}";

        string _path = $"{SaveDir}/{fileName}.xml";

        if (!Directory.Exists(SaveDir))
            Directory.CreateDirectory(SaveDir);

        // Save the photo to the path
        XMLSerializer.Serialize<Photo>(_path, this);
    }

    public void Render(SpriteBatch spriteBatch) {

        // set render target
        renderTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, 1920, 1080);
        spriteBatch.GraphicsDevice.SetRenderTarget(renderTarget);
        spriteBatch.GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

        // Draw the background
        spriteBatch.Draw(TLib.PlayingBackground[0], new Vector2(0, 0), Color.White);

        // sort content by Y position
        content.Sort((a, b) => a.position.Y.CompareTo(b.position.Y));

        // sort content by X position
        content.Sort((a, b) => a.position.X.CompareTo(b.position.X));

        // Draw the content
        foreach (var item in content) {

            if (item.isBoat) {

                spriteBatch.Draw(TLib.Boat[item.isFlipped ? 1 : 0], item.position, null, Color.White, item.rotation, new Vector2(128, 128), 0.5f, item.isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            else if (item.isNessie) {

                spriteBatch.Draw(TLib.TheNessie, item.position, null, Color.White, item.rotation, new Vector2(128, 128), 0.5f, item.isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            else {

                spriteBatch.Draw(TLib.Flotsam[item.spriteID], item.position, null, Color.White, item.rotation, new Vector2(128, 128), 0.5f, item.isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
        }

        // Draw the photo
       // spriteBatch.Draw(renderTarget, location, Color.White);

        spriteBatch.End();

        // reset render target
        spriteBatch.GraphicsDevice.SetRenderTarget(null);

        // spriteBatch.Begin();
        // // Draw the photo cropped to the camera view
        // spriteBatch.Draw(renderTarget, new Vector2(0,0), new Rectangle((int)location.X, (int)location.Y, 128, 128), Color.White, 0, new Vector2(64, 64), 1, SpriteEffects.None, 0);

        // spriteBatch.End();
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
