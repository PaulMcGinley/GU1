﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class CamView : IMove {

    public Vector2 position = Vector2.Zero;
    public Vector2 offset = Vector2.Zero;
    Color colour;
    int maxPhotos;
    int remainingPhotos;

    // Rectangle CameraFrame => new ((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
    // Rectangle PNumberBox => new ((int)position.X - 20, (int)position.Y, 20, 20);

    public CamView(Color colour, int maxPhotos) {

        this.position = position;                                                                           // Position of the camera view within the game world
        this.colour = colour;                                                                               // The players colour
        this.maxPhotos = maxPhotos;                                                                         // The maximum number of photos the player can take
        this.remainingPhotos = maxPhotos;                                                                   // The number of photos the player has left (set this to the max number of photos at the start of the game)
    }

    #region IMove Interface

    public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Friction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MaxSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float MinSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Move(GameTime gameTime) {

        position += Velocity;
    }

    #endregion

    public void Update(GameTime gameTime) {

         Math.Clamp(offset.X, -500, 500);
         Math.Clamp(offset.Y, -500, 500);

    }

    public void TakePhoto() {

        if (remainingPhotos > 0)
            remainingPhotos--;

        // Play the camera sound

        // Flash the screen white

        // Save the photo to the photo book
    }

    public void Draw(SpriteBatch spriteBatch) {

        // Draw the camera frame border
        // DrawRectangle(CameraFrame, spriteBatch, colour, 1);

        // // Draw the player number box
        // DrawFilledRectangle(PNumberBox, spriteBatch, colour);
        // spriteBatch.DrawString(FLib.DebugFont, remainingPhotos.ToString(), new Vector2(position.X +5, position.Y - 15), Color.White);

        spriteBatch.Draw(TLib.CameraView, position + offset, Color.White); // Draw the camera view (texture
    }

    //public void DrawCutout(SpriteBatch spriteBatch) => DrawFilledRectangle(CameraFrame, spriteBatch, Color.Transparent);

}
