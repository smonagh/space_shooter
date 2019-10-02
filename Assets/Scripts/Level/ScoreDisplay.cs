using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    public GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        string newScore = gameSession.GetScore().ToString();
        string writeScore = string.Format("Score: {0}", newScore);
        scoreText.text = writeScore;
    }
}
