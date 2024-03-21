namespace GU1.Engine.Trackers;

public class EventsPerSecond {

    private int counter = 0;                                                                                // Trackers the number of events per second
    private float elapsedTime = 0f;                                                                         // Trackers the elapsed time
    private float interval = 1f;                                                                            // Time interval in seconds, to calculate the events per second
    private float averageEPS = 0;                                                                           // The average events per second

    public int Counter => counter;                                                                          // User access to the counter
    public float AverageEPS => averageEPS;                                                                  // User access to the average events per second

    /// <summary>
    /// Constructor for the EventsPerSecond class
    /// </summary>
    /// <param name="interval">Default: 1f (1 second)</param>
    public EventsPerSecond(float interval = 1f) {
            
            this.interval = interval;                                                                       // Set the interval
    } // End of EventsPerSecond constructor

    /// <summary>
    /// Update the events per second tracker
    /// </summary>
    /// <param name="deltaTime">GameTime.TotalGameTime.TotalMilliseconds</param>
    public void Update(float deltaTime) {

        counter++;                                                                                          // Increment the counter
        elapsedTime += deltaTime;                                                                           // Increment the elapsed time by the delta time (total seconds)

        if (elapsedTime > interval) {                                                                       // If the elapsed time is greater than the interval (1 second)

            averageEPS = counter / elapsedTime;                                                             // Calculate the average events per second
            counter = 0;                                                                                    // Reset the counter
            elapsedTime = 0;                                                                                // Reset the elapsed time
        }
    }
}
