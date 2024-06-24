using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir;

public class Achievements {

    #region Photos Taken Achievements

    public AchievementToast Achievement_1_PhotoTaken = new() {

        iconIndex = 4,
        name = "Amateur Photographer",
        description = "Take your first photo."
    };

    public AchievementToast Achievement_5_PhotosTaken = new() {

        iconIndex = 4,
        name = "Snap Happy",
        description = "Take 5 photos."
    };

    public AchievementToast Achievement_10_PhotosTaken = new() {

        iconIndex = 4,
        name = "Shutterbug",
        description = "Take 10 photos."
    };

    public AchievementToast Achievement_25_PhotosTaken = new() {

        iconIndex = 4,
        name = "Click Enthusiast",
        description = "Take 25 photos."
    };

    public AchievementToast Achievement_50_PhotosTaken = new() {

        iconIndex = 4,
        name = "Picture Perfect",
        description = "Take 50 photos."
    };

    public AchievementToast Achievement_100_PhotosTaken = new() {

        iconIndex = 4,
        name = "Frame Fanatic",
        description = "Take 100 photos."
    };

    public AchievementToast Achievement_250_PhotosTaken = new() {

        iconIndex = 4,
        name = "Phenom Photo",
        description = "Take 250 photos."
    };

    public AchievementToast Achievement_500_PhotosTaken = new() {

        iconIndex = 4,
        name = "Master of the Lens",
        description = "Take 500 photos."
    };

    public AchievementToast Achievement_1000_PhotosTaken = new() {

        iconIndex = 4,
        name = "Photography Virtuoso",
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

        iconIndex = 9,
        name = "Beachcomber Beginner",
        description = "Collect your first flotsam."
    };

    public AchievementToast Achievement_5_FlotsamCollected = new() {

        iconIndex = 9,
        name = "Seashore Scavenger",
        description = "Collect 5 flotsam."
    };

    public AchievementToast Achievement_10_FlotsamCollected = new() {

        iconIndex = 9,
        name = "Tidal Treasure Hunter",
        description = "Collect 10 flotsam."
    };

    public AchievementToast Achievement_25_FlotsamCollected = new() {

        iconIndex = 9,
        name = "Driftwood Devotee",
        description = "Collect 25 flotsam."
    };

    public AchievementToast Achievement_50_FlotsamCollected = new() {

        iconIndex = 9,
        name = "Flotsam Finder",
        description = "Collect 50 flotsam."
    };

    public AchievementToast Achievement_100_FlotsamCollected = new() {

        iconIndex = 2,
        name = "Coastal Collector",
        description = "Collect 100 flotsam."
    };

    public AchievementToast Achievement_250_FlotsamCollected = new() {

        iconIndex = 2,
        name = "Oceanic Hoarder",
        description = "Collect 250 flotsam."
    };

    public AchievementToast Achievement_500_FlotsamCollected = new() {

        iconIndex = 2,
        name = "Marine Magpie",
        description = "Collect 500 flotsam."
    };

    public AchievementToast Achievement_1000_FlotsamCollected = new() {

        iconIndex = 2,
        name = "Legendary Beachcomber",
        description = "Collect 1000 flotsam."
    };

    public AchievementToast Achievement_5000_FlotsamCollected = new() {

        iconIndex = 2,
        name = "Seaside Sage",
        description = "Collect 5000 flotsam."
    };

    public AchievementToast Achievement_10000_FlotsamCollected = new() {

        iconIndex = 2,
        name = "Tidepool Titans",
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

        iconIndex = 7,
        name = "Hidden Treasure Spotter",
        description = "Reveal your first flotsam."
    };

    public AchievementToast Achievement_5_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Secret Seeker",
        description = "Reveal 5 flotsam."
    };

    public AchievementToast Achievement_10_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Beach Detective",
        description = "Reveal 10 flotsam."
    };

    public AchievementToast Achievement_25_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Mystery Unveiler",
        description = "Reveal 25 flotsam."
    };

    public AchievementToast Achievement_50_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Flotsam Extraordinaire",
        description = "Reveal 50 flotsam."
    };

    public AchievementToast Achievement_100_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Coastal Sleuth",
        description = "Reveal 100 flotsam."
    };

    public AchievementToast Achievement_250_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Oceanic Revelator",
        description = "Reveal 250 flotsam."
    };

    public AchievementToast Achievement_500_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Marine Mystery Master",
        description = "Reveal 500 flotsam."
    };

    public AchievementToast Achievement_1000_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Legendary Revealer",
        description = "Reveal 1000 flotsam."
    };

    public AchievementToast Achievement_5000_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Seaside Seer",
        description = "Reveal 5000 flotsam."
    };

    public AchievementToast Achievement_10000_FlotsamRevealed = new() {

        iconIndex = 7,
        name = "Tidal Truth Teller",
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

    #region Rounds Played Achievements

    public AchievementToast Achievement_1_RoundsPlayed = new() {

        iconIndex = 3,
        name = "First Timer",
        description = "Play your first round."
    };

    public AchievementToast Achievement_5_GamesPlayed = new() {

        iconIndex = 3,
        name = "Enthusiast",
        description = "Play 5 rounds."
    };

    public AchievementToast Achievement_10_GamesPlayed = new() {

        iconIndex = 3,
        name = "Casual Competitor",
        description = "Play 10 rounds."
    };

    public AchievementToast Achievement_25_GamesPlayed = new() {

        iconIndex = 3,
        name = "Playtime Pro",
        description = "Play 25 rounds."
    };

    public AchievementToast Achievement_50_GamesPlayed = new() {

        iconIndex = 3,
        name = "Match Maverick",
        description = "Play 50 rounds."
    };

    public AchievementToast Achievement_100_GamesPlayed = new() {

        iconIndex = 3,
        name = "Roundabout Regular",
        description = "Play 100 rounds."
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

        iconIndex = 0,
        name = "Challenger",
        description = "Finish your first game."
    };

    public AchievementToast Achievement_5_GamesFinished = new() {

        iconIndex = 0,
        name = "Challenger",
        description = "Finish 5 games."
    };

    public AchievementToast Achievement_10_GamesFinished = new() {

        iconIndex = 0,
        name = "Competitor",
        description = "Finish 10 games."
    };

    public AchievementToast Achievement_25_GamesFinished = new() {

        iconIndex = 0,
        name = "Endgame Expert",
        description = "Finish 25 games."
    };

    public AchievementToast Achievement_50_GamesFinished = new() {

        iconIndex = 0,
        name = "Fanatic",
        description = "Finish 50 games."
    };

    public AchievementToast Achievement_100_GamesFinished = new() {

        iconIndex = 0,
        name = "Connoisseur",
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

        iconIndex = 0,
        name = "Quartet Conductor",
        description = "Play a game with a party of 4."
    };

    public AchievementToast Achievement_5_LargestParty = new() {

        iconIndex = 1,
        name = "Fivesome Leader",
        description = "Play a game with a party of 5."
    };

    public AchievementToast Achievement_6_LargestParty = new() {

        iconIndex = 1,
        name = "Sextet Supervisor",
        description = "Play a game with a party of 6."
    };

    public AchievementToast Achievement_7_LargestParty = new() {

        iconIndex = 1,
        name = "Sevenfold Chief",
        description = "Play a game with a party of 7."
    };

    public AchievementToast Achievement_8_LargestParty = new() {

        iconIndex = 1,
        name = "Octet Organizer",
        description = "Play a game with a party of 8."
    };

    public AchievementToast Achievement_9_LargestParty = new() {

        iconIndex = 1,
        name = "Nonet Navigator",
        description = "Play a game with a party of 9."
    };

    public AchievementToast Achievement_10_LargestParty = new() {

        iconIndex = 1,
        name = "Decade Director",
        description = "Play a game with a party of 10."
    };

    public AchievementToast Achievement_11_LargestParty = new() {

        iconIndex = 1,
        name = "Elven Commander",
        description = "Play a game with a party of 11."
    };

    public AchievementToast Achievement_12_LargestParty = new() {

        iconIndex = 1,
        name = "Dozen Dictator",
        description = "Play a game with a party of 12."
    };

    public AchievementToast Achievement_13_LargestParty = new() {

        iconIndex = 1,
        name = "Baker's Dozen Boss",
        description = "Play a game with a party of 13."
    };

    public AchievementToast Achievement_14_LargestParty = new() {

        iconIndex = 1,
        name = "Fourteenfold Foreman",
        description = "Play a game with a party of 14."
    };

    public AchievementToast Achievement_15_LargestParty = new() {

        iconIndex = 1,
        name = "Quindecim Captain",
        description = "Play a game with a party of 15."
    };

    public AchievementToast Achievement_16_LargestParty = new() {

        iconIndex = 8,
        name = "Sweet Sixteen Sovereign",
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
    double ToastEndTimer = 0;

    [XmlIgnore]
    AchievementToast currentToast = null;

    [XmlIgnore]
    Queue<AchievementToast> toastQueue = new();

    [XmlIgnore]
    Color currentColor = Color.White;

    public void Update(GameTime gameTime) {

        if (gameTime.TotalGameTime.TotalSeconds > ToastEndTimer) {
            currentToast = null;
            ToastEndTimer = 0;
        }

        if (toastQueue.Count > 0)
            if (ToastEndTimer <= 0) {

                SLib.Achievement.Play(GameState.SFXVolume, 0, 0);
                currentToast = toastQueue.Dequeue();
                ToastEndTimer = gameTime.TotalGameTime.TotalSeconds + 3;
            }


        // Colour funkiness
        float elapsedTime = (float)gameTime.TotalGameTime.TotalSeconds;                                     // Total time elapsed in seconds

        // Duration of each color transition in seconds
        float transitionDuration = 0.25f;                                                                   // time in seconds for each transition

        // Calculate progress through the current transition phase
        float progress = (elapsedTime % transitionDuration) / transitionDuration;                           // progress through the current transition phase (0-1)

        // Target colours
        Color yellow = Color.DarkBlue;
        Color skyBlue = Color.DarkRed;
        Color pink = Color.DarkGreen;

        // Determine which phase of the transition we're in
        int phase = (int)(elapsedTime / transitionDuration) % 3;                                            // phase of the transition (0-2)

        Color startColor, endColor;                                                                         // start and end colors for the current phase
        switch (phase)
        {
            case 0:
                startColor = yellow;
                endColor = skyBlue;
                break;
            case 1:
                startColor = skyBlue;
                endColor = pink;
                break;
            case 2:
                startColor = pink;
                endColor = yellow;
                break;
            default:
                startColor = Color.White;
                endColor = Color.White;
                break;
        }

        // Interpolate between the start and end colors based on progress
        currentColor = Color.Lerp(startColor, endColor, progress);
    }

    public void Draw(SpriteBatch spriteBatch) {

        if (currentToast == null) return;

        int yBase = 950;
        //new
        spriteBatch.Draw(TLib.Circle_80px, new Rectangle((1920/2)-(320/2)-40,yBase+15,40, 80), new Rectangle(0,0,40,80), currentColor * 0.5f);  //left semi circle
        DrawFilledRectangle(new Rectangle((1920/2)-(320/2),yBase+15,400-80,80), spriteBatch, currentColor * 0.5f);                             //rectangle
        spriteBatch.Draw(TLib.Circle_80px, new Rectangle((1920/2)+(320/2),yBase+15,40,80), new Rectangle(40,0,40,80), currentColor * 0.5f);    //right semi circle
        spriteBatch.Draw(TLib.AchievementIcons[currentToast.iconIndex], new Rectangle((1920/2)-(320/2)-8,yBase+15+8,64,64), Color.White * 0.9f);     //icon
        spriteBatch.DrawString(FLib.AchievementTitleFont, currentToast.name, new Vector2((1920/2)-(320/2)+64, yBase+15+8), Color.White *0.95f);                 //name
        spriteBatch.DrawString(FLib.AchievementDescriptionFont, currentToast.description, new Vector2((1920/2)-(320/2)+64, yBase+15+48), Color.White);         //description
    }
}
