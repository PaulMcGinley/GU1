using System;
using System.Collections.Generic;

namespace GU1.Envir.Libraries;

public static class Rumble
{
    public static E.Models.Sequences.Rumble HeartBeat;

    public static void Initialize() {

        HeartBeat = new E.Models.Sequences.Rumble() {

            Steps = new List<E.Models.Sequences.Rumble.Step>() {

                new() { LeftMotor = 0.1f, RightMotor = 0f, Duration = 250 },
                new() { LeftMotor = 0f, RightMotor = 0f, Duration = 250 },
                new() { LeftMotor = 0f, RightMotor = 0.1f, Duration = 250 },
                new() { LeftMotor = 0f, RightMotor = 0f, Duration = 750 },
            }
        };
    }
}
