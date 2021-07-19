using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Toggle[] ballColors;
    [SerializeField] private Toggle[] paddleColors;
    [SerializeField] private GameObject paddle;
    [SerializeField] private GameObject ball;

    private void Start()
    {
        Color ballColor = GameManager.Instatnce.ballColor;
        Color paddleColor = GameManager.Instatnce.paddleColor;
        ball.GetComponent<MeshRenderer>().material.color = ballColor;
        paddle.GetComponent<MeshRenderer>().material.color = paddleColor;
        InitializeValues(ballColor, ballColors);
        InitializeValues(paddleColor, paddleColors);
    }
    public void SetColor(string obj)
    {
        Toggle[] toggles;
        GameObject gameObject;
        Color color = Color.clear;
        if (obj == "ball")
        {
            toggles = ballColors;
            gameObject = ball;
        }
        else
        {
            toggles = paddleColors;
            gameObject = paddle;
        }
        foreach (var toggle in toggles)
        {
            if (toggle.isOn)
            {
                string colorSelected = toggle.gameObject.GetComponentInChildren<Text>().text;
                switch (colorSelected)
                {
                    case "Yellow": color = Color.yellow; break;
                    case "Red": color = Color.red; break;
                    case "Green": color = Color.green; break;
                    case "Blue": color = Color.blue; break;
                    case "Magenta": color = Color.magenta; break;
                }
                break;
            }
        }
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }

    //Set toggles in compliance with loaded settings from disk
    private void InitializeValues(Color color, Toggle[] toggles)
    {
        if (color == Color.blue)
        {
            toggleOn("Blue", toggles);
        }
        else if (color == Color.red)
        {
            toggleOn("Red", toggles);
        }
        else if (color == Color.green)
        {
            toggleOn("Green", toggles);
        }
        else if (color == Color.yellow)
        {
            toggleOn("Yellow", toggles);
        }
        else if (color == Color.magenta)
        {
            toggleOn("Magenta", toggles);
        }
        void toggleOn(string colorName, Toggle[] _toggles)
        {
            foreach (var item in _toggles)
            {
                if (colorName == item.gameObject.GetComponentInChildren<Text>().text)
                {
                    item.isOn = true;
                    break;
                }
            }
        }
    }
    public void SavePrefernces()
    {
        var saveData = new GameSettings();

        saveData.ballColor = ball.GetComponent<MeshRenderer>().material.color;
        saveData.paddleColor = paddle.GetComponent<MeshRenderer>().material.color;

        string path = Application.persistentDataPath + "/settings.json";
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);

        GameManager.Instatnce.ballColor = ball.GetComponent<MeshRenderer>().material.color;
        GameManager.Instatnce.paddleColor = paddle.GetComponent<MeshRenderer>().material.color;
    }
    public void ReturnToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
