using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance;

    public delegate void OnScoreChanged(int score);
    public event OnScoreChanged bestScoreChangeEvent;
    public event OnScoreChanged currentScoreChangeEvent;

    public int bestScore = 0;
    public int currentScore = 0;


    private void Awake() // singleton pattern
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // this GameUIController component should not be destroyed when scenes are changed it controlls the other scenes also. that's why we do it.
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScore = PlayerPrefs.GetInt("bestScore");
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", bestScore); 
        }

    }

    public void SetScore(int score)
    {
        this.currentScore = score;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("bestScore", bestScore);
            if (bestScoreChangeEvent != null && bestScoreChangeEvent.GetInvocationList().Length > 0)
            {
                bestScoreChangeEvent(bestScore);
            }

        }
        if (currentScoreChangeEvent != null && currentScoreChangeEvent.GetInvocationList().Length > 0)
        {
            currentScoreChangeEvent(currentScore);
        }
        
        // currentScoreChanged eVENTİ
    }

}
