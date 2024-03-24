using System.Collections.Generic;

namespace GU1;

public static class GameState {

    public static GameScene CurrentScene { get; set; } = GameScene.Playing;

    public static List<Player> Players { get; set; } = new List<Player>();

}
