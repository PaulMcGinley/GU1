using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Photo {

    public static string SaveDir; //=> $"{Directory.GetCurrentDirectory()}/Photos";

    #region Data

    public long GroupID;                                                                                    // The game the photo was taken in

    public Vector2 location;                                                                                // On the map
    public string fileName = string.Empty;                                                                  // Save file name
    public string photographer;                                                                             // The name of the player who took the photo
    public DateTime timeStamp;                                                                              // The time the photo was taken
    public string Date => timeStamp.ToString("yyyy-MM-dd");                                                 // The date the photo was taken (Extracted from the timeStamp)
    public string Time => timeStamp.ToString("HH:mm");                                                      // The time the photo was taken (Extracted from the timeStamp)

    public List<Content> content = new();                                                                   // The content of the photo
    public List<Cloud> clouds = new();                                                                      // The clouds in the photo

    public float bgOpacity = 0f;                                                                            // The opacity of the background

    #endregion

    #region Texture

    [XmlIgnore]
    public RenderTarget2D fullPicture;                                                                      // The render target for the photo

    [XmlIgnore]
    public RenderTarget2D croppedPicture;                                                                   // The cropped picture

    [XmlIgnore]
    public RenderTarget2D framedPicture;                                                                    // The framed picture

    #endregion

    public void Dispose() {

        fullPicture.Dispose();
        GC.Collect();
    }

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

    public void RenderFullPicture(SpriteBatch spriteBatch) {

        fullPicture?.Dispose();                                                                             // Null check and dispose of the current render target ( ?. is the null propagation operator )

        // set render target
        fullPicture = new RenderTarget2D(spriteBatch.GraphicsDevice, 1920*2, 1080*2);
        spriteBatch.GraphicsDevice.SetRenderTarget(fullPicture);
        spriteBatch.GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

        // Draw the background
        spriteBatch.Draw(TLib.PlayingBackground[0], new Rectangle(0,0,1920*2,1080*2), new Rectangle(0,0,1920,1080), Color.White);
        spriteBatch.Draw(TLib.PlayingBackground[1], new Rectangle(0,0,1920*2,1080*2), new Rectangle(0,0,1920,1080), Color.White * bgOpacity, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

        // Sort the sprites so they are drawn in the correct order
        content.Sort((a, b) => a.position.X.CompareTo(b.position.X));
        content.Sort((a, b) => a.position.Y.CompareTo(b.position.Y));

        // Draw the content
        foreach (var entity in content) {

            if (entity.isBoat)
                spriteBatch.Draw(TLib.Boat[entity.isFlipped ? 1 : 0], entity.position + new Vector2(1920/2,1080/2), null, Color.White, entity.rotation, new Vector2(128, 128), 0.75f, SpriteEffects.None, 0);

            else if (entity.isNessie)
                spriteBatch.Draw(TLib.TheNessie, entity.position+ new Vector2(1920/2,1080/2), null, Color.White, entity.rotation, new Vector2(128, 128), entity.scale, entity.isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            else
                spriteBatch.Draw(TLib.Flotsam[entity.spriteID], entity.position+ new Vector2(1920/2,1080/2), null, Color.White, entity.rotation, new Vector2(128, 128), entity.scale, entity.isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        foreach (var cloud in clouds)
            cloud.position += new Vector2(1920/2, 1080/2);


        // Draw the clouds
        foreach (var cloud in clouds)
            cloud.Draw(spriteBatch);

        spriteBatch.End();

        spriteBatch.GraphicsDevice.SetRenderTarget(null);
    }

    public void RenderCroppedPicture(SpriteBatch spriteBatch) {

        if (croppedPicture != null)
            croppedPicture.Dispose();

        croppedPicture = new RenderTarget2D(spriteBatch.GraphicsDevice, 256, 256);
        spriteBatch.GraphicsDevice.SetRenderTarget(croppedPicture);
        spriteBatch.GraphicsDevice.Clear(Color.White);

        spriteBatch.Begin();

        spriteBatch.Draw(fullPicture, new Rectangle(0, 0, 256, 256), new Rectangle((int)location.X +(1920/2)-128 , (int)location.Y - 128 + (1080/2), 256, 256), Color.White);

        spriteBatch.End();

        spriteBatch.GraphicsDevice.SetRenderTarget(null);
    }

    public void RenderFramedPicture(SpriteBatch spriteBatch) {

        if (framedPicture != null)
            framedPicture.Dispose();

        int width = croppedPicture.Width + 50;
        int height = croppedPicture.Height + 125;

        framedPicture = new RenderTarget2D(spriteBatch.GraphicsDevice, width, height);

        spriteBatch.GraphicsDevice.SetRenderTarget(framedPicture);
        spriteBatch.GraphicsDevice.Clear(Color.White);

        spriteBatch.Begin();

        // Outer Border
        DrawRectangle(new Rectangle(1, 1, framedPicture.Width-1, framedPicture.Height-1), spriteBatch, Color.DarkGray, 0);
        DrawRectangle(new Rectangle(2, 2, framedPicture.Width-3, framedPicture.Height-3), spriteBatch, Color.DarkGray, 0);


        // Photograph
        spriteBatch.Draw(croppedPicture, new Vector2(25, 25), Color.White);

        // Photograph Border
        DrawRectangle(new Rectangle(20, 20, croppedPicture.Width+10, croppedPicture.Height+10), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(21, 21, croppedPicture.Width+8, croppedPicture.Height+8), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(22, 22, croppedPicture.Width+6, croppedPicture.Height+6), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(23, 23, croppedPicture.Width+4, croppedPicture.Height+4), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(24, 24, croppedPicture.Width+2, croppedPicture.Height+2), spriteBatch, Color.Black, 0);
        DrawRectangle(new Rectangle(25, 25, croppedPicture.Width, croppedPicture.Height), spriteBatch, Color.Black, 0);

        // Writing
        // spriteBatch.DrawString(FLib.DebugFont, $"Captured by {photographer}", new Vector2(25, 25 + croppedPicture.Height + 15), Color.DarkRed);
        // spriteBatch.DrawString(FLib.DebugFont, $"Date: {Date}", new Vector2(25, 25 + croppedPicture.Height + 35), Color.DarkRed);
        // spriteBatch.DrawString(FLib.DebugFont, $"Time: {Time}", new Vector2(25, 25 + croppedPicture.Height + 55), Color.DarkRed);

        Vector2 nameSize = FLib.PhotoNameFont.MeasureString($"{photographer}");
        Vector2 dateSize = FLib.PhotoDateFont.MeasureString($"{Date} / {Time}");

        spriteBatch.DrawString(FLib.PhotoNameFont, $"{photographer}", new Vector2(25, 25 + croppedPicture.Height + 15), Color.Black);
        spriteBatch.DrawString(FLib.PhotoDateFont, $"{Date} / {Time}", new Vector2(width - dateSize.X - 15, height -dateSize.Y - 15), Color.Black);

        spriteBatch.End();

        spriteBatch.GraphicsDevice.SetRenderTarget(null);
    }

    public void Render(SpriteBatch spriteBatch) {

        if (fullPicture == null)
            RenderFullPicture(spriteBatch);

        if (croppedPicture == null)
            RenderCroppedPicture(spriteBatch);

        if (framedPicture == null)
            RenderFramedPicture(spriteBatch);
    }

    public struct Content {

        public Vector2 position;
        public float rotation;
        public int spriteID;
        public bool isFlipped;
        public bool isNessie;
        public bool isBoat;
        public float scale;
    }
}
