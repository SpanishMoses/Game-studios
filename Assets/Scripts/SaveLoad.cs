using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveLoad : MonoBehaviour
{

    //help from https://www.youtube.com/watch?v=Rgxbl5uIKO0&ab_channel=GameGrind

    public static void Save<T>(T objectToSave, string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSave);
        }
        Debug.Log("Saved");
    }

        public static T Load<T>(string key)
        {
            string path = Application.persistentDataPath + "/saves/";
            BinaryFormatter formatter = new BinaryFormatter();
            T returnValue = default(T);
            using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open))
            {
                 returnValue = (T)formatter.Deserialize(fileStream);
            }
        return returnValue;
        }

    public static bool SaveExists(string key){
        string path = Application.persistentDataPath + "/saves/" + key + ".txt";
        return File.Exists(path);
    }

    public static void SeriouslyDeleteAllSaveFiles(){
        string path = Application.persistentDataPath + "/saves/";
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete();
        Directory.CreateDirectory(path);
    }
}
