using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;
    private float health = 100;

    private void Awake(){
        SetUpSingleton();
    }

    private void SetUpSingleton(){
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberGameSessions > 1){
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore(){
        return score;
    }

    public float GetHealth(){
        return health;
    }

    public void AddToScore(int scoreValue){
        score += scoreValue;
    }

    public void UpdateHealth(float playerHealth){
        health = playerHealth;
    }

    public void ResetGame(){
        Destroy(gameObject);
    }
}
