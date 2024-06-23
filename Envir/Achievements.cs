using System;

namespace GU1.Envir;

public class Achievements {

    #region Photos Taken Achievements

    public bool Achievement_1_PhotoTaken = false;
    public bool Achievement_5_PhotosTaken = false;
    public bool Achievement_10_PhotosTaken = false;
    public bool Achievement_25_PhotosTaken = false;
    public bool Achievement_50_PhotosTaken = false;
    public bool Achievement_100_PhotosTaken = false;
    public bool Achievement_250_PhotosTaken = false;
    public bool Achievement_500_PhotosTaken = false;
    public bool Achievement_1000_PhotosTaken = false;

    int totalPhotosTaken = 0;
    public int TotalPhotosTaken {
        get => totalPhotosTaken;
        set {
            totalPhotosTaken = value;
            UpdatePhotoAchievements();
        }
    }

    private void UpdatePhotoAchievements() {

        UpdateAchievement(ref Achievement_1_PhotoTaken, 1);
        UpdateAchievement(ref Achievement_5_PhotosTaken, 5);
        UpdateAchievement(ref Achievement_10_PhotosTaken, 10);
        UpdateAchievement(ref Achievement_25_PhotosTaken, 25);
        UpdateAchievement(ref Achievement_50_PhotosTaken, 50);
        UpdateAchievement(ref Achievement_100_PhotosTaken, 100);
        UpdateAchievement(ref Achievement_250_PhotosTaken, 250);
        UpdateAchievement(ref Achievement_500_PhotosTaken, 500);
        UpdateAchievement(ref Achievement_1000_PhotosTaken, 1000);
    }

    #endregion

    #region Floatsam Collected Achievements

    public bool Achievement_1_FlotsamCollected = false;
    public bool Achievement_5_FlotsamCollected = false;
    public bool Achievement_10_FlotsamCollected = false;
    public bool Achievement_25_FlotsamCollected = false;
    public bool Achievement_50_FlotsamCollected = false;
    public bool Achievement_100_FlotsamCollected = false;
    public bool Achievement_250_FlotsamCollected = false;
    public bool Achievement_500_FlotsamCollected = false;
    public bool Achievement_1000_FlotsamCollected = false;
    public bool Achievement_5000_FlotsamCollected = false;
    public bool Achievement_10000_FlotsamCollected = false;

    int totalFlotsamCollected = 0;
    public int TotalFlotsamCollected {
        get => totalFlotsamCollected;
        set {
            totalFlotsamCollected = value;
            UpdateFlotsamAchievements();
        }
    }

    private void UpdateFlotsamAchievements() {
        UpdateAchievement(ref Achievement_1_FlotsamCollected, 1);
        UpdateAchievement(ref Achievement_5_FlotsamCollected, 5);
        UpdateAchievement(ref Achievement_10_FlotsamCollected, 10);
        UpdateAchievement(ref Achievement_25_FlotsamCollected, 25);
        UpdateAchievement(ref Achievement_50_FlotsamCollected, 50);
        UpdateAchievement(ref Achievement_100_FlotsamCollected, 100);
        UpdateAchievement(ref Achievement_250_FlotsamCollected, 250);
        UpdateAchievement(ref Achievement_500_FlotsamCollected, 500);
        UpdateAchievement(ref Achievement_1000_FlotsamCollected, 1000);
        UpdateAchievement(ref Achievement_5000_FlotsamCollected, 5000);
        UpdateAchievement(ref Achievement_10000_FlotsamCollected, 10000);
    }

    #endregion

    #region Games Played Achievements

    public bool Achievement_1_GamePlayed = false;
    public bool Achievement_5_GamesPlayed = false;
    public bool Achievement_10_GamesPlayed = false;
    public bool Achievement_25_GamesPlayed = false;
    public bool Achievement_50_GamesPlayed = false;
    public bool Achievement_100_GamesPlayed = false;

    int gamesPlayed = 0;
    public int GamesPlayed {
        get => gamesPlayed;
        set {
            gamesPlayed = value;

            if (gamesPlayed >= 1 && !Achievement_1_GamePlayed)
                Achievement_1_GamePlayed = true;

            if (gamesPlayed >= 5 && !Achievement_5_GamesPlayed)
                Achievement_5_GamesPlayed = true;

            if (gamesPlayed >= 10 && !Achievement_10_GamesPlayed)
                Achievement_10_GamesPlayed = true;

            if (gamesPlayed >= 25 && !Achievement_25_GamesPlayed)
                Achievement_25_GamesPlayed = true;

            if (gamesPlayed >= 50 && !Achievement_50_GamesPlayed)
                Achievement_50_GamesPlayed = true;

            if (gamesPlayed >= 100 && !Achievement_100_GamesPlayed)
                Achievement_100_GamesPlayed = true;
        }
    }

    #endregion

    #region Games Finished Achievements

    public bool Achievement_1_GameFinished = false;
    public bool Achievement_5_GamesFinished = false;
    public bool Achievement_10_GamesFinished = false;
    public bool Achievement_25_GamesFinished = false;
    public bool Achievement_50_GamesFinished = false;
    public bool Achievement_100_GamesFinished = false;

    int gamesFinished = 0;
    public int GamesFinished {
        get => gamesFinished;
        set {
            gamesFinished = value;

            if (gamesFinished >= 1 && !Achievement_1_GameFinished)
                Achievement_1_GameFinished = true;

            if (gamesFinished >= 5 && !Achievement_5_GamesFinished)
                Achievement_5_GamesFinished = true;

            if (gamesFinished >= 10 && !Achievement_10_GamesFinished)
                Achievement_10_GamesFinished = true;

            if (gamesFinished >= 25 && !Achievement_25_GamesFinished)
                Achievement_25_GamesFinished = true;

            if (gamesFinished >= 50 && !Achievement_50_GamesFinished)
                Achievement_50_GamesFinished = true;

            if (gamesFinished >= 100 && !Achievement_100_GamesFinished)
                Achievement_100_GamesFinished = true;
        }
    }

    #endregion

    #region Largest Party Achievements

    public bool Achievement_4_LargestParty = false;
    public bool Achievement_5_LargestParty = false;
    public bool Achievement_6_LargestParty = false;
    public bool Achievement_7_LargestParty = false;
    public bool Achievement_8_LargestParty = false;
    public bool Achievement_9_LargestParty = false;
    public bool Achievement_10_LargestParty = false;
    public bool Achievement_11_LargestParty = false;
    public bool Achievement_12_LargestParty = false;
    public bool Achievement_13_LargestParty = false;
    public bool Achievement_14_LargestParty = false;
    public bool Achievement_15_LargestParty = false;
    public bool Achievement_16_LargestParty = false;

    int largestParty = 0;
    public int LargestParty {
        get => largestParty;
        set {
            largestParty = value;

            if (largestParty >= 4 && !Achievement_4_LargestParty)
                Achievement_4_LargestParty = true;

            if (largestParty >= 5 && !Achievement_5_LargestParty)
                Achievement_5_LargestParty = true;

            if (largestParty >= 6 && !Achievement_6_LargestParty)
                Achievement_6_LargestParty = true;

            if (largestParty >= 7 && !Achievement_7_LargestParty)
                Achievement_7_LargestParty = true;

            if (largestParty >= 8 && !Achievement_8_LargestParty)
                Achievement_8_LargestParty = true;

            if (largestParty >= 9 && !Achievement_9_LargestParty)
                Achievement_9_LargestParty = true;

            if (largestParty >= 10 && !Achievement_10_LargestParty)
                Achievement_10_LargestParty = true;

            if (largestParty >= 11 && !Achievement_11_LargestParty)
                Achievement_11_LargestParty = true;

            if (largestParty >= 12 && !Achievement_12_LargestParty)
                Achievement_12_LargestParty = true;

            if (largestParty >= 13 && !Achievement_13_LargestParty)
                Achievement_13_LargestParty = true;

            if (largestParty >= 14 && !Achievement_14_LargestParty)
                Achievement_14_LargestParty = true;

            if (largestParty >= 15 && !Achievement_15_LargestParty)
                Achievement_15_LargestParty = true;

            if (largestParty >= 16 && !Achievement_16_LargestParty)
                Achievement_16_LargestParty = true;
        }
    }

    #endregion

    /// <summary>
    /// The date and time of the first game played.
    /// </summary>
    public DateTime firstGame = DateTime.MinValue;

    /// <summary>
    /// The date and time of the last game played.
    /// </summary>
    public DateTime lastGame = DateTime.MinValue;

    private void UpdateAchievement(ref bool achievement, int threshold) {
        if (totalFlotsamCollected >= threshold && !achievement)
            achievement = true;
    }
}
