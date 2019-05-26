
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad {

    private static string path;

    private static string path_cell1 = Application.persistentDataPath + "/game1.data",
                        path_cell2 = Application.persistentDataPath + "/game2.data",
                        path_cell3 = Application.persistentDataPath + "/game3.data",
                        path_top1 = Application.persistentDataPath + "/top1.data",
                        path_top2 = Application.persistentDataPath + "/top2.data",
                        path_top3 = Application.persistentDataPath + "/top3.data";

    private static string path_usernames = Application.persistentDataPath + "/usernames.list";  

    public enum paths {cell1, cell2, cell3, top1, top2, top3};                   

    public static string[] getUserNames() {
        if(File.Exists(path_usernames)) {
            BinaryFormatter formatter = new BinaryFormatter(); 
            FileStream stream = new FileStream(path_usernames, FileMode.Open);

            if(stream.Length == 0) {
                stream.Close(); 
                return null;
            }

            UserNamesSerializable data = formatter.Deserialize(stream) as UserNamesSerializable;
            
            stream.Close(); 
            return data.usernames;
        } else {
            Debug.LogError("Save file not found in path: " + path_usernames);
            return null;
        }
    }

    public static void addUserNames(string[] usernames) {
        BinaryFormatter formatter = new BinaryFormatter(); 
        FileStream stream = new FileStream(path_usernames, FileMode.Create);

        formatter.Serialize(stream, new UserNamesSerializable(usernames));
        stream.Close();   
    }

    public static void saveGameDataAt(GameDataSerializable data, paths pathEnum) {
        if(pathEnum == paths.cell1) path = path_cell1;
        if(pathEnum == paths.cell2) path = path_cell2;
        if(pathEnum == paths.cell3) path = path_cell3;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void saveTopGameDataAt(TopGameData data, paths pathEnum) {
        if(data == null) return;
 
        if(pathEnum == paths.top1) path = path_top1;
        if(pathEnum == paths.top2) path = path_top2;
        if(pathEnum == paths.top3) path = path_top3;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameDataSerializable loadGameDataAt(paths pathEnum) {
        if(pathEnum == paths.cell1) path = path_cell1;
        if(pathEnum == paths.cell2) path = path_cell2;
        if(pathEnum == paths.cell3) path = path_cell3;

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

    public static TopGameData loadTopGameDataAt(paths pathEnum) {
        if(pathEnum == paths.top1) path = path_top1;
        if(pathEnum == paths.top2) path = path_top2;
        if(pathEnum == paths.top3) path = path_top3;

        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter(); 
            FileStream stream = new FileStream(path, FileMode.Open);

            TopGameData data = formatter.Deserialize(stream) as TopGameData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in path: " + path);
            return null;
        }
    }

    public static GameDataSerializable[] loadAllGameData() {
        GameDataSerializable[] data = new GameDataSerializable[3];
        data[0] = loadGameDataAt(paths.cell1);
        data[1] = loadGameDataAt(paths.cell2);
        data[2] = loadGameDataAt(paths.cell3);
        return data;
    }

    public static TopGameData[] loadAllTopGameData() {
        TopGameData[] data = new TopGameData[3];
        data[0] = loadTopGameDataAt(paths.top1);
        data[1] = loadTopGameDataAt(paths.top2);
        data[2] = loadTopGameDataAt(paths.top3);

        return testTopGameNotNull(data);
    }

    private static TopGameData[] testTopGameNotNull(TopGameData[] topData) {
        if(topData[0] == null) topData[0] = new TopGameData(0, 0, 0, 0);
        if(topData[1] == null) topData[1] = new TopGameData(0, 0, 0, 0);
        if(topData[2] == null) topData[2] = new TopGameData(0, 0, 0, 0);

        return topData;
    }
}