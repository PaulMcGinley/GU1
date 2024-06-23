global using E = GU1.Engine;
global using GU1.Engine.Extensions;
global using GU1.Engine.Interfaces;
global using GU1.Engine.IO;
global using GU1.Engine.Models;
global using static GU1.Engine.Constants;
global using static GU1.Engine.IO.DeviceState;
global using static GU1.Engine.Graphics.Draw;

global using GU1.Envir;
global using GU1.Envir.Models;

global using FLib = GU1.Envir.Libraries.Fonts;
global using TLib = GU1.Envir.Libraries.Texture;
global using RLib = GU1.Envir.Libraries.Rumble;
global using SLib = GU1.Envir.Libraries.Sounds;

using System.IO;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;

namespace GU1;

internal static class Program {

    private static void Main() {

        // Set the save directory for photos (Operating System specific)
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Photo.SaveDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Sightings/Photos";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Photo.SaveDir = "~/.sightings/photos";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            Photo.SaveDir = "/Users/Shared/Sightings/Photos";

        // Create the save directory if it doesn't exist
        if (!Directory.Exists(Photo.SaveDir))
            Directory.CreateDirectory(Photo.SaveDir);

        // Create the game and run it
        using Game game = new GameMain();
        game.Run();
    }
}

// Required font:
//   - https://www.1001freefonts.com/optien.font
//   - https://www.dafont.com/hello-samosa.font
// https://greatdocbrown.itch.io/gamepad-ui?download