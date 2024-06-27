/*
 * This is the content of the drawable Toast object
*/

using System;

namespace GU1.Envir.Models;

public class AchievementToast {

    public string group;                                                                                    // This is the group they will appear in on the achievement scene (Photos, Floatsam, Game)

    public bool isAchieved;                                                                                 // This is the flag to determine if the achievement has been achieved
    public DateTime awarded = DateTime.MinValue;                                                            // This is the date the achievement was awarded, minimum value means it has not been awarded

    public int iconIndex;                                                                                   // From the TLib.AchievementIcons array, which index to draw
    public string name;                                                                                     // The name of the achievement appearing on the toast
    public string description;                                                                              // The description of the achievement appearing on the toast

    public AchievementToast() { }                                                                           // Empty constructor
}
