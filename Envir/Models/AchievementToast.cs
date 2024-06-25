using System;

namespace GU1.Envir.Models;

public class AchievementToast {

    public string group;

    public bool isAchieved;
    public DateTime awarded = DateTime.MinValue;

    public int iconIndex;
    public string name;
    public string description;

    public AchievementToast() { }
}
