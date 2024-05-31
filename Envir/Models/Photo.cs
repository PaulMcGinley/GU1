using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace GU1.Envir.Models;

public class Photo {

    //Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml").Last()
    //$"{DateTime.Now.ToBinary()}.xml"

    string SaveDir => $"{Directory.GetCurrentDirectory()}/Photos";

    public List<Content> content;

    public Vector2 location; // On the map

    public string fileName; // Save file path

    public string photographer;

    public DateTime timeStamp;
    public string Date => timeStamp.ToString("yyyy-MM-dd");
    public string Time => timeStamp.ToString("HH:mm");

    public Photo Load(string fileName) {

        string _path = $"{SaveDir}/{fileName}.xml";

        if (!File.Exists(_path))
            throw new FileNotFoundException($"File not found: {_path}");

        this.fileName = fileName;

        // Load the photo from the path
        return XMLSerializer.Deserialize<Photo>(_path);
    }

    public void Save() {

        if (fileName == null)
            fileName = $"{DateTime.Now.ToBinary()}";

        string _path = $"{SaveDir}/{fileName}.xml";

        if (!Directory.Exists(SaveDir))
            Directory.CreateDirectory(SaveDir);

        // Save the photo to the path
        XMLSerializer.Serialize<Photo>(_path, this);
    }

    // public void Optimize() {

    //     foreach (var item in content) {

    //         if (item.isBoat) continue;
    //         if (item.isNessie) continue;

    //         if(item.position.X < location.X - 128) content.Remove(item);
    //         else if(item.position.X > location.X + 128) content.Remove(item);
    //         else if(item.position.Y < location.Y - 128) content.Remove(item);
    //         else if(item.position.Y > location.Y + 128) content.Remove(item);
    //     }
    // }

    public class Content {

        public Vector2 position;
        public float rotation;
        public int spriteID;
        public bool isFlipped;
        public bool isNessie;
        public bool isBoat;
    }
}
