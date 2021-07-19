using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instatnce { get; private set; }
    public string PlayerName { get; private set; }
    private int bestScore;
    private string bestPlayer;
    public List<HighScoreEntry> scores;
    public Color ballColor;
    public Color paddleColor;

    private void Awake()
    {
        if (Instatnce != null)
        {
            Destroy(gameObject);
            return;
        }
        Instatnce = this;
        DontDestroyOnLoad(gameObject);
        PlayerName = "Player";
        scores = new List<HighScoreEntry>();
        if (!LoadHighScores())
        {
            InitializeTable();
        }
        InitializeBestScore();
        if(!LoadSettings())
        {   
            //if cannot load settings from disk initialize with default values
            ballColor = Color.red;
            paddleColor = Color.red;
        }
        
    }
    public void UpdateBestScore(int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
            bestPlayer = PlayerName;
        }
    }
    public void SetPlayerName(string name)
    {
        PlayerName = name;
        if (bestScore == 0)
        {
            bestPlayer = name;
        }
    }
    public string GetBestScoreText()
    {
        return $"{"Best Score: "} {bestPlayer}{":"} {bestScore}";
    }
    public void SaveHighScores()
    {
        Table table = new Table() { scores = new List<string>() };
        foreach (var item in scores)
        {
            string entry = item.s_Name + " " + item.s_Score;
            table.scores.Add(entry);
        }
        string json = JsonUtility.ToJson(table);
        File.WriteAllText(Application.persistentDataPath + "/table.json", json);
    }
    public bool LoadHighScores()
    {
        string path = Application.persistentDataPath + "/table.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var table = JsonUtility.FromJson<Table>(json);
            foreach (var item in table.scores)
            {
                var row = item.Split(' ');
                scores.Add(new HighScoreEntry() { s_Name = row[0], s_Score = int.Parse(row[1]) });
            }
            return true;
        }
        return false;
    }
    private void InitializeBestScore()
    {
        int maxScore = scores.Max(e => e.s_Score);
        var entry = scores.Find(e => e.s_Score == maxScore);
        bestPlayer = entry.s_Name;
        bestScore = entry.s_Score;
    }
    public void UpdateHighScores(int score)
    {
        UpdateBestScore(score);
        int min = scores.Min(e => e.s_Score);
        if (score > min)
        {
            var entryToRemove = scores.Find(e => e.s_Score == min);
            scores.Remove(entryToRemove);
            scores.Add(new HighScoreEntry() { s_Name = PlayerName, s_Score = score });
        }
        
    }

    // if cannot load data source from file, initialize it with default values
    private void InitializeTable()
    {
        scores.Add(new HighScoreEntry() { s_Name = "PlayerOne", s_Score = 0 });
        scores.Add(new HighScoreEntry() { s_Name = "PlayerTwo", s_Score = 0 });
        scores.Add(new HighScoreEntry() { s_Name = "PlayerThree", s_Score = 0 });
        scores.Add(new HighScoreEntry() { s_Name = "PlayerFour", s_Score = 0 });
        scores.Add(new HighScoreEntry() { s_Name = "PlayerFive", s_Score = 0 });
    }
    private bool LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var settingsData = JsonUtility.FromJson<GameSettings>(json);
            ballColor = settingsData.ballColor;
            paddleColor = settingsData.paddleColor;
            return true;
        }
        return false;
    }
}
[System.Serializable]
class Table
{
    public List<string> scores;
}
public class HighScoreEntry
{
    public string s_Name;
    public int s_Score;
}
[System.Serializable]
class GameSettings
{
   public Color ballColor;
   public Color paddleColor;
}





