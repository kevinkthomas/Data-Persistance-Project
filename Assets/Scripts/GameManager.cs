using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string CurrentName;
    public string HighScoreName;
    public int HighScore;
    public List<HighScoreEntry> TopHighScores = new List<HighScoreEntry>();

    protected int highScoreCount = 10;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadGameData();
    }

    public void SetHighScore(int newHighScore)
    {
        HighScore = newHighScore;
        HighScoreName = CurrentName;
    }

    public void AddHighScoreToTable(int points)
    {
        if (TopHighScores.Count < highScoreCount || points > TopHighScores[TopHighScores.Count-1].HighScore)
        {
            HighScoreEntry newEntry = new HighScoreEntry();
            newEntry.HighScoreName = CurrentName;
            newEntry.HighScore = points;

            TopHighScores.Add(newEntry);

            TopHighScores.Sort((x, y) => y.HighScore.CompareTo(x.HighScore));

            if (TopHighScores.Count > highScoreCount)
            {
                TopHighScores.RemoveAt(highScoreCount);
            }
        }
    }

    public string GetHighScoreString()
    {
        return "Best Score: " + HighScoreName + " - " + HighScore.ToString();
    }

    void OnApplicationQuit()
    {
        SaveGameData();
    }

    [System.Serializable]
    public class HighScoreEntry
    {
        public string HighScoreName;
        public int HighScore;
    }

    [System.Serializable]
    class SaveData
    {
        public string CurrentName;
        public string HighScoreName;
        public int HighScore;
        public List<HighScoreEntry> TopHighScores;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.CurrentName = CurrentName;
        data.HighScoreName = HighScoreName;
        data.HighScore = HighScore;
        data.TopHighScores = TopHighScores;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            CurrentName = data.CurrentName;
            HighScoreName = data.HighScoreName;
            HighScore = data.HighScore;
            TopHighScores = data.TopHighScores;
        }
    }
}
