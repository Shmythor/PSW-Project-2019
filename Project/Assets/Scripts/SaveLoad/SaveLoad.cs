
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad {

    private static string path = Application.persistentDataPath + "/game.pipo";

    public static string path_cell1 = Application.persistentDataPath + "/game1.data",
                        path_cell2 = Application.persistentDataPath + "/game2.data",
                        path_cell3 = Application.persistentDataPath + "/game3.data";
    public enum paths {path1, path2, path3};                    

    public static void saveGameData(GameDataSerializable data) {
        BinaryFormatter formatter = new BinaryFormatter();        

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("YEAH!");       
    }

    public static void saveGameDataAt(GameDataSerializable data, paths pathEnum) {
        if(pathEnum == paths.path1) path = path_cell1;
        if(pathEnum == paths.path2) path = path_cell2;
        if(pathEnum == paths.path3) path = path_cell3;

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

    public static GameDataSerializable loadGameDataAt(paths pathEnum) {
        if(pathEnum == paths.path1) path = path_cell1;
        if(pathEnum == paths.path2) path = path_cell2;
        if(pathEnum == paths.path3) path = path_cell3;

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

    public static GameDataSerializable[] loadAllGameData() {
        GameDataSerializable[] data = new GameDataSerializable[3];
        data[0] = loadGameDataAt(paths.path1);
        data[1] = loadGameDataAt(paths.path2);
        data[2] = loadGameDataAt(paths.path3);
        return data;
    }
}