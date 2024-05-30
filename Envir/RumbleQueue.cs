using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GU1;

public static class RumbleQueue {

    static List<Rumble> rumbles = new();

    public static void AddRumble(int controllerIndex, double duration, double startTime, float leftMotor, float rightMotor) {
        rumbles.Add(new Rumble {
            ControllerIndex = controllerIndex,
            EndTime = startTime+duration,
            StartTime = startTime,
            LeftMotor = leftMotor,
            RightMotor = rightMotor
        });
    }

    public static void Update(GameTime gameTime) {

        if (rumbles.Count == 0) return;

        double currentTime = gameTime.TotalGameTime.TotalMilliseconds;

        for (int i = rumbles.Count -1 ; i >= 0; i--) {
            if (currentTime >= rumbles[i].EndTime) {
                GamePad.SetVibration((PlayerIndex)rumbles[i].ControllerIndex, 0, 0);
                rumbles.RemoveAt(i);
            } else {
                GamePad.SetVibration((PlayerIndex)rumbles[i].ControllerIndex, rumbles[i].LeftMotor, rumbles[i].RightMotor);
            }
        }

        // for (int i = 0; i < rumbles.Count; i++) {
        //     if (currentTime >= rumbles[i].EndTime) {
        //         GamePad.SetVibration((PlayerIndex)rumbles[i].ControllerIndex, 0, 0);
        //         rumbles.RemoveAt(i);
        //         i--;
        //     } else {
        //         GamePad.SetVibration((PlayerIndex)rumbles[i].ControllerIndex, rumbles[i].LeftMotor, rumbles[i].RightMotor);
        //     }
        // }
    }

    public static void Clear() {

        rumbles.Clear();
    }

    public static void Stop(int controllerIndex) {
        for (int i = 0; i < rumbles.Count; i++) {
            if (rumbles[i].ControllerIndex == controllerIndex) {
                GamePad.SetVibration((PlayerIndex)rumbles[i].ControllerIndex, 0, 0);
                rumbles.RemoveAt(i);
                i--;
            }
        }
    }

    public static void StopAll() {
        for (int i = 0; i < rumbles.Count; i++) {
            GamePad.SetVibration((PlayerIndex)rumbles[i].ControllerIndex, 0, 0);
        }
        rumbles.Clear();
    }

    public static void StopAllExcept(int controllerIndex) {
        for (int i = 0; i < rumbles.Count; i++) {
            if (rumbles[i].ControllerIndex != controllerIndex) {
                GamePad.SetVibration((PlayerIndex)rumbles[i].ControllerIndex, 0, 0);
                rumbles.RemoveAt(i);
                i--;
            }
        }
    }

    class Rumble {
        public int ControllerIndex;
        public double EndTime;
        public double StartTime;
        public float LeftMotor;
        public float RightMotor;
    }
}
