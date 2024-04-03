namespace GU1.Engine.IO;

public class XMLSerializer {

    static System.Xml.Serialization.XmlSerializer serializer;

    /// <summary>
    /// Serialize the data to the specified path
    /// </summary>
    /// <typeparam name="T">The type of data to serialize (class object)</typeparam>
    /// <param name="path"></param>
    /// <param name="data"></param>
    public static void Serialize<T>(string path, T data) {

        serializer = new(typeof(T));                                                                        // Create a new serializer for the specified type

        using System.IO.StreamWriter writer = new(path);                                                    // Create a new StreamWriter for the specified path
        serializer.Serialize(writer, data);                                                                 // Serialize the data to the specified path
    }

    /// <summary>
    /// Deserialize the data from the specified path
    /// </summary>
    /// <typeparam name="T">The type of data to serialize (class object)</typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T Deserialize<T>(string path) {

        serializer = new(typeof(T));                                                                        // Create a new serializer for the specified type

        using System.IO.StreamReader reader = new(path);                                                    // Create a new StreamReader for the specified path
        return (T)serializer.Deserialize(reader);                                                           // Deserialize the data from the specified path
    }
}
