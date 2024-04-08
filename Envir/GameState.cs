using System.Collections.Generic;
using System.Xml.Serialization;

namespace GU1;

public class GameState {

    [XmlIgnore]
    public static GameScene CurrentScene = GameScene.Playing;                                              // The current scene of the game

    // Players

    [XmlElement(Order = 1)]
    public List<Models.Flotsam> Flotsam = new();

}
