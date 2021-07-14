using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instatnce { get; private set; }
    public string PlayerName { get; private set; }
    private int bestScore;
    private string bestPlayer;

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
        if (!LoadData())
        {
            bestPlayer = PlayerName;
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
    public void SaveData()
    {
        var saveData = new Data() { s_BestPlayer = bestPlayer, s_BestScore = bestScore };
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/gameData.json", json);
    }
    public bool LoadData()
    {
        string path = Application.persistentDataPath + "/gameData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<Data>(json);
            bestPlayer = data.s_BestPlayer;
            bestScore = data.s_BestScore;
            return true;
        }
        return false;
    }
}
[System.Serializable]
class Data
{
    public string s_BestPlayer;
    public int s_BestScore;
}




