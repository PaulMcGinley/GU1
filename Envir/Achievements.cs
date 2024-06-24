﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir;

public class Achievements {

    #region Photos Taken Achievements

    public AchievementToast Achievement_1_PhotoTaken = new() {

        iconIndex = -1,
        name = "First Photo",
        description = "Take your first photo."
    };

    public AchievementToast Achievement_5_PhotosTaken = new() {

        iconIndex = -1,
        name = "5 Photos Taken",
        description = "Take 5 photos."
    };

    public AchievementToast Achievement_10_PhotosTaken = new() {

        iconIndex = -1,
        name = "10 Photos Taken",
        description = "Take 10 photos."
    };

    public AchievementToast Achievement_25_PhotosTaken = new() {

        iconIndex = -1,
        name = "25 Photos Taken",
        description = "Take 25 photos."
    };

    public AchievementToast Achievement_50_PhotosTaken = new() {

        iconIndex = -1,
        name = "50 Photos Taken",
        description = "Take 50 photos."
    };

    public AchievementToast Achievement_100_PhotosTaken = new() {

        iconIndex = -1,
        name = "100 Photos Taken",
        description = "Take 100 photos."
    };

    public AchievementToast Achievement_250_PhotosTaken = new() {

        iconIndex = -1,
        name = "250 Photos Taken",
        description = "Take 250 photos."
    };

    public AchievementToast Achievement_500_PhotosTaken = new() {

        iconIndex = -1,
        name = "500 Photos Taken",
        description = "Take 500 photos."
    };

    public AchievementToast Achievement_1000_PhotosTaken = new() {

        iconIndex = -1,
        name = "1000 Photos Taken",
        description = "Take 1000 photos."
    };

    int totalPhotosTaken = 0;
    public int TotalPhotosTaken {
        get => totalPhotosTaken;
        set {
            totalPhotosTaken = value;
            UpdatePhotoAchievements();
        }
    }

    private void UpdatePhotoAchievements() {

        UpdateAchievement(ref Achievement_1_PhotoTaken, totalPhotosTaken, 1);
        UpdateAchievement(ref Achievement_5_PhotosTaken, totalPhotosTaken, 5);
        UpdateAchievement(ref Achievement_10_PhotosTaken, totalPhotosTaken, 10);
        UpdateAchievement(ref Achievement_25_PhotosTaken, totalPhotosTaken, 25);
        UpdateAchievement(ref Achievement_50_PhotosTaken, totalPhotosTaken, 50);
        UpdateAchievement(ref Achievement_100_PhotosTaken, totalPhotosTaken, 100);
        UpdateAchievement(ref Achievement_250_PhotosTaken, totalPhotosTaken, 250);
        UpdateAchievement(ref Achievement_500_PhotosTaken, totalPhotosTaken, 500);
        UpdateAchievement(ref Achievement_1000_PhotosTaken, totalPhotosTaken, 1000);
    }

    #endregion

    #region Floatsam Collected Achievements

    public AchievementToast Achievement_1_FlotsamCollected = new() {

        iconIndex = -1,
        name = "First Flotsam",
        description = "Collect your first flotsam."
    };

    public AchievementToast Achievement_5_FlotsamCollected = new() {

        iconIndex = -1,
        name = "5 Flotsam Collected",
        description = "Collect 5 flotsam."
    };

    public AchievementToast Achievement_10_FlotsamCollected = new() {

        iconIndex = -1,
        name = "10 Flotsam Collected",
        description = "Collect 10 flotsam."
    };

    public AchievementToast Achievement_25_FlotsamCollected = new() {

        iconIndex = -1,
        name = "25 Flotsam Collected",
        description = "Collect 25 flotsam."
    };

    public AchievementToast Achievement_50_FlotsamCollected = new() {

        iconIndex = -1,
        name = "50 Flotsam Collected",
        description = "Collect 50 flotsam."
    };

    public AchievementToast Achievement_100_FlotsamCollected = new() {

        iconIndex = -1,
        name = "100 Flotsam Collected",
        description = "Collect 100 flotsam."
    };

    public AchievementToast Achievement_250_FlotsamCollected = new() {

        iconIndex = -1,
        name = "250 Flotsam Collected",
        description = "Collect 250 flotsam."
    };

    public AchievementToast Achievement_500_FlotsamCollected = new() {

        iconIndex = -1,
        name = "500 Flotsam Collected",
        description = "Collect 500 flotsam."
    };

