using System.Collections.Generic;
using System.Xml.Serialization;

namespace GU1.Envir;

public static class GameState {

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene CurrentScene = GameScene.MainMenu;                                              // The current scene of the game

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene PreviousScene = GameScene.None;                                                 // The previous scene of the game

    [XmlIgnore]
    static float musicVolume = 0.5f;                                                                        // The volume of the music
    [XmlIgnore]
    public static float MusicVolume {                                                                       // The volume of the music
        get => musicVolume;                                                                                 // Get the volume of the music
        set {                                                                                               // Set the volume of the music
            if (value < 0)
                musicVolume = 0;
            else if (value > 1)
                musicVolume = 1;
            else
                musicVolume = value;

            SLib.Click.Play(musicVolume, 0,0);
        }
    }

    [XmlIgnore]
    static float sfxVolume = 0.5f;                                                                          // The volume of the sound effects
    [XmlIgnore]
    public static float SFXVolume {                                                                         // The volume of the sound effects
        get => sfxVolume;                                                                                   // Get the volume of the sound effects
        set {                                                                                               // Set the volume of the sound effects
            if (value < 0)
                sfxVolume = 0;
            else if (value > 1)
                sfxVolume = 1;
            else
                sfxVolume = value;

            SLib.Click.Play(sfxVolume, 0,0);
        }
    }

    [XmlElement(Order = 1)]
    public static List<Flotsam> Flotsam = new();                                                            // The flotsam in the game

    [XmlElement(Order = 2)]
    public static List<Player> Players = new();                                                             // The players in the game

    [XmlElement(Order = 3)]
    public static Boat Boat = new(-1);                                                                      // The boat in the game

}
