// This block of code is a "global using directive" that allows the use of the Engine namespace and its members without specifying the namespace.
// For example, instead of writing "E.Models.Graphic2D" you can write "Graphic2D".
global using E = GU1.Engine;                                                                                // If we ever need to use the Engine namespace, we can use the alias "E"
global using GU1.Engine.Models;                                                                             // Allows easy access to the Models namespace and its members
global using GU1.Engine.Extensions;                                                                         // Allows easy access to the Extensions namespace and its members
global using static GU1.Engine.IO.DeviceState;                                                              // Advanced device state management
global using static GU1.Engine.Randoms;                                                                     // Allows the ability to create random numbers without creating an instance of the Random class

global using Libs = GU1.Envir.Libraries;                                                                    // Allows easy access to the Libraries namespace and its members

internal class Program {

    private static void Main(string[] args) {

        using var game = new GU1.GameMain();
        game.Run();       
    }
}
