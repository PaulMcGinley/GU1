using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GU1.Envir.Models;

public class Photo {

    public List<Content> content;

    public Vector2 location; // On the map

    public string fileName; // Save file path

    public string photographer;

    public DateTime timeStamp;
    public string Date => timeStamp.ToString("yyyy-MM-dd");
    public string Time => timeStamp.ToString("HH:mm");

    public void Load(string fileName) {

        this.fileName = fileName;
    }

    public void Save(string fileName) {
        // Save the photo to the path
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
