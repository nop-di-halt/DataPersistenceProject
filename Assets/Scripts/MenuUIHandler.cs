using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private Text welcomeText;
    [SerializeField] private Text nameInput;
    // Start is called before the first frame update
    void Start()
    {
        welcomeText.text = "Welcome, " + GameManager.Instatnce.PlayerName + "!";
    }

    public void SetPlayerName()
    {
        welcomeText.text = "Welcome, " + nameInput.text + "!";
        GameManager.Instatnce.SetPlayerName(nameInput.text);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        GameManager.Instatnce.SaveHighScores();
    }
    public void LoadSettingsScreen()
    {
        SceneManager.LoadScene("SettingsScreen");
    }
}
