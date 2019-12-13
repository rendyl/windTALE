using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtGAMEOVER;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void upScore()
    {
        score++;
        txtScore.SetText("Score : " + score);
    }

    public void gameOver()
    {
        txtGAMEOVER.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
