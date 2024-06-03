using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GU1;

public static class RumbleQueue {

    static List<Rumble> rumbles = new();

    public static void AddRumble(int controllerIndex,  double startTime, double duration,float leftMotor, float rightMotor) {

        rumbles.Add(new Rumble {

            ControllerIndex = controllerIndex,
            EndTime = startTime+duration,
            StartTime = startTime,
            LeftMotor = leftMotor,
            RightMotor = rightMotor,
            Expired = false
        });
    }

    public static void Update(GameTime gameTime) {

        double currentTime = gameTime.TotalGameTime.TotalMilliseconds;

        // Handle rumble
        foreach (var rumble in rumbles) {

            // Start rumbles
            if (currentTime >= rumble.StartTime && !rumble.Expired)
                GamePad.SetVibration((PlayerIndex)rumble.ControllerIndex, rumble.LeftMotor, rumble.RightMotor);

            // Stop rumbles
            if (currentTime >= rumble.EndTime && !rumble.Expired) {
                GamePad.SetVibration((PlayerIndex)rumble.ControllerIndex, 0, 0);
                rumble.Expired = true;
            }
        }
    }

    public static void StopAll() {

        for (int i = 0; i < rumbles.Count; i++)
            GamePad.SetVibration((PlayerIndex)rumbles[i].ControllerIndex, 0, 0);
    }

    class Rumble {

        public int ControllerIndex;
        public double EndTime;
        public double StartTime;
        public float LeftMotor;
        public float RightMotor;
        public bool Expired;
    }
}
