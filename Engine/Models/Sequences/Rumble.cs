using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GU1.Engine.Models.Sequences;

public class Rumble
{
    int index = 0;
    int Index {
        get => index;
        set {
            if (value < 0)
                index = Steps.Count - 1;
            else if (value >= Steps.Count)
                index = 0;
            else
                index = value;
        }
    }

    bool IsPlaying { get; set; } = false;
    bool IsPaused { get; set; }  = false;
    bool IsLooping { get; set; } = false;
    bool IsReversed { get; set; } = false;
    bool IsRandom { get; set; } = false;

    float previousTime = 0;

    public float PlaySpeed { get; set; } = 1f;                                                              // 1f = normal speed, 0.5f = half speed, 2f = double speed

    #region Controls

    /// <summary>
    /// Start playing the rumble sequence
    /// </summary>
    public void Play() {

        IsPlaying = true;
        IsPaused = false;
    }

    /// <summary>
    /// Stop playing the rumble sequence and reset the index
    /// </summary>
    public void Stop() {

        IsPlaying = false;
        IsPaused = false;
        Index = 0;
    }

    /// <summary>
    /// Pause the rumble sequence at the current index
    /// </summary>
    public void Pause() => IsPaused = true;

    /// <summary>
    /// Resume the rumble sequence from the current index
    /// </summary>
    public void Resume() => IsPaused = false;

    /// <summary>
    /// Progress to the next step in the sequence
    /// </summary>
    void Next() {

        if (IsRandom)
            Index = RandomInteger(Steps.Count - 1);
        else if (IsReversed)
            Index--;
        else
            Index++;
    }

    /// <summary>
    /// Go back to the previous step in the sequence
    /// </summary>
    void Previous() {

        if (IsRandom)
            Index = RandomInteger(Steps.Count - 1);
        else if (IsReversed)
            Index++;
        else
            Index--;
    }

    #endregion


    public void Update(GameTime gameTime) {

        if (previousTime == 0)
            previousTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

        float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
        float frameTime = now - previousTime;

        if (frameTime > Steps[Index].Duration)
            frameTime = Steps[Index].Duration;

        previousTime = now;

        if (IsPlaying && !IsPaused)
           Rumble(0, Steps[Index].LeftMotor, Steps[Index].RightMotor);

        if (IsPlaying && !IsPaused && frameTime >= Steps[Index].Duration) {
            if (IsReversed)
                Previous();
            else
                Next();
        }
    }

    #region Sequence sections

    /// <summary>
    /// Add a single step within the sequence
    /// </summary>
    public class Step {

        public float LeftMotor { get; set; } = 0f;
        public float RightMotor { get; set; } = 0f;
        public int Duration { get; set; } = 1; // time in milliseconds
    }

    /// <summary>
    /// List of rumble steps
    /// </summary>
    public List<Step> Steps = new();

    #endregion
}
