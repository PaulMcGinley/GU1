using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GU1.Envir.Libraries;

public static class Sounds {

    public static Song MenuMusic;
    public static Song[] GameMusic;

    // Sound effects
    public static SoundEffect Ambient;
    public static SoundEffect Click;
    public static SoundEffect[] Camera;
    public static SoundEffect[] Collect;
    public static SoundEffect Ding;
    public static SoundEffect Victory;
    public static SoundEffect Whistle;
    public static SoundEffect Achievement;

    // Music

    public static void Initialize(ContentManager content) {

        // Load music
        MenuMusic = content.Load<Song>("Audio/Music/Menu-Music");

        GameMusic = new Song[15] {
            content.Load<Song>("Audio/Music/amazing-grace"),
            content.Load<Song>("Audio/Music/arewell-to-lochaber"),
            content.Load<Song>("Audio/Music/auld-lang-syne"),
            content.Load<Song>("Audio/Music/barbara-allen"),
            content.Load<Song>("Audio/Music/glenogie"),
            content.Load<Song>("Audio/Music/hush-ye-my-bairnie"),
            content.Load<Song>("Audio/Music/johnnie-cope"),
            content.Load<Song>("Audio/Music/leezie-lindsay"),
            content.Load<Song>("Audio/Music/mary-morison"),
            content.Load<Song>("Audio/Music/oh-charlie-is-my-darling"),
            content.Load<Song>("Audio/Music/oh-rowan-tree"),
            content.Load<Song>("Audio/Music/skye-boat-song"),
            content.Load<Song>("Audio/Music/the-campbells-are-comin"),
            content.Load<Song>("Audio/Music/the-water-is-wide"),
            content.Load<Song>("Audio/Music/yon-bonnie-banks-loch-lomond")
        };

        // Load sound effects
        Ambient = content.Load<SoundEffect>("Audio/ambient");
        Click = content.Load<SoundEffect>("Audio/click");
        Camera = new SoundEffect[2] {

            content.Load<SoundEffect>("Audio/camera1"),
            content.Load<SoundEffect>("Audio/camera2")
        };
        Collect = new SoundEffect[2] {

            content.Load<SoundEffect>("Audio/collect1"),
            content.Load<SoundEffect>("Audio/collect2")
        };
        Ding = content.Load<SoundEffect>("Audio/ding");
        Victory = content.Load<SoundEffect>("Audio/victory");
        Whistle = content.Load<SoundEffect>("Audio/whistle");

        Achievement = content.Load<SoundEffect>("Audio/achievement");
    }

}
