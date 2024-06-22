using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using GU1.Envir.Scenes;
using Microsoft.Xna.Framework.Media;

namespace GU1.Envir;

public static class GameState {

    // Create an event handler for the full screen change
    public static event EventHandler IsFullScreenChanged;

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

    [XmlIgnore]
    static bool isFullScreen = false;
    public static bool IsFullScreen {
        get => isFullScreen;
        set {
            isFullScreen = value;
            OnIsFullScreenChanged();
        }
    }

    static void OnIsFullScreenChanged() {
        IsFullScreenChanged?.Invoke(null, EventArgs.Empty);
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

        using BinaryWriter writer = new(File.Create(SettingsFile));                                         // Create the settings file

        writer.Write("Sightings Settings File");                                                            // Header
        writer.Write((byte)1);                                                                              // Version number (Used to upgrade user settings in the future without losing them)

        // ----- Version 1 --------------------------------------------------
        writer.Write(MusicVolume);
        writer.Write(SFXVolume);
        writer.Write(MaxPhotos);
        writer.Write(ControllerSensitivity);
        writer.Write(IsFullScreen);
        // ----- Version 2 --------------------------------------------------
        // Nessie / Tourist split
        // Floatsam count to spawn
    }

    public static void LoadSettings() {

        if (!File.Exists(SettingsFile)) {                                                                   // If the file does not exist

            Settings.SetDefaultValues();                                                                    // Set the default values
            return;                                                                                         // Return
        }

        // Read the file

        using BinaryReader reader = new(File.Open(SettingsFile, FileMode.Open));                            // Open the settings file

        string header = reader.ReadString();                                                                // Make sure the file is a settings file by checking the header

        if (header != "Sightings Settings File") {                                                          // If the header is not correct

            Settings.SetDefaultValues();                                                                    // Set the default values
            return;                                                                                         // Return
        }

        // Load the settings from the file

        byte version = reader.ReadByte();                                                                   // Version number not used yet

        // ----- Version 1 --------------------------------------------------
        MusicVolume = reader.ReadSingle();
        SFXVolume = reader.ReadSingle();
        MaxPhotos = reader.ReadInt32();
        ControllerSensitivity = reader.ReadSingle();
        IsFullScreen = reader.ReadBoolean();

        // ----- Version 2 --------------------------------------------------
        // Nessie / Tourist split
        // Floatsam count to spawn
    }

}
