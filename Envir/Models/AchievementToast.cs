using System;

namespace GU1.Envir.Models;

public class AchievementToast {

    public bool isAchieved = false;
    public DateTime awarded = DateTime.MinValue;

    public int iconIndex;
    public string name;
    public string description;
}
