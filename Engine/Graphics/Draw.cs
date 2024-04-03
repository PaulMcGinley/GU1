using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Engine.Graphics;

public static class Draw {

    /// <summary>
    /// Draw a line between two points
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="spriteBatch"></param>
    public static void DrawLine(Vector2 start, Vector2 end, SpriteBatch spriteBatch, Color color = default) {

        float distance = Vector2.Distance(start, end);
        float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
        Vector2 scale = new(distance, 1);
        Vector2 origin = new(0, 0.5f);

        spriteBatch.Draw(TLib.Pixel, start, null, color, angle, origin, scale, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Draw a rectangle filled with a color
    /// </summary>
    /// <param name="rectangle"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="color"></param>
    public static void DrawFilledRectangle(Rectangle rectangle, SpriteBatch spriteBatch, Color color = default) => spriteBatch.Draw(TLib.Pixel, rectangle, color);

    /// <summary>
    /// Draw a rectangle outline
    /// </summary>
    /// <param name="rectangle"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="color"></param>
    /// <param name="thickness"></param>
    public static void DrawRectangle(Rectangle rectangle, SpriteBatch spriteBatch, Color color = default, int thickness = 1) {

        DrawLine(new Vector2(rectangle.Left, rectangle.Top), new Vector2(rectangle.Right, rectangle.Top), spriteBatch, color);
        DrawLine(new Vector2(rectangle.Right, rectangle.Top), new Vector2(rectangle.Right, rectangle.Bottom), spriteBatch, color);
        DrawLine(new Vector2(rectangle.Right, rectangle.Bottom), new Vector2(rectangle.Left, rectangle.Bottom), spriteBatch, color);
        DrawLine(new Vector2(rectangle.Left, rectangle.Bottom), new Vector2(rectangle.Left, rectangle.Top), spriteBatch, color);
    }

    /// <summary>
    /// Draw a triangle outline
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <param name="point3"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="color"></param>
    public static void DrawTriangle(Vector2 point1, Vector2 point2, Vector2 point3, SpriteBatch spriteBatch, Color color = default) {

        DrawLine(point1, point2, spriteBatch, color);
        DrawLine(point2, point3, spriteBatch, color);
        DrawLine(point3, point1, spriteBatch, color);
    }

    /// <summary>
    /// Draw an arc outline
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="startAngle"></param>
    /// <param name="endAngle"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="color"></param>
    public static void DrawArc(Vector2 center, float radius, float startAngle, float endAngle, SpriteBatch spriteBatch, Color color = default) {

        for (int i = (int)startAngle; i < endAngle; i++) {

            float x1 = center.X + (float)Math.Cos(MathHelper.ToRadians(i)) * radius;
            float y1 = center.Y + (float)Math.Sin(MathHelper.ToRadians(i)) * radius;
            float x2 = center.X + (float)Math.Cos(MathHelper.ToRadians(i + 1)) * radius;
            float y2 = center.Y + (float)Math.Sin(MathHelper.ToRadians(i + 1)) * radius;

            DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), spriteBatch, color);
        }
    }

    /// <summary>
    /// Draw a circle outline
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="color"></param>
    public static void DrawCircle(Vector2 center, float radius, SpriteBatch spriteBatch, Color color = default) => DrawArc(center, radius, 0, 360, spriteBatch, color);

    /// <summary>
    /// NOTE: WORK IN PROGRESS
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="curvature"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="color"></param>
    public static void DrawCurvedLine(Vector2 start, Vector2 end, float curvature, SpriteBatch spriteBatch, Color color = default) {

        Vector2 mid = new((start.X + end.X) / 2, (start.Y + end.Y) / 2);
        Vector2 control = new(mid.X, mid.Y + curvature);

        Vector2 lastPoint = start;
        for (float i = 0; i < 1f; i += 0.01f) {

            Vector2 p1 = Vector2.Lerp(start, control, i);
            Vector2 p2 = Vector2.Lerp(control, end, i);
            Vector2 p3 = Vector2.Lerp(p1, p2, i);

            // If this is the first point, assign the lastPoint and skip drawing the line
            if (i == 0) {
                lastPoint = p3;
                continue;
            }

            //spriteBatch.Draw(TLib.Pixel, p3, color);
            DrawLine(lastPoint, p3, spriteBatch, color);

            lastPoint = p3; // Update the last point to the current point for the next iteration
        }
    }

    /// <summary>
    /// NOTE: WORK IN PROGRESS
    /// </summary>
    /// <param name="points">Range: 100 - 10000000</param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="curvature"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="color"></param>
    public static void DrawNPointCurvedLine(int points, Vector2 start, Vector2 end, float curvature, SpriteBatch spriteBatch, Color color = default) {

        Vector2 mid = new((start.X + end.X) / 2, (start.Y + end.Y) / 2);
        Vector2 control = new(mid.X, mid.Y + curvature);

        float pFloat = (float)points/100;                                          // Convert the points to a float for the loop

        for (float i = 0; i < pFloat; i += 0.01f) {

            Vector2 p1 = Vector2.Lerp(start, control, i/pFloat);
            Vector2 p2 = Vector2.Lerp(control, end, i/pFloat);
            Vector2 p3 = Vector2.Lerp(p1, p2, i/pFloat);

            spriteBatch.Draw(TLib.Pixel, p3, color);
        }
    }

    /// <summary>
    /// NOTE: WORK IN PROGRESS
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="curvature"></param>
    /// <param name="spriteBatch"></param>
    /// <param name="color"></param>
    public static void DrawSolidCurvedLine(Vector2 start, Vector2 end, float curvature, SpriteBatch spriteBatch, Color color = default) {

        Vector2 mid = new((start.X + end.X) / 2, (start.Y + end.Y) / 2);
        Vector2 control = new(mid.X, mid.Y + curvature);

        for (float i = 0; i < 1; i += 0.01f) {

            Vector2 p1 = Vector2.Lerp(start, control, i);
            Vector2 p2 = Vector2.Lerp(control, end, i);
            Vector2 p3 = Vector2.Lerp(p1, p2, i);

            spriteBatch.Draw(TLib.Pixel, p3, color);
            DrawLine(start, p1, spriteBatch, color);
            DrawLine(p1, p3, spriteBatch, color);
            DrawLine(p3, p2, spriteBatch, color);
            DrawLine(p2, end, spriteBatch, color);
        }
    }
}
