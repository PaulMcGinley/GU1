using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class FancyNumber {

    public uint Value = 0;
    public float Scale = 0.2f;

    public FancyNumber(uint value) {
        Value = value;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color colour = default) {

        string value = Value.ToString();

        float totalWidth = 0;

        for (int i = 0; i < value.Length; i++)
            totalWidth += TLib.Numbers[int.Parse(value[i].ToString())].Width;

        float x = position.X - totalWidth * Scale / 2;

        for (int i = 0; i < value.Length; i++) {

            int num = int.Parse(value[i].ToString());

            spriteBatch.Draw(TLib.Numbers[num], new Vector2(x, position.Y), null, colour, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);

            x += TLib.Numbers[num].Width * Scale;
        }

    }

}
