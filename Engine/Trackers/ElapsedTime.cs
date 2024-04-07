using System;
using System.Diagnostics;

namespace GU1.Engine.Trackers;

public class ElapsedTime {

    #region Properties

    /// <summary>
    /// Assigning a name to the tracker will make the output more readable
    /// </summary>
    private string Name { get; set; }

    /// <summary>
    /// The precision of the time output
    /// </summary>
    private string Precision { get; set; }

    /// <summary>
    /// The Stopwatch object that will be used to track the time
    /// </summary>
    private Stopwatch Stopwatch { get; set; }

    /// <summary>
    /// The time elapsed between the start and end times in milliseconds
    /// </summary>
    private double Time => Stopwatch.Elapsed.TotalMilliseconds;

    #endregion

    /// <summary>
    /// Create a new ElapsedTime object
    /// </summary>
    /// <param name="name">Name of the tracker (for output purposes)</param>
    public ElapsedTime(string name, string precision = "0.0000000000") {

        Name = name;
        Precision = precision;
        Stopwatch = new Stopwatch();
    }

    #region Stopwatch Control

    /// <summary>
    /// Start tracking the time
    /// </summary>
    public void Start() => Stopwatch.Start();

    /// <summary>
    /// Stop tracking the time
    /// </summary>
    public void Stop() => Stopwatch.Stop();

    /// <summary>
    /// Reset the time tracker
    /// </summary>
    public void Reset() => Stopwatch.Reset();

    #endregion


    #region Result Output

    /// <summary>
    /// Get the result of the time tracking
    /// </summary>
    public string Result => string.Format($"[Elapsed Time Tracker - {Name}] Time elapsed: {{0:{Precision}}}ms", Time);

    /// <summary>
    /// Override the ToString method to return the result
    /// </summary>
    /// <returns></returns>
    public override string ToString() => Result;

    /// <summary>
    /// Print the result to the debug console
    /// </summary>
    public void PrintToDebug() => System.Diagnostics.Debug.WriteLine(Result);

    /// <summary>
    /// Print the result to the console
    /// </summary>
    public void PrintToConsole() => Console.WriteLine(Result);

    #endregion


    #region Operators

    public static bool operator <(ElapsedTime a, ElapsedTime b) => a.Time < b.Time;
    public static bool operator >(ElapsedTime a, ElapsedTime b) => a.Time > b.Time;
    public static bool operator <=(ElapsedTime a, ElapsedTime b) => a.Time <= b.Time;
    public static bool operator >=(ElapsedTime a, ElapsedTime b) => a.Time >= b.Time;
    public static bool operator ==(ElapsedTime a, ElapsedTime b) => a.Time == b.Time;
    public static bool operator !=(ElapsedTime a, ElapsedTime b) => a.Time != b.Time;

    public static ElapsedTime operator +(ElapsedTime a, ElapsedTime b) => new($"Sum of Elapsed Times {a.Time + b.Time}");
    public static ElapsedTime operator -(ElapsedTime a, ElapsedTime b) => new($"Difference of Elapsed Times {a.Time - b.Time}");
    public static ElapsedTime operator *(ElapsedTime a, ElapsedTime b) => new($"Product of Elapsed Times {a.Time * b.Time}");
    public static ElapsedTime operator /(ElapsedTime a, ElapsedTime b) => new($"Quotient of Elapsed Times {a.Time / b.Time}");

    #endregion


    #region Comparators

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

    #endregion

    /// <summary>
    /// Get the hash code of the time
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => HashCode.Combine(Time);
}
