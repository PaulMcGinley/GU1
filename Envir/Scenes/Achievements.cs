using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Achievements : IScene {

    List<Toast> Photo_Toasts = new ();
    Toast[] Photo_Toasts_Unlockable;

    List<Toast> Flotsam_Toasts = new ();
    Toast[] Flotsam_Toasts_Unlockable;

    List<Toast> Game_Toasts = new ();
    Toast[] Game_Toasts_Unlockable;

    float ScrollOffset = 0;

    Toast[] leftMenuToast = new Toast[MenuItems.GetValues(typeof(MenuItems)).Length];

    int selectedMenuIndex = 0;                                                                              // The index of the selected menu item (variable)
    int SelectedMenuIndex {                                                                                 // The index of the selected menu item (property)

        get => selectedMenuIndex;                                                                           // Get the selected menu index
        set {
            if (value < 0)                                                                                  // If the value is less than 0 (less than the first menu item)
                selectedMenuIndex = Enum.GetValues(typeof(MenuItems)).Length - 1;                           // Set the selected menu index to the last menu item
            else if (value > Enum.GetValues(typeof(MenuItems)).Length - 1)                                  // If the value is greater than the last menu item
                selectedMenuIndex = 0;                                                                      // Set the selected menu index to the first menu item
            else                                                                                            // Otherwise
                selectedMenuIndex = value;                                                                  // Set the selected menu index to the value

            ScrollOffset = 0;                                                                               // Reset the scroll offset
            SLib.Click.Play(GameState.SFXVolume, 0, 0);                                                     // Play the click sound
        }
    }

    Color groupColdColour = Color.Black * 0.8f;
    Color groupHotColour = Color.White * 0.95f;

    Color groupTextColdColour = Color.Black;
    Color groupTextHotColour = Color.White;

    Color achievementColdColour = Color.Black * 0.8f;
    Color achievementHotColour = Color.Gold * 0.95f;

    Color menuHotColor = Color.Lime;                                                                       // Selected menu item colour
    Color menuColdColor = Color.White;                                                                      // Unselected menu item colour

    public void Initialize(GraphicsDevice device) {

        float yOffsetForCentering = (device.Viewport.Height - (leftMenuToast.Length * 100)) / 2;

        leftMenuToast[0] = new Toast(new Vector2(100, 0 + yOffsetForCentering)) {

            Caption = "Overview",
            IconIndex = 0,
        };

        leftMenuToast[1] = new Toast(new Vector2(100, 100 + yOffsetForCentering)) {

            Caption = "Photos",
            IconIndex = 4
        };

        leftMenuToast[2] = new Toast(new Vector2(100, 200 + yOffsetForCentering)) {

            Caption = "Floatsam",
            IconIndex = 2
        };

        leftMenuToast[3] = new Toast(new Vector2(100, 300 + yOffsetForCentering)) {

            Caption = "Game",
            IconIndex = 3
        };

        leftMenuToast[4] = new Toast(new Vector2(100, 400 + yOffsetForCentering)) {

            Caption = "Cheats",
            IconIndex = 10
        };
    }

    public void LoadContent(ContentManager content) {}
    public void UnloadContent() {}

    public void Update(GameTime gameTime) {

        if (IsAnyInputPressed(Keys.B, Buttons.B)) {

            // TODO: Find a better sound to transition scenes
            //SLib.Click.Play(GameState.SFXVolume, 0, 0);
            GameState.CurrentScene = GameScene.MainMenu;
        }

        // Check for menu navigation
        if (IsAnyInputPressed(Keys.Down, Buttons.DPadDown, Buttons.LeftThumbstickDown))
            SelectedMenuIndex++;

        if (IsAnyInputPressed(Keys.Up, Buttons.DPadUp, Buttons.LeftThumbstickUp))
            SelectedMenuIndex--;

        if (IsAnyInputDown(Buttons.RightThumbstickDown))
                ScrollOffset -= (float)gameTime.ElapsedGameTime.TotalSeconds * 500;

        if (IsAnyInputDown(Buttons.RightThumbstickUp))
                ScrollOffset += (float)gameTime.ElapsedGameTime.TotalSeconds * 500;

        switch ((MenuItems)SelectedMenuIndex) {

            case MenuItems.Overview:
                ScrollOffset = 0;
                break;
            case MenuItems.Photos:
                ScrollOffset = Math.Clamp(ScrollOffset, -(Photo_Toasts.Count * 100) + (1080/2), 0);
                break;
            case MenuItems.Floatsam:
                ScrollOffset = Math.Clamp(ScrollOffset, -(Flotsam_Toasts.Count * 100) + (1080/2), 0);
                break;
            case MenuItems.Game:
                ScrollOffset = Math.Clamp(ScrollOffset, -(Game_Toasts.Count * 100) + (1080/2), 0);
                break;
            case MenuItems.Misc:
                ScrollOffset = 0;
                break;
        }

        if (selectedMenuIndex == (int)MenuItems.Misc) {

            if (IsAnyInputPressed(Buttons.RightThumbstickDown))
                SelectedCheataIndex++;

            if (IsAnyInputPressed(Buttons.RightThumbstickUp))
                SelectedCheataIndex--;

            if (IsAnyInputPressed(Keys.Right, Buttons.DPadRight, Buttons.LeftThumbstickRight, Buttons.RightThumbstickRight) || IsAnyInputPressed(Keys.Left, Buttons.DPadLeft, Buttons.LeftThumbstickLeft, Buttons.RightThumbstickLeft)) {

                switch ((Cheats)SelectedCheataIndex) {

                    case Cheats.ShapeShifting:
                    if (GameState.Achievements.Achievement_16_LargestParty.isAchieved)
                        GameState.AllowShapeShift = !GameState.AllowShapeShift;
                        break;
                }
            }
        }
    }

    public void FixedUpdate(GameTime gameTime) {}

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.Draw(TLib.AchievementBackground, new Rectangle(0, 0, 1920, 1080), Color.White);

        DrawGroups(spriteBatch);

        switch ((MenuItems)SelectedMenuIndex) {

            case MenuItems.Overview:
                Draw_Overview(spriteBatch);
                break;

            case MenuItems.Photos:
                Draw_Photos_Achievements(spriteBatch);
                break;

            case MenuItems.Floatsam:
                Draw_Floatsam_Achievements(spriteBatch);
                break;

            case MenuItems.Game:
                Draw_Game_Achievements(spriteBatch);
                break;

            case MenuItems.Misc:
                Draw_Cheats(spriteBatch);
                break;
        }

        spriteBatch.End();
    }

    private void Draw_Cheats(SpriteBatch spriteBatch) {

        if (!GameState.Achievements.Achievement_16_LargestParty.isAchieved) {
            DrawTextCenteredScreen(spriteBatch, FLib.AchievementCaptionFont, "Unlock cheats by completing achievements!", 1008/2+2, new Vector2(1920, 1080), Color.Black);
            DrawTextCenteredScreen(spriteBatch, FLib.AchievementCaptionFont, "Unlock cheats by completing achievements!", 1008/2, new Vector2(1920, 1080), Color.Yellow);
            return;
        }

        spriteBatch.DrawString(FLib.LeaderboardFont, "Allow Shape shifting (Y)", new Vector2(((1920 / 6)*4)  - FLib.LeaderboardFont.MeasureString("Allow Shape shifting (Y)").X, 200), SelectedCheataIndex == 0 ? menuHotColor : menuColdColor);
        spriteBatch.DrawString(FLib.LeaderboardFont, $"{GameState.AllowShapeShift}", new Vector2(((1920 / 4)*3) + 50, 200), SelectedCheataIndex == 0 ? menuHotColor : menuColdColor);
    }

    void Draw_Overview(SpriteBatch spriteBatch) {

        // If i can be arsed later i will sort this out
        List<AchievementToast> achieved = new List<AchievementToast>();
        achieved.AddRange(GetAllToastObjects("Photos", true));
        achieved.AddRange(GetAllToastObjects("Flotsam", true));
        achieved.AddRange(GetAllToastObjects("Game", true));
        achieved.Sort((a, b) => b.awarded.CompareTo(a.awarded));


        Toast[] toasts = new Toast[achieved.Count];

        int limit = Math.Min(achieved.Count, 9);

        for (int i = 0; i < limit; i++) {

            toasts[i] = new Toast(new Vector2(600, 100 + (i * 100))) {

                Title = achieved[i].name,
                Description = achieved[i].description,
                IconIndex = achieved[i].iconIndex,
                Colour = achievementHotColour,
                IconColour = Color.Black,
                TextColour = Color.Black
            };
        }

        for (int i = 0; i < limit; i++)
            toasts[i].Draw(spriteBatch);

        Color titleColour = Color.White;
        Color valueColour = Color.Yellow;
        Vector2 shadowOffset = new Vector2(1, 2);

        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, "Flotsam Collected", new Vector2(1200, 100), titleColour, shadowOffset, Color.Black);
        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, GameState.Achievements.TotalFlotsamCollected.ToString(), new Vector2(1200, 150), valueColour, shadowOffset, Color.Black);

        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, "Flotsam Revealed", new Vector2(1200, 250), titleColour, shadowOffset, Color.Black);
        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, GameState.Achievements.TotalFlotsamRevealed.ToString(), new Vector2(1200, 300), valueColour, shadowOffset, Color.Black);

        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, "Photos Taken", new Vector2(1200, 400), titleColour, shadowOffset, Color.Black);
        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, GameState.Achievements.TotalPhotosTaken.ToString(), new Vector2(1200, 450), valueColour, shadowOffset, Color.Black);

        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, "Largest Party", new Vector2(1200, 550), titleColour, shadowOffset, Color.Black);
        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, GameState.Achievements.LargestParty.ToString(), new Vector2(1200, 600), valueColour, shadowOffset, Color.Black);

        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, "Rounds Played", new Vector2(1200, 700), titleColour, shadowOffset, Color.Black);
        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, GameState.Achievements.RoundsPlayed.ToString(), new Vector2(1200, 750), valueColour, shadowOffset, Color.Black);

        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, "Games Finished", new Vector2(1200, 850), titleColour, shadowOffset, Color.Black);
        DrawShadowedText(spriteBatch, FLib.AchievementCaptionFont, GameState.Achievements.GamesFinished.ToString(), new Vector2(1200, 900), valueColour, shadowOffset, Color.Black);
    }

    void Draw_Photos_Achievements(SpriteBatch spriteBatch) {

        int yOffset = 0;

        foreach (Toast toast in Photo_Toasts) {

            toast.Position = new Vector2(600, 100 + yOffset + (int)ScrollOffset);
            toast.Draw(spriteBatch);

            yOffset += 100;
        }
    }

    void Draw_Floatsam_Achievements(SpriteBatch spriteBatch) {

        int yOffset = 0;

        foreach (Toast toast in Flotsam_Toasts) {

            toast.Position = new Vector2(600, 100 + yOffset + (int)ScrollOffset);
            toast.Draw(spriteBatch);

            yOffset += 100;
        }
    }

    void Draw_Game_Achievements(SpriteBatch spriteBatch) {

        int yOffset = 0;

        for (int i = 0; i < Game_Toasts.Count; i++) {

            Game_Toasts[i].Position = new Vector2(600, 100 + yOffset + (int)ScrollOffset);
            Game_Toasts[i].Draw(spriteBatch);

            if (Game_Toasts_Unlockable[i] != null) {

                Game_Toasts_Unlockable[i].Position = new Vector2(1450, 100 + yOffset + (int)ScrollOffset);
                Game_Toasts_Unlockable[i].Draw(spriteBatch);
            }

            yOffset += 100;
        }
    }

    void DrawGroups(SpriteBatch spriteBatch) {

        for (int i = 0; i < leftMenuToast.Length; i++) {

            if (i == SelectedMenuIndex) {

                leftMenuToast[i].Colour = groupHotColour;
                leftMenuToast[i].TextColour = groupTextColdColour;
                leftMenuToast[i].IconColour = groupTextColdColour;
            }
            else {

                leftMenuToast[i].Colour = groupColdColour;
                leftMenuToast[i].TextColour = groupTextHotColour;
                leftMenuToast[i].IconColour = groupTextHotColour;
            }

            leftMenuToast[i].Draw(spriteBatch);
        }

    }

    public void OnSceneStart() {

        // ---------------------------------------------------------------------------------
        // ----- Photos Achievements -------------------------------------------------------
        // ---------------------------------------------------------------------------------

        List<AchievementToast> Photo_Achievements = GetAllToastObjects("Photos", true);
        Photo_Achievements.AddRange(GetAllToastObjects("Photos", false));

        Photo_Toasts = new List<Toast>();
        for (int i=0; i<Photo_Achievements.Count; i++) {

            Photo_Toasts.Add(new Toast(new Vector2(0, 0)) {

                Width = 800,
                Title = Photo_Achievements[i].name,
                Description = Photo_Achievements[i].description,
                IconIndex = Photo_Achievements[i].iconIndex,
                Colour = Photo_Achievements[i].isAchieved ? achievementHotColour : achievementColdColour,
                IconColour = Photo_Achievements[i].isAchieved ? Color.Black : Color.White,
                TextColour = Photo_Achievements[i].isAchieved ? Color.Black : Color.White,
                // awardedAt = Photo_Achievements[i].awarded
            });
        }
        Photo_Toasts_Unlockable = new Toast[Photo_Achievements.Count];

        // ---------------------------------------------------------------------------------
        // ----- Flotsam Achievements ------------------------------------------------------
        // ---------------------------------------------------------------------------------

        List<AchievementToast> Flotsam_Achievements = GetAllToastObjects("Flotsam", true);
        Flotsam_Achievements.AddRange(GetAllToastObjects("Flotsam", false));

        Flotsam_Toasts = new List<Toast>();
        for (int i=0; i<Flotsam_Achievements.Count; i++) {

            Flotsam_Toasts.Add(new Toast(new Vector2(0, 0)) {

                Width = 800,
                Title = Flotsam_Achievements[i].name,
                Description = Flotsam_Achievements[i].description,
                IconIndex = Flotsam_Achievements[i].iconIndex,
                Colour = Flotsam_Achievements[i].isAchieved ? achievementHotColour : achievementColdColour,
                IconColour = Flotsam_Achievements[i].isAchieved ? Color.Black : Color.White,
                TextColour = Flotsam_Achievements[i].isAchieved ? Color.Black : Color.White,
                // awardedAt = Photo_Achievements[i].awarded
            });
        }
        Flotsam_Toasts_Unlockable = new Toast[Flotsam_Achievements.Count];

        // ---------------------------------------------------------------------------------
        // ----- Game Achievements --------------------------------------------------------
        // ---------------------------------------------------------------------------------

        List<AchievementToast> Game_Achievements = GetAllToastObjects("Game", true);
        Game_Achievements.AddRange(GetAllToastObjects("Game", false));

        Game_Toasts = new List<Toast>();
        Game_Toasts_Unlockable = new Toast[Game_Achievements.Count];
        for (int i=0; i<Game_Achievements.Count; i++) {

            Game_Toasts.Add(new Toast(new Vector2(0, 0)) {

                Width = 800,
                Title = Game_Achievements[i].name,
                Description = Game_Achievements[i].description,
                IconIndex = Game_Achievements[i].iconIndex,
                Colour = Game_Achievements[i].isAchieved ? achievementHotColour : achievementColdColour,
                IconColour = Game_Achievements[i].isAchieved ? Color.Black : Color.White,
                TextColour = Game_Achievements[i].isAchieved ? Color.Black : Color.White,
                // awardedAt = Photo_Achievements[i].awarded
            });

            if (Game_Achievements[i].name == "Sweet Sixteen Sovereign")
                Game_Toasts_Unlockable[i] = new Toast(new Vector2(0, 0)) {

                    Caption = "Shape Shifting",
                    IconIndex = Game_Achievements[i].isAchieved ? 11 : 10,
                    Colour = Game_Achievements[i].isAchieved ? achievementHotColour : achievementColdColour,
                    IconColour = Game_Achievements[i].isAchieved ? Color.Black : Color.White,
                    TextColour = Game_Achievements[i].isAchieved ? Color.Black : Color.White
                };
        }


    }

    public void OnSceneEnd() { }

    enum MenuItems {

        Overview = 0,
        Photos = 1,
        Floatsam = 2,
        Game = 3,
        Misc = 4
    }

    enum Cheats {

        ShapeShifting = 0,
    }

    int selectedCheatIndex = 0;                                                                              // The index of the selected menu item (variable)
    int SelectedCheataIndex {                                                                                 // The index of the selected menu item (property)

        get => selectedCheatIndex;                                                                           // Get the selected menu index
        set {
            if (value < 0)                                                                                  // If the value is less than 0 (less than the first menu item)
                selectedCheatIndex = Enum.GetValues(typeof(Cheats)).Length - 1;                           // Set the selected menu index to the last menu item
            else if (value > Enum.GetValues(typeof(Cheats)).Length - 1)                                  // If the value is greater than the last menu item
                selectedCheatIndex = 0;                                                                      // Set the selected menu index to the first menu item
            else                                                                                            // Otherwise
                selectedCheatIndex = value;                                                                  // Set the selected menu index to the value

            SLib.Click.Play(GameState.SFXVolume, 0, 0);                                                     // Play the click sound
        }
    }



    public List<AchievementToast> GetAllToastObjects(string group, bool achieved) {

        List<AchievementToast> toasts = new ();
        FieldInfo[] fields = GameState.Achievements.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields) {


            if (field.FieldType != typeof(AchievementToast))
                continue;

            AchievementToast toastInstance = field.GetValue(GameState.Achievements) as AchievementToast;

            if (toastInstance == null)
                continue;

            string instanceGroup = toastInstance.group;
            bool instanceAchieved = toastInstance.isAchieved;

            if (instanceGroup == group && instanceAchieved == achieved)
                toasts.Add(toastInstance);
        }

        return toasts;
    }
}
