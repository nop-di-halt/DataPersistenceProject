using System.Collections;
using System.Collections.Generic;
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
        bestPlayer = PlayerName;
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
}
