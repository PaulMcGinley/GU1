/*
    This class will hold on the game variables and the game state.
    It will be serialized to an XML file to save the game state.
*/

using System.Collections.Generic;
using System.Xml.Serialization;

namespace GU1;

public class GameState {

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene CurrentScene = GameScene.MainMenu;                                              // The current scene of the game

    // Players

    [XmlElement(Order = 1)]
    public List<Models.Flotsam> Flotsam = new();

}
