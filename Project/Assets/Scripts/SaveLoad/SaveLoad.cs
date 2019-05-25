
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad {

    private static string path = Application.persistentDataPath + "/game.pipo";

    public static void saveGameData(GameDataSerializable data) {
        BinaryFormatter formatter = new BinaryFormatter();        

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("YEAH!");       
    }

    public static GameDataSerializable loadGameData() {
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();        

            FileStream stream = new FileStream(path, FileMode.Open);

            GameDataSerializable data = formatter.Deserialize(stream) as GameDataSerializable;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in path: " + path);
            return null;
        }
    }
}