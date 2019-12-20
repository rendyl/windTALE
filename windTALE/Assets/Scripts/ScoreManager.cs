using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    float timeGO = 0;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtGAMEOVER;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void upScore()
    {
        score++;
        txtScore.SetText("SCORE : " + score);
    }

    public void gameOver(float time)
    {
        timeGO = time;
    }

    public void gameOverZ()
    {
        txtGAMEOVER.gameObject.SetActive(true);
        txtScore.color = Color.black;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeGO > 0)
        {
            timeGO -= Time.deltaTime;
            if(timeGO <= 0)
            {
                gameOverZ();
            }
        }
    }
}