    public AchievementToast Achievement_1000_FlotsamCollected = new() {

        iconIndex = -1,
        name = "1000 Flotsam Collected",
        description = "Collect 1000 flotsam."
    };

    public AchievementToast Achievement_5000_FlotsamCollected = new() {

        iconIndex = -1,
        name = "5000 Flotsam Collected",
        description = "Collect 5000 flotsam."
    };

    public AchievementToast Achievement_10000_FlotsamCollected = new() {

        iconIndex = -1,
        name = "10000 Flotsam Collected",
        description = "Collect 10000 flotsam."
    };

    int totalFlotsamCollected = 0;
    public int TotalFlotsamCollected {
        get => totalFlotsamCollected;
        set {
            totalFlotsamCollected = value;
            UpdateFlotsamAchievements();
        }
    }

    private void UpdateFlotsamAchievements() {

        UpdateAchievement(ref Achievement_1_FlotsamCollected, totalFlotsamCollected, 1);
        UpdateAchievement(ref Achievement_5_FlotsamCollected, totalFlotsamCollected, 5);
        UpdateAchievement(ref Achievement_10_FlotsamCollected, totalFlotsamCollected, 10);
        UpdateAchievement(ref Achievement_25_FlotsamCollected, totalFlotsamCollected, 25);
        UpdateAchievement(ref Achievement_50_FlotsamCollected, totalFlotsamCollected, 50);
        UpdateAchievement(ref Achievement_100_FlotsamCollected, totalFlotsamCollected, 100);
        UpdateAchievement(ref Achievement_250_FlotsamCollected, totalFlotsamCollected, 250);
        UpdateAchievement(ref Achievement_500_FlotsamCollected, totalFlotsamCollected, 500);
        UpdateAchievement(ref Achievement_1000_FlotsamCollected, totalFlotsamCollected, 1000);
        UpdateAchievement(ref Achievement_5000_FlotsamCollected, totalFlotsamCollected, 5000);
        UpdateAchievement(ref Achievement_10000_FlotsamCollected, totalFlotsamCollected, 10000);
    }

    #endregion

    #region Flotsam Revealed Achievements

