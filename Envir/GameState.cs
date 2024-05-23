using System.Collections.Generic;
using System.Xml.Serialization;

namespace GU1;

public class GameState {

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene CurrentScene = GameScene.MainMenu;                                              // The current scene of the game

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene PreviousScene = GameScene.None;                                             // The previous scene of the game

    [XmlElement(Order = 1)]
    public List<Flotsam> Flotsam = new();

    [XmlElement(Order = 2)]
    public List<Actor> Actors = new();
}
