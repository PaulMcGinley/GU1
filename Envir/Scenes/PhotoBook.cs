using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Scenes;

public class PhotoBook : IScene {

    Random  rand = new();

    Photo photo;
    SpriteBatch spriteBatch;

    Photo[] photos;
    Vector2[] photoLocations;
    float[] photoRotations;


    public void Set(SpriteBatch spriteBatch) {

            this.spriteBatch = spriteBatch;
    }

    public void Initialize(GraphicsDevice device) {

        photo = new Photo();
        photo = photo.Load("-8584844245441848118");

        photo.Render(spriteBatch);

        string directoryPath = photo.SaveDir; // your directory path
        var regex = new Regex(@"^-\d+\.xml$");

        string[] files = Directory.EnumerateFiles(directoryPath, "*.xml")
                                .Where(path => regex.IsMatch(Path.GetFileName(path)))
                                .Select(path => Path.GetFileNameWithoutExtension(path))
                                .ToArray();



        photos = new Photo[files.Length];
        photoLocations = new Vector2[files.Length];
        photoRotations = new float[files.Length];

        for (int i = 0; i < files.Length; i++) {

            photos[i] = new Photo();
            photos[i] = photos[i].Load(files[i]);
            photos[i].Render(spriteBatch);
            photoLocations[i] = new Vector2(rand.Next(0, 1920), rand.Next(0, 1080));
            photoRotations[i] = (float)rand.NextDouble();
        }
    }

    public void LoadContent(ContentManager content) {

    }

    public void UnloadContent() {

    }

    public void Update(GameTime gameTime) {

    }

    public void FixedUpdate(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.Draw(photos[0].fullPicture, new Rectangle(0, 0, 1920, 1080), Color.White); // TODO: Make the photo return a polaroid or just the picture

        // spriteBatch.Draw(TLib.Pixel , new Rectangle((1920/2)-150, (1080/2) - 150, 300, 350), Color.White); // TODO: Make the photo return a polaroid or just the picture
        // spriteBatch.Draw(photo.fullPicture, new Vector2(1920/2,1080/2), new Rectangle((int)photo.location.X -128, (int)photo.location.Y -128, 256, 256), Color.White, 0, new Vector2(128, 128), 1, SpriteEffects.None, 0);

        for (int i = 0; i < photos.Length; i++)
            spriteBatch.Draw(photos[i].framedPicture, photoLocations[i], null, Color.White, photoRotations[i], new Vector2(0, 0), 1, SpriteEffects.None, 0);

        // foreach (var photo in photos)
        //     spriteBatch.Draw(photo.framedPicture, new Vector2(rand.Next(0, 1920), rand.Next(0, 1080)), null, Color.White, (float)rand.NextDouble(), new Vector2(0, 0), 1, SpriteEffects.None, 0);




        spriteBatch.End();
    }

    public void OnSceneStart() {

    }

    public void OnSceneEnd() {

    }
}
