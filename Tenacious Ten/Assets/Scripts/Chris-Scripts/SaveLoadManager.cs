using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager{
    public static void SaveLevelData(SaveFile savefile)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/savefile.citrus", FileMode.Create);
        SaveData data = new SaveData(savefile);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadLevelData()
    {
        if (File.Exists(Application.persistentDataPath + "/savefile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/savefile.citrus", FileMode.Open);

            SaveData data = bf.Deserialize(stream) as SaveData;

            stream.Close();
            return data.LevelNum;
        }
        else
        {
            Debug.Log("Could not Deserialize save data");
            return 1;
        }
    }

    public static float LoadVolumeData()
    {
        if (File.Exists(Application.persistentDataPath + "/savefile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/savefile.citrus", FileMode.Open);

            SaveData data = bf.Deserialize(stream) as SaveData;

            stream.Close();
            return data.Volume;
        }
        else
        {
            Debug.Log("Could not Deserialize save data");
            return 1;
        }

    }

    public static void DeleteLevelData()
    {
        if(File.Exists(Application.persistentDataPath + "/savefile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/savefile.citrus", FileMode.Create);
            SaveData data = new SaveData();

            bf.Serialize(stream, data);
            stream.Close();
        }
    }
}

[System.Serializable]
public class SaveData
{
    public int LevelNum;
    public float Volume;
    public SaveData(SaveFile savefile)
    {
        LevelNum = savefile.currentLevel;
        Volume = savefile.currentVolume;
    }
    public SaveData()
    {
        LevelNum = 1;
        Volume = 1;
    }
}
