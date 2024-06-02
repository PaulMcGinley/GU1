using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GU1.Envir.Libraries;

public static class Sounds {

    // Sound effects
    public static SoundEffect Ambient;
    public static SoundEffect Click;
    public static SoundEffect[] Camera;
    public static SoundEffect[] Collect;
    public static SoundEffect Ding;
    public static SoundEffect Victory;
    public static SoundEffect Whistle;

    // Music

    public static void Initialize(ContentManager content) {

        // Load sound effects
        Ambient = content.Load<SoundEffect>("Audio/ambient");
        Click = content.Load<SoundEffect>("Audio/click");
        Camera = new SoundEffect[2];
        Camera[0] = content.Load<SoundEffect>("Audio/camera1");
        Camera[1] = content.Load<SoundEffect>("Audio/camera2");
        Collect = new SoundEffect[2];
        Collect[0] = content.Load<SoundEffect>("Audio/collect1");
        Collect[1] = content.Load<SoundEffect>("Audio/collect2");
        Ding = content.Load<SoundEffect>("Audio/ding");
        Victory = content.Load<SoundEffect>("Audio/victory");
        Whistle = content.Load<SoundEffect>("Audio/whistle");

        // Load music
    }

}