    public AchievementToast Achievement_1_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "First Flotsam Revealed",
        description = "Reveal your first flotsam."
    };

    public AchievementToast Achievement_5_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "5 Flotsam Revealed",
        description = "Reveal 5 flotsam."
    };

    public AchievementToast Achievement_10_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "10 Flotsam Revealed",
        description = "Reveal 10 flotsam."
    };

    public AchievementToast Achievement_25_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "25 Flotsam Revealed",
        description = "Reveal 25 flotsam."
    };

    public AchievementToast Achievement_50_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "50 Flotsam Revealed",
        description = "Reveal 50 flotsam."
    };

    public AchievementToast Achievement_100_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "100 Flotsam Revealed",
        description = "Reveal 100 flotsam."
    };

    public AchievementToast Achievement_250_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "250 Flotsam Revealed",
        description = "Reveal 250 flotsam."
    };

    public AchievementToast Achievement_500_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "500 Flotsam Revealed",
        description = "Reveal 500 flotsam."
    };

    public AchievementToast Achievement_1000_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "1000 Flotsam Revealed",
        description = "Reveal 1000 flotsam."
    };

    public AchievementToast Achievement_5000_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "5000 Flotsam Revealed",
        description = "Reveal 5000 flotsam."
    };

    public AchievementToast Achievement_10000_FlotsamRevealed = new() {

        iconIndex = -1,
        name = "10000 Flotsam Revealed",
        description = "Reveal 10000 flotsam."
    };

    int totalFlotsamRevealed = 0;
    public int TotalFlotsamRevealed {
        get => totalFlotsamRevealed;
        set {
            totalFlotsamRevealed = value;
            UpdateFlotsamRevealedAchievements();
        }
    }

    private void UpdateFlotsamRevealedAchievements() {

        UpdateAchievement(ref Achievement_1_FlotsamRevealed, totalFlotsamRevealed, 1);
        UpdateAchievement(ref Achievement_5_FlotsamRevealed, totalFlotsamRevealed, 5);
        UpdateAchievement(ref Achievement_10_FlotsamRevealed, totalFlotsamRevealed, 10);
        UpdateAchievement(ref Achievement_25_FlotsamRevealed, totalFlotsamRevealed, 25);
        UpdateAchievement(ref Achievement_50_FlotsamRevealed, totalFlotsamRevealed, 50);
        UpdateAchievement(ref Achievement_100_FlotsamRevealed, totalFlotsamRevealed, 100);
        UpdateAchievement(ref Achievement_250_FlotsamRevealed, totalFlotsamRevealed, 250);
        UpdateAchievement(ref Achievement_500_FlotsamRevealed, totalFlotsamRevealed, 500);
        UpdateAchievement(ref Achievement_1000_FlotsamRevealed, totalFlotsamRevealed, 1000);
        UpdateAchievement(ref Achievement_5000_FlotsamRevealed, totalFlotsamRevealed, 5000);
        UpdateAchievement(ref Achievement_10000_FlotsamRevealed, totalFlotsamRevealed, 10000);
    }

    #endregion

    #region Games Played Achievements

    public AchievementToast Achievement_1_RoundsPlayed = new() {

        iconIndex = -1,
        name = "First Game",
        description = "Play your first game."
    };

    public AchievementToast Achievement_5_GamesPlayed = new() {

        iconIndex = -1,
        name = "5 Games Played",
        description = "Play 5 games."
    };

    public AchievementToast Achievement_10_GamesPlayed = new() {

        iconIndex = -1,
        name = "10 Games Played",
        description = "Play 10 games."
    };

    public AchievementToast Achievement_25_GamesPlayed = new() {

        iconIndex = -1,
        name = "25 Games Played",
        description = "Play 25 games."
    };

    public AchievementToast Achievement_50_GamesPlayed = new() {

        iconIndex = -1,
        name = "50 Games Played",
        description = "Play 50 games."
    };

    public AchievementToast Achievement_100_GamesPlayed = new() {

        iconIndex = -1,
        name = "100 Games Played",
        description = "Play 100 games."
    };

    int roundsPlayed = 0;
    public int RoundsPlayed {
        get => roundsPlayed;
        set {
            roundsPlayed = value;

            UpdateAchievement(ref Achievement_1_RoundsPlayed, roundsPlayed, 1);
            UpdateAchievement(ref Achievement_5_GamesPlayed, roundsPlayed, 5);
            UpdateAchievement(ref Achievement_10_GamesPlayed, roundsPlayed, 10);
            UpdateAchievement(ref Achievement_25_GamesPlayed, roundsPlayed, 25);
            UpdateAchievement(ref Achievement_50_GamesPlayed, roundsPlayed, 50);
            UpdateAchievement(ref Achievement_100_GamesPlayed, roundsPlayed, 100);
        }
    }

    #endregion

    #region Games Finished Achievements

    public AchievementToast Achievement_1_GameFinished = new() {

        iconIndex = -1,
        name = "First Game Finished",
        description = "Finish your first game."
    };

    public AchievementToast Achievement_5_GamesFinished = new() {

        iconIndex = -1,
        name = "5 Games Finished",
        description = "Finish 5 games."
    };

    public AchievementToast Achievement_10_GamesFinished = new() {

        iconIndex = -1,
        name = "10 Games Finished",
        description = "Finish 10 games."
    };

    public AchievementToast Achievement_25_GamesFinished = new() {

        iconIndex = -1,
        name = "25 Games Finished",
        description = "Finish 25 games."
    };

    public AchievementToast Achievement_50_GamesFinished = new() {

        iconIndex = -1,
        name = "50 Games Finished",
        description = "Finish 50 games."
    };

    public AchievementToast Achievement_100_GamesFinished = new() {

        iconIndex = -1,
        name = "100 Games Finished",
        description = "Finish 100 games."
    };

    int gamesFinished = 0;
    public int GamesFinished {
        get => gamesFinished;
        set {
            gamesFinished = value;

            UpdateAchievement(ref Achievement_1_GameFinished, gamesFinished, 1);
            UpdateAchievement(ref Achievement_5_GamesFinished, gamesFinished, 15);
            UpdateAchievement(ref Achievement_10_GamesFinished, gamesFinished, 110);
            UpdateAchievement(ref Achievement_25_GamesFinished, gamesFinished, 125);
            UpdateAchievement(ref Achievement_50_GamesFinished, gamesFinished, 150);
            UpdateAchievement(ref Achievement_100_GamesFinished, gamesFinished, 1100);
        }
    }

    #endregion

    #region Largest Party Achievements

    public AchievementToast Achievement_4_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 4",
        description = "Play a game with a party of 4."
    };

    public AchievementToast Achievement_5_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 5",
        description = "Play a game with a party of 5."
    };

    public AchievementToast Achievement_6_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 6",
        description = "Play a game with a party of 6."
    };

    public AchievementToast Achievement_7_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 7",
        description = "Play a game with a party of 7."
    };

    public AchievementToast Achievement_8_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 8",
        description = "Play a game with a party of 8."
    };

    public AchievementToast Achievement_9_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 9",
        description = "Play a game with a party of 9."
    };

    public AchievementToast Achievement_10_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 10",
        description = "Play a game with a party of 10."
    };

    public AchievementToast Achievement_11_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 11",
        description = "Play a game with a party of 11."
    };

    public AchievementToast Achievement_12_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 12",
        description = "Play a game with a party of 12."
    };

    public AchievementToast Achievement_13_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 13",
        description = "Play a game with a party of 13."
    };

    public AchievementToast Achievement_14_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 14",
        description = "Play a game with a party of 14."
    };

    public AchievementToast Achievement_15_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 15",
        description = "Play a game with a party of 15."
    };

    public AchievementToast Achievement_16_LargestParty = new() {

        iconIndex = -1,
        name = "Largest Party of 16",
        description = "Play a game with a party of 16."
    };

    int largestParty = 0;
    public int LargestParty {
        get => largestParty;
        set {
            largestParty = value;

            UpdateAchievement(ref Achievement_4_LargestParty, largestParty, 4);
            UpdateAchievement(ref Achievement_5_LargestParty, largestParty, 5);
            UpdateAchievement(ref Achievement_6_LargestParty, largestParty, 6);
            UpdateAchievement(ref Achievement_7_LargestParty, largestParty, 7);
            UpdateAchievement(ref Achievement_8_LargestParty, largestParty, 8);
            UpdateAchievement(ref Achievement_9_LargestParty, largestParty, 9);
            UpdateAchievement(ref Achievement_10_LargestParty, largestParty, 10);
            UpdateAchievement(ref Achievement_11_LargestParty, largestParty, 11);
            UpdateAchievement(ref Achievement_12_LargestParty, largestParty, 12);
            UpdateAchievement(ref Achievement_13_LargestParty, largestParty, 13);
            UpdateAchievement(ref Achievement_14_LargestParty, largestParty, 14);
            UpdateAchievement(ref Achievement_15_LargestParty, largestParty, 15);
            UpdateAchievement(ref Achievement_16_LargestParty, largestParty, 16);
        }
    }

    #endregion

    /// <summary>
    /// The date and time of the first game played.
    /// </summary>
    //public DateTime firstGame = DateTime.MinValue;

    /// <summary>
    /// The date and time of the last game played.
    /// </summary>
    //public DateTime lastGame = DateTime.MinValue;

    private void UpdateAchievement(ref AchievementToast achievement, int value, int threshold) {

        if (value >= threshold && !achievement.isAchieved) {

            achievement.isAchieved = true;
            achievement.awarded = DateTime.Now;
            toastQueue.Enqueue(achievement);
            GameState.SaveAchievements();
        }
    }

    [XmlIgnore]
    public double ToastEndTimer = 0;

    [XmlIgnore]
    AchievementToast currentToast = null;

    [XmlIgnore]
    Queue<AchievementToast> toastQueue = new();

    public void Update(GameTime gameTime) {

        if (gameTime.TotalGameTime.TotalSeconds > ToastEndTimer) {
            currentToast = null;
            ToastEndTimer = 0;
        }

        if (toastQueue.Count == 0) return;

        if (ToastEndTimer <= 0) {
            currentToast = toastQueue.Dequeue();
            ToastEndTimer = gameTime.TotalGameTime.TotalSeconds + 5;
        }
    }

    public void Draw(SpriteBatch spriteBatch) {

        if (currentToast == null) return;

        DrawFilledRectangle(new Rectangle((1920/2-200), 1080 - 60 - 15, 400, 60), spriteBatch, Color.Black * 0.5f);
        spriteBatch.DrawString(FLib.DebugFont, currentToast.name, new Vector2(1920/2 - FLib.DebugFont.MeasureString(currentToast.name).X/2, 1080 - 60), Color.White);
        spriteBatch.DrawString(FLib.DebugFont, currentToast.description, new Vector2(1920/2 - (int)(FLib.DebugFont.MeasureString(currentToast.description).X/2), 1080 - 45), Color.White);
    }
}
