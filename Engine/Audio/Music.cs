using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace GU1.Engine.Audio;

public static class Music
{
    // Music Queue
    public static Queue<Song> musicQueue = new();

    // Tracker Variables
    private static double songLength = 0;
    private static double startTime = 0;
    private static double EndTime => startTime + songLength;

    public static void Update(GameTime gameTime) {

        // Check if the MediaPlayer is playing
        if (MediaPlayer.State != MediaState.Playing)
            return;

        // Check if the start time is 0
        if (startTime == 0)                                                                                 // A new song has started
            startTime = gameTime.TotalGameTime.TotalMilliseconds;                                           // Set the start time to the current time

        if (songLength == 0)                                                                                // A new song has started
            songLength = musicQueue.Peek().Duration.TotalMilliseconds;                                      // Set songlength variable to the duration of the song

        // Check if the music queue is empty
        if (musicQueue.Count <= 0)
            return;

        // Check if the song has ended
        if (gameTime.TotalGameTime.TotalMilliseconds >= EndTime)
            PlayNextSong();
    }

    // Play Music
    private static void Play(Song song) {

        // Reset the tracker variables
        startTime = 0;
        songLength = 0;

        // Play Song
        MediaPlayer.Play(song);

    }

    public static void PlayNextSong() {

        if (musicQueue.Count <= 0)
            return;

        Play(musicQueue.Dequeue());
    }

    /// <summary>
    /// Pauses the current song
    /// </summary>
    public static void PauseMusic() => MediaPlayer.Pause();

    /// <summary>
    /// Resumes the paused song
    /// </summary>
    public static void ResumeMusic() => MediaPlayer.Resume();

    /// <summary>
    /// Stops the current song from playing
    /// </summary>
    public static void StopMusic() => MediaPlayer.Stop();

}
