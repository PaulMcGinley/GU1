using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GU1.Engine.Models.Sequences;

public class Rumble
{
    int index = 0;
    int Index {
        get => index;
        set {
            if (value < 0) {
                index = 0;
            } else if (value >= Beats.Count) {
                index = Beats.Count - 1;
            } else {
                index = value;
            }
        }
    }

    public bool IsPlaying { get; set; }
    public bool IsPaused { get; set; }
    public bool IsLooping { get; set; }
    public bool IsReversed { get; set; }
    public bool IsRandom { get; set; }

    public float previousTime = 0;

    public List<Frame> Beats = new();

    public void Play() {

        IsPlaying = true;
        IsPaused = false;
    }

    public void Stop() {

        IsPlaying = false;
        IsPaused = false;
        Index = 0;
    }

    public void Pause() {

        IsPaused = true;
    }

    public void Resume() {

        IsPaused = false;
    }

    public void Next() {

        if (IsRandom)
            Index = RandomInteger(Beats.Count);        
        else if (IsReversed)
            Index--;
        else
            Index++;
    }

    public void Previous() {

    }

    public void Update(GameTime gameTime) {

        if (previousTime == 0)
            previousTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

        float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
        float frameTime = now - previousTime;

        if (frameTime > Beats[Index].Duration)
            frameTime = Beats[Index].Duration;

        previousTime = now;

        if (IsPlaying && !IsPaused)
           Rumble(0, Beats[Index].LeftMotor, Beats[Index].RightMotor);

        if (IsPlaying && !IsPaused && frameTime >= Beats[Index].Duration) {
            if (IsReversed) {
                Index--;
            } else {
                Index++;
            }
        }
    }

    public class Frame {

        public float LeftMotor { get; set; }
        public float RightMotor { get; set; }
        public int Duration { get; set; } // time in milliseconds
    }
}
