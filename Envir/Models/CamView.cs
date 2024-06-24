using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class CamView : IMove {

    Random rand = new();

    public long GroupID;                                                                                     // The game the photo was taken in

    public Vector2 position = Vector2.Zero;
    public Vector2 offset = Vector2.Zero;
    public Rectangle boundaryBox => new Rectangle((int)position.X + (int)offset.X, (int)position.Y + (int) offset.Y, TLib.CameraView.Width, TLib.CameraView.Height);
    public Color colour;
    int maxPhotos;
    public int remainingPhotos;
    public Photo[] photos;
    public string playerName = string.Empty;
    float camMaxDistance = 650;

    public CamView(Color colour, int maxPhotos) {

        //this.position = position;                                                                         // Position of the camera view within the game world
        this.colour = colour;                                                                               // The players colour
        this.maxPhotos = maxPhotos;                                                                         // The maximum number of photos the player can take
        this.remainingPhotos = maxPhotos;                                                                   // The number of photos the player has left (set this to the max number of photos at the start of the game)
        offset = new Vector2(rand.Next(-500,500), rand.Next(-500,500));                                     // Random offset for the camera view

        if (maxPhotos < 26)                                                                                 // If the max photos is set to unlimited
            photos = new Photo[maxPhotos];                                                                  // Create an array of photos
    }

    #region IMove Interface

    public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Friction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MaxSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MinSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Move(GameTime gameTime) {
        //position += (Velocity * 1000) * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    #endregion

    public void Update(GameTime gameTime) {

        offset.X = Math.Clamp(offset.X, -camMaxDistance - boundaryBox.Width, camMaxDistance);
        offset.Y =  Math.Clamp(offset.Y, -camMaxDistance - boundaryBox.Height, camMaxDistance);
    }

    public bool TakePhoto(float bgOpacity) {

        // Ain't got no photos, ain't getting no picture bud!
        if (remainingPhotos <= 0)
            return false;

        // Don't save in Unlimted mode but still allow the photo to be taken
        if (maxPhotos > 26 )
            return true;

        int nextPhotoIndex = Array.FindIndex(photos, p => p == null);
        // Create a new Photo object and fill in the details
        photos[nextPhotoIndex] = new() {

            bgOpacity = bgOpacity,                                                                          // Set the background opacity
            location = position + offset,                                                                   // Set the location of the photo to the camera view position
            timeStamp = DateTime.Now,                                                                       // Set the time stamp to the current time
            photographer = playerName,                                                                      // Set the photographer to the player's name
            GroupID = GroupID                                                                               // Set the group ID to the current game ID
        };

        // Loop through all the flotsam in the game and add them to the photo
        foreach (Flotsam flotsam in GameState.Flotsam) {

            if (!flotsam.isAlive) continue;                                                                 // If the flotsam is not alive, skip it

            photos[nextPhotoIndex].content.Add(new Photo.Content() {

                scale = flotsam.scale,                                                                      // Set the scale of the flotsam
                position = flotsam.Position,                                                                // Set the position of the flotsam
                rotation = flotsam.sprite.GetRotation(),                                                    // Set the rotation of the flotsam
                spriteID = flotsam.spriteIndex,                                                             // Set the sprite ID of the flotsam
                isFlipped = flotsam.Velocity.X < 0,                                                         // Set the flip of the flotsam based on the velocity
                isNessie = flotsam.PlayerControlled                                                         // Set the isNessie flag to true if the flotsam is Nessie
            });
        }

        foreach (Cloud cloud in GameState.Clouds) {

            photos[nextPhotoIndex].clouds.Add(new Cloud(cloud.position, cloud.scale, cloud.speed, cloud.index, cloud.spriteEffect, false));
        }

        photos[nextPhotoIndex].content.Add(new Photo.Content() {

            position = GameState.Boat.Position,
            rotation = GameState.Boat.rotation,
            spriteID = -1,
            isFlipped = GameState.Boat.Velocity.X < 0,
            isBoat = true
        });

        photos[nextPhotoIndex].Save();                                                                      // Save the photo to the disk

        remainingPhotos--;                                                                                  // Decrement the number of remaining photos

        return true;
    }

    public void Reset() {

        remainingPhotos = maxPhotos;                                                                        // Reset the number of remaining photos to the max number of photos

        if (maxPhotos < 26)                                                                                 // If the max photos is NOT set to unlimited
            photos = new Photo[maxPhotos];                                                                  // Create an array of photos, ignore if unlimited as the photos are not saved
    }

    public void Draw(SpriteBatch spriteBatch) {

        // draw the cam view boundary box
        spriteBatch.Draw(TLib.Pixel, boundaryBox, Color.White * (remainingPhotos <= 0 ? 0.05f : 0.5f));         // Draw the camera view boundary box (rectangle)
        spriteBatch.Draw(TLib.CameraView, position + offset, Color.White * (remainingPhotos <= 0 ? 0.1f : 1f)); // Draw the camera view (texture

        // Remaining photos
        if (maxPhotos < 26) {
            spriteBatch.DrawString(FLib.LeaderboardFont, $"{remainingPhotos}/{maxPhotos}", position + offset + new Vector2(TLib.CameraView.Width - FLib.LeaderboardFont.MeasureString($"{remainingPhotos}/{maxPhotos}").X+1, TLib.CameraView.Height + 5), Color.Black);
            spriteBatch.DrawString(FLib.LeaderboardFont, $"{remainingPhotos}/{maxPhotos}", position + offset + new Vector2(TLib.CameraView.Width - FLib.LeaderboardFont.MeasureString($"{remainingPhotos}/{maxPhotos}").X, TLib.CameraView.Height + 4), Color.White);
        }
    }
}
