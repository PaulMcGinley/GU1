using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Media;

namespace GU1.Envir;

public static class GameState {

    [XmlIgnore]
    public static string SettingsFile {
        get {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Sightings/settings.dat";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "~/.sightings/settings.dat";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "/Users/Shared/Sightings/settings.dat";

            return string.Empty;
        }
    }

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene CurrentScene = GameScene.MainMenu;                                              // The current scene of the game

    [XmlIgnore] // Ignore this property when serializing
    public static GameScene PreviousScene = GameScene.None;                                                 // The previous scene of the game

    [XmlIgnore]
    static float musicVolume = 1f;                                                                          // The volume of the music
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

            MediaPlayer.Volume = musicVolume;                                                               // Set the volume of the music
            SLib.Click.Play(musicVolume, 0, 0);                                                             // Play the click sound
        }
    }

    [XmlIgnore]
    static float sfxVolume = 1f;                                                                            // The volume of the sound effects
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

            SLib.Click.Play(sfxVolume, 0, 0);
        }
    }

    //[XmlIgnore]
    //public static float roleSplit = 0.33f;                                                                  // The split between the roles

    [XmlIgnore]
    static int maxPhotos = 5;
    public static int MaxPhotos {
        get => maxPhotos;
        set {
            if (value < 1)
                maxPhotos = int.MaxValue;
            else if (value > 25)
                maxPhotos = int.MaxValue;
            else
                maxPhotos = value;
        }
    }

    [XmlIgnore]
    static float controllerSensitivity = 10f;
    public static float ControllerSensitivity {
        get => controllerSensitivity;
        set {
            if (value < 1)
                controllerSensitivity = 100;
            else if (value > 100)
                controllerSensitivity = 1;
            else
                controllerSensitivity = value;
        }
    }

    [XmlElement(Order = 1)]
    public static List<Flotsam> Flotsam = new();                                                            // The flotsam in the game

    [XmlElement(Order = 2)]
    public static List<Player> Players = new();                                                             // The players in the game

    [XmlElement(Order = 3)]
    public static Boat Boat = new(-1);                                                                      // The boat in the game

    [XmlElement(Order = 4)]
    public static List<Cloud> Clouds = new();                                                               // The clouds in the game

    public static void SaveSettings() {

        using BinaryWriter writer = new(File.Open(SettingsFile, FileMode.Create));

        writer.Write(MusicVolume);
        writer.Write(SFXVolume);
        writer.Write(MaxPhotos);
        writer.Write(ControllerSensitivity);
    }

    public static void LoadSettings() {

        if (!File.Exists(SettingsFile))
            return;

        using BinaryReader reader = new(File.Open(SettingsFile, FileMode.Open));

        MusicVolume = reader.ReadSingle();
        SFXVolume = reader.ReadSingle();
        MaxPhotos = reader.ReadInt32();
        ControllerSensitivity = reader.ReadSingle();
    }

}
