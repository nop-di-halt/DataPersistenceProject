using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoresController : MonoBehaviour
{
    private List<HighScoreEntry> scores;
    private bool loadStartScreen;
    [SerializeField] private float delayTime = 3.0f;
    private IEnumerator delayCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        scores = GameManager.Instatnce.scores;
        SetTableEntries();
        delayCoroutine = Delay(delayTime);
        StartCoroutine(delayCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(delayCoroutine);
            loadStartScreen = true;
        }
        if (loadStartScreen)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }

    // Fill high scores table with data
    private void SetTableEntries()
    {
        int entryNumber = 1;
        foreach (var item in scores.OrderByDescending(e => e.s_Score))
        {
            var entry = GameObject.Find(entryNumber.ToString());
            var row = entry.GetComponentsInChildren<Text>();
            row[1].text = item.s_Name;
            row[2].text = item.s_Score.ToString();
            entryNumber++;
        }
    }

    // Show high scores table for specified time then load start screen
    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        loadStartScreen = true;
    }
}


