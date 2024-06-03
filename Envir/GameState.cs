﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace GU1.Envir;

public static class GameState {

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene CurrentScene = GameScene.MainMenu;                                              // The current scene of the game

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene PreviousScene = GameScene.None;                                                 // The previous scene of the game

    [XmlElement(Order = 1)]
    public static List<Flotsam> Flotsam = new();                                                            // The flotsam in the game

    [XmlElement(Order = 2)]
    public static List<Player> Players = new();                                                             // The players in the game

    [XmlElement(Order = 3)]
    public static Boat Boat = new(-1);                                                                      // The boat in the game

}
