using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class serialize : MonoBehaviour {
    public static class SaveLoad
    {
        public static List<Game> savedGames = new List<Game>();

        public static void Save()
        {
            savedGames.Add(Game.current);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/savedGames.citrus");
            bf.Serialize(file, SaveLoad.savedGames);
            file.Close();
        }
        public static void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/savedGames.citrus"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.citrus", FileMode.Open);
                SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
                file.Close();
            }
        }

    }

    [System.Serializable]
    public class Game
    {
        public static Game current;
        public SaveFile save;

        public Game()
        {
            save = new SaveFile();
        }
    }
}
