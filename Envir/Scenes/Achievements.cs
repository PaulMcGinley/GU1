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
    List<Toast> Flotsam_Toasts = new ();
    List<Toast> Game_Toasts = new ();

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

    public void Initialize(GraphicsDevice device) {

        float yOffsetForCentering = (device.Viewport.Height - (leftMenuToast.Length * 100)) / 2;

        leftMenuToast[0] = new Toast(new Vector2(100, 0 + yOffsetForCentering)) {

            Caption = "Overview",
            IconIndex = 0,
        };

        leftMenuToast[1] = new Toast(new Vector2(100, 100 + yOffsetForCentering)) {

            Caption = "Photos",
            IconIndex = 1
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

            Caption = "Misc",
            IconIndex = 4
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
                break;
        }
    }

    public void FixedUpdate(GameTime gameTime) {}

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();

        spriteBatch.Draw(TLib.mainMenuBackground, new Rectangle(0, 0, 1920, 1080), Color.White);

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
                break;
        }

        spriteBatch.End();
    }

    void Draw_Overview(SpriteBatch spriteBatch) {

        // Draw the overview screen
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

        foreach (Toast toast in Game_Toasts) {

            toast.Position = new Vector2(600, 100 + yOffset + (int)ScrollOffset);
            toast.Draw(spriteBatch);

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
                TextColour = Photo_Achievements[i].isAchieved ? Color.Black : Color.White
            });
        }

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
                TextColour = Flotsam_Achievements[i].isAchieved ? Color.Black : Color.White
            });
        }

        // ---------------------------------------------------------------------------------
        // ----- Game Achievements --------------------------------------------------------
        // ---------------------------------------------------------------------------------

        List<AchievementToast> Game_Achievements = GetAllToastObjects("Game", true);
        Game_Achievements.AddRange(GetAllToastObjects("Game", false));

        Game_Toasts = new List<Toast>();
        for (int i=0; i<Game_Achievements.Count; i++) {

            Game_Toasts.Add(new Toast(new Vector2(0, 0)) {

                Width = 800,
                Title = Game_Achievements[i].name,
                Description = Game_Achievements[i].description,
                IconIndex = Game_Achievements[i].iconIndex,
                Colour = Game_Achievements[i].isAchieved ? achievementHotColour : achievementColdColour,
                IconColour = Game_Achievements[i].isAchieved ? Color.Black : Color.White,
                TextColour = Game_Achievements[i].isAchieved ? Color.Black : Color.White
            });
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



    public List<AchievementToast> GetAllToastObjects(string group, bool achieved) {

        List<AchievementToast> toasts = new List<AchievementToast>();
        FieldInfo[] fields = GameState.Achievements.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields) {


            if (field.FieldType != typeof(AchievementToast))
                continue;

            AchievementToast toastInstance = field.GetValue(GameState.Achievements) as AchievementToast;

            if (toastInstance == null)
                continue;

            string instanceGroup = toastInstance.group; // Assuming Group is a property
            bool instanceAchieved = toastInstance.isAchieved; // Assuming isAchieved is a property

            if (instanceGroup == group && instanceAchieved == achieved)
                toasts.Add(toastInstance);

            // PropertyInfo pGroup = field.FieldType.GetProperty("group");
            // PropertyInfo pAchieved = field.FieldType.GetProperty("isAchieved");

            // bool groupMatch = (pGroup.GetValue(toastInstance) as string) == group;
            // bool achievedMatch = (bool)pAchieved.GetValue(toastInstance) == achieved;

            // if (groupMatch && achievedMatch)
            //     toasts.Add(toastInstance);
        }

        return toasts;
    }
}
