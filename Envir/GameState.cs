using System.Collections.Generic;

namespace GU1;

public class GameState {

    public static GameScene CurrentScene = GameScene.Playing;                                              // The current scene of the game
    // Players

    // Flotsam and Jetsam
    public List<Models.Floatsam> Flotsam = new();

}
