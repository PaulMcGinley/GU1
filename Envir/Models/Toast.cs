using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Toast : IGameObject {

    public Vector2 Position { get; set; } = Vector2.Zero;
    public string Title { get; set; }
    public string Description { get; set; }
    public string Caption { get; set; }
    public int IconIndex { get; set; } = 0;
    public Color Colour { get; set; } = Color.White;
    public float Width { get; set; } = 400;
    // public DateTime awardedAt { get; set; } = DateTime.MinValue;

    public Color IconColour { get; set; } = Color.White * 0.9f;
    public Color TextColour { get; set; } = Color.White * 0.95f;

    public Toast(Vector2 position) {

        Position = position;
    }

    public void Initialize(GraphicsDevice device) {}
    public void LoadContent(ContentManager content) {}
    public void Update(GameTime gameTime) {}
    public void FixedTimestampUpdate(GameTime gameTime) {}
    public void Draw(SpriteBatch spriteBatch) {

        // Shape
        spriteBatch.Draw(TLib.Circle_80px, new Rectangle((int)Position.X, (int)Position.Y, 40, 80), new Rectangle(0, 0, 40, 80), Colour);  //left semi circle
        DrawFilledRectangle(new Rectangle((int)Position.X + 40, (int)Position.Y, (int)Width - 80, 80), spriteBatch, Colour);                             //rectangle
        spriteBatch.Draw(TLib.Circle_80px, new Rectangle((int)Position.X + (int)Width - 40, (int)Position.Y, 40, 80), new Rectangle(40, 0, 40, 80), Colour);    //right semi circle

        // Content
        spriteBatch.Draw(TLib.AchievementIcons[IconIndex], new Rectangle((int)Position.X + 24, (int)Position.Y + 16, 48, 48), IconColour);     //icon

        if (Title != null)
            spriteBatch.DrawString(FLib.AchievementTitleFont, Title, new Vector2(Position.X + 96, Position.Y + 8), TextColour);                 //name

        if (Description != null)
            spriteBatch.DrawString(FLib.AchievementDescriptionFont, Description, new Vector2(Position.X + 96, Position.Y + 48), TextColour);         //description

        if (Caption != null)
            spriteBatch.DrawString(FLib.AchievementCaptionFont, Caption, new Vector2(Position.X + 96, Position.Y + 40 - (FLib.AchievementCaptionFont.MeasureString(Caption).Y/2)), TextColour);                 //caption

        // if (awardedAt == DateTime.MinValue)
        //     spriteBatch.DrawString(FLib.AchievementDescriptionFont, $"{awardedAt.Date.ToShortDateString()} - {awardedAt.ToShortTimeString()}", new Vector2(Position.X + Width - FLib.AchievementDescriptionFont.MeasureString($"{awardedAt.Date.ToShortDateString()} - {awardedAt.ToShortTimeString()}").X, Position.Y + 40), TextColour);                 //caption

    }
}
