using System;

namespace GU1.Engine.Trackers;

public class ElapsedTime {

    #region Properties
    /// <summary>
    /// Assigning a name to the tracker will make the output more readable
    /// </summary>
    private string Name { get; set; }

    /// <summary>
    /// Start time of the tracker in milliseconds
    /// </summary>
    private double StartTime { get; set; }

    /// <summary>
    /// End time of the tracker in milliseconds
    /// </summary>
    private double EndTime { get; set; }

    /// <summary>
    /// The time elapsed between the start and end times in milliseconds
    /// </summary>
    private TimeSpan Time => TimeSpan.FromMilliseconds(EndTime - StartTime);

    #endregion

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    public ElapsedTime(string name = "Undefined") {

        Name = name;
        Reset();
    }

    /// <summary>
    /// Start tracking the time
    /// </summary>
    public void Start() => StartTime = DateTime.Now.TimeOfDay.TotalMilliseconds;

    /// <summary>
    /// Stop tracking the time
    /// </summary>
    public void Stop() => EndTime = DateTime.Now.TimeOfDay.TotalMilliseconds;

    /// <summary>
    /// Reset the time tracker
    /// </summary>
    public void Reset() => StartTime = EndTime = 0;

    /// <summary>
    /// Get the result of the time tracking
    /// </summary>
    public string Result => $"[{Name}] Time elapsed: {Time}";

    /// <summary>
    /// Override the ToString method to return the result
    /// </summary>
    /// <returns></returns>
    public override string ToString() => Result;

    /// <summary>
    /// Compare the time of two ElapsedTime objects
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(ElapsedTime other) => Time.CompareTo(other.Time);

    /// <summary>
    /// Check if the object is equal to another ElapsedTime object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj) => obj is ElapsedTime time && Time.Equals(time.Time);

    /// <summary>
    /// Get the hash code of the time
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => HashCode.Combine(Time);

    #region Operators

    public static bool operator <(ElapsedTime a, ElapsedTime b) => a.Time < b.Time;
    public static bool operator >(ElapsedTime a, ElapsedTime b) => a.Time > b.Time;
    public static bool operator <=(ElapsedTime a, ElapsedTime b) => a.Time <= b.Time;
    public static bool operator >=(ElapsedTime a, ElapsedTime b) => a.Time >= b.Time;
    public static bool operator ==(ElapsedTime a, ElapsedTime b) => a.Time == b.Time;
    public static bool operator !=(ElapsedTime a, ElapsedTime b) => a.Time != b.Time;

    // ? Maybe these should use resulting time instead of start and end times
    public static ElapsedTime operator +(ElapsedTime a, ElapsedTime b) => new($"{a.Name} + {b.Name}") { StartTime = a.StartTime, EndTime = b.EndTime };
    public static ElapsedTime operator -(ElapsedTime a, ElapsedTime b) => new($"{a.Name} - {b.Name}") { StartTime = a.StartTime, EndTime = b.EndTime };
    public static ElapsedTime operator *(ElapsedTime a, ElapsedTime b) => new($"{a.Name} * {b.Name}") { StartTime = a.StartTime, EndTime = b.EndTime };
    public static ElapsedTime operator /(ElapsedTime a, ElapsedTime b) => new($"{a.Name} / {b.Name}") { StartTime = a.StartTime, EndTime = b.EndTime };
    public static ElapsedTime operator %(ElapsedTime a, ElapsedTime b) => new($"{a.Name} % {b.Name}") { StartTime = a.StartTime, EndTime = b.EndTime };

    #endregion

}
