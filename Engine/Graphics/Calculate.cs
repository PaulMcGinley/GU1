using Microsoft.Xna.Framework;

namespace GU1.Engine.Graphics;

public static class Calculate {

    /// <summary>
    /// Calculate the percentage of the area of the sprite that is inside the camera view.
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="cameraView"></param>
    /// <returns></returns>
    public static int SpriteInRectanglePercentage(Rectangle sprite, Rectangle cameraView) {

        Rectangle intersection = Rectangle.Intersect(sprite, cameraView);                                   // Get the intersection between the sprite and the camera view

        int totalArea = sprite.Width * sprite.Height;                                                       // Calculate the total area of the sprite
        int intersectedArea = intersection.Width * intersection.Height;                                     // Calculate the area of the intersection

        int percentage = (int)((float)intersectedArea / totalArea * 100);                                   // Calculate the percentage of the area of the sprite that is inside the camera view
        return percentage * 10;                                                                             // Return the percentage as an integer multiplied by 10 for a maximum of 1000 score
    }

    /// <summary>
    /// Calculate the percentage of the area of the sprites that is inside the camera view.
    /// </summary>
    /// <param name="sprites">Nessie sprite rectangle</param>
    /// <param name="cameraView">Player camera rectangle (player who took the picture)</param>
    /// <returns></returns>
    public static int SpritesInRectanglePercentage(Rectangle[] sprites, Rectangle cameraView) {

        int totalArea = 0;                                                                                  // Total area of the sprites
        int intersectedArea = 0;                                                                            // Area of the intersections

        foreach (Rectangle sprite in sprites) {

            Rectangle intersection = Rectangle.Intersect(sprite, cameraView);                               // Get the intersection between the sprite and the camera view

            totalArea += sprite.Width * sprite.Height;                                                      // Calculate the total area of the sprite
            intersectedArea += intersection.Width * intersection.Height;                                    // Calculate the area of the intersection
        }

        int percentage = (int)((float)intersectedArea / totalArea * 100);                                   // Calculate the percentage of the area of the sprite that is inside the camera view
        return percentage * 10;                                                                             // Return the percentage as an integer
    }
}
