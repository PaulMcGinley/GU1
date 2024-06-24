using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Libraries;

public static class Fonts {

    public static SpriteFont DebugFont;

    public static SpriteFont MainMenuFont;
    public static SpriteFont LeaderboardFont;

    public static SpriteFont PhotoNameFont;
    public static SpriteFont PhotoDateFont;

    public static SpriteFont AchievementTitleFont;
    public static SpriteFont AchievementDescriptionFont;

    public static void Initialize(ContentManager content) {

        DebugFont = content.Load<SpriteFont>("Fonts/Debug");
        MainMenuFont = content.Load<SpriteFont>("Fonts/MenuText");
        LeaderboardFont = content.Load<SpriteFont>("Fonts/LeaderBoardText");

        PhotoNameFont = content.Load<SpriteFont>("Fonts/PhotoName");
        PhotoDateFont = content.Load<SpriteFont>("Fonts/PhotoTimeStampFont");

        AchievementTitleFont = content.Load<SpriteFont>("Fonts/AchievementTitle");
        AchievementDescriptionFont = content.Load<SpriteFont>("Fonts/AchievementBody");
    }
}
