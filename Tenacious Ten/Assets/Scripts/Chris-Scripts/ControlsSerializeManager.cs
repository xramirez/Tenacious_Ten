using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class ControlsSerializeManager
{
    public static void SaveControlsData(ControlsSerialize controlsSave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/controlsfile.citrus", FileMode.Create);
        ControlsData data = new ControlsData(controlsSave);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static KeyCode Load_MoveLeft_Data()
    {
        if (File.Exists(Application.persistentDataPath + "/controlsfile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/controlsfile.citrus", FileMode.Open);

            ControlsData data = bf.Deserialize(stream) as ControlsData;

            stream.Close();
            return data.MoveLeft;
        }
        else
        {
            Debug.Log("Could not Deserialize save data");
            return KeyCode.LeftArrow;
        }
    }

    public static KeyCode Load_MoveRight_Data()
    {
        if (File.Exists(Application.persistentDataPath + "/controlsfile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/controlsfile.citrus", FileMode.Open);

            ControlsData data = bf.Deserialize(stream) as ControlsData;

            stream.Close();
            return data.MoveRight;
        }
        else
        {
            Debug.Log("Could not Deserialize save data");
            return KeyCode.RightArrow;
        }
    }

    public static KeyCode Load_Jump_Data()
    {
        if (File.Exists(Application.persistentDataPath + "/controlsfile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/controlsfile.citrus", FileMode.Open);

            ControlsData data = bf.Deserialize(stream) as ControlsData;

            stream.Close();
            return data.Jump;
        }
        else
        {
            Debug.Log("Could not Deserialize save data");
            return KeyCode.UpArrow;
        }
    }

    public static KeyCode Load_ShootLemon_Data()
    {
        if (File.Exists(Application.persistentDataPath + "/controlsfile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/controlsfile.citrus", FileMode.Open);

            ControlsData data = bf.Deserialize(stream) as ControlsData;

            stream.Close();
            return data.ShootLemon;
        }
        else
        {
            Debug.Log("Could not Deserialize save data");
            return KeyCode.Space;
        }
    }

    public static KeyCode Load_Pause_Data()
    {
        if (File.Exists(Application.persistentDataPath + "/controlsfile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/controlsfile.citrus", FileMode.Open);

            ControlsData data = bf.Deserialize(stream) as ControlsData;

            stream.Close();
            return data.Pause;
        }
        else
        {
            Debug.Log("Could not Deserialize save data");
            return KeyCode.Escape;
        }
    }

    public static void DeleteControlsData()
    {
        if (File.Exists(Application.persistentDataPath + "/controlsfile.citrus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/controlsfile.citrus", FileMode.Create);
            ControlsData data = new ControlsData();

            bf.Serialize(stream, data);
            stream.Close();
        }
    }
}

[System.Serializable]
public class ControlsData
{
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Jump;
    public KeyCode ShootLemon;
    public KeyCode Pause;
    public ControlsData(ControlsSerialize controlsSave)
    {
        MoveLeft = controlsSave._moveLeft;
        MoveRight = controlsSave._moveRight;
        Jump = controlsSave._jump;
        ShootLemon = controlsSave._shootLemon;
        Pause = controlsSave._pause;
    }
    public ControlsData()
    {
        /*
        MoveLeft = KeyCode.LeftArrow;
        MoveRight = KeyCode.RightArrow;
        Jump = KeyCode.UpArrow;
        ShootLemon = KeyCode.Space;
        Pause = KeyCode.Escape;
        */
        MoveLeft = KeyCode.J;
        MoveRight = KeyCode.K;
        Jump = KeyCode.I;
        ShootLemon = KeyCode.P;
        Pause = KeyCode.Escape;
    }
}
