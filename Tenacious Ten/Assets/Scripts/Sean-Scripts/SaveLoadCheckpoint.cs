using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadCheckpoint
{
    public static void SaveLevelCheckPointData(Checkpoint checkpointFile)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/checkpoint.citrus", FileMode.Create);

        CheckpointData data = new CheckpointData(checkpointFile);

        bf.Serialize(stream, data);
        stream.Close();
    }
    public static float[] LoadLevelCheckPointData()
    {
        if (File.Exists(Application.persistentDataPath + "/checkpoint.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/checkpoint.citrus", FileMode.Open);

            CheckpointData data = bf.Deserialize(stream) as CheckpointData;

            stream.Close();
            return data.CheckpointPos;
        }
        else
        {
            Debug.LogError("File Doesnt Exist from Serialize Load");
            return new float[3];
        }
    }
}

[System.Serializable]
public class CheckpointData
{
    public float[] CheckpointPos;
    public CheckpointData(Checkpoint checkpointFile)
    {
        CheckpointPos = new float[4];
        CheckpointPos[0] = checkpointFile.checkPointPos[0];
        CheckpointPos[1] = checkpointFile.checkPointPos[1];
        CheckpointPos[2] = checkpointFile.checkPointPos[2];
        //Scene Num
        CheckpointPos[3] = checkpointFile.checkPointPos[3];
    }
}