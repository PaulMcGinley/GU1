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

namespace GU1;

internal static class Program {

    private static void Main() {

        if (!Directory.Exists(Photo.SaveDir))
            Directory.CreateDirectory(Photo.SaveDir);

        using Game game = new GameMain();
        game.Run();
    }
}

// Required font: https://www.1001freefonts.com/optien.font
// https://www.fontspace.com/casanova-scotia-font-f29908
// https://greatdocbrown.itch.io/gamepad-ui?download