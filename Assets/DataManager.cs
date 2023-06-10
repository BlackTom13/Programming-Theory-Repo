using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager dM;

    public string currentName;
    public string playerName;
    public int playerScore;

    private void Awake()
    {
        if (dM != null)
        {
            Destroy(gameObject);
            return;
        }
        dM = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.name = playerName;
        data.score = playerScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.name;
            playerScore = data.score;
        }
    }

}
