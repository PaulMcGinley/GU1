using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Libraries;

public static class Fonts {

    public static SpriteFont DebugFont;

    public static SpriteFont MainMenuFont;
    public static SpriteFont LeaderboardFont;

    public static void Initialize(ContentManager content) {

        DebugFont = content.Load<SpriteFont>("Fonts/Debug");
        MainMenuFont = content.Load<SpriteFont>("Fonts/MenuText");
        LeaderboardFont = content.Load<SpriteFont>("Fonts/LeaderBoardText");
    }
}
