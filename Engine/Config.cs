namespace GU1.Engine;

public static class Config {

    /// <summary>
    /// Track the Update, FixedUpdate and Draw rates
    /// </summary>
    public static bool TrackUpdateRate { get; set; } = true;                                                // If true, the update rate will be tracked

    /// <summary>
    /// Show the debug info
    /// </summary>
    public static bool ShowDebugInfo { get; set; } = true;                                                  // If true, the debug info will be shown

    /// <summary>
    /// Smooths the edges of the sprites
    /// </summary>
    public static bool AntiAliasing { get; set; } = false;                                                  // If true, anti-aliasing will be enabled

}
