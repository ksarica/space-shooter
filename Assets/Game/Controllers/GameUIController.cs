using KS.Common.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EventHandler = KS.Common.GameEvents.EventHandler;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance;

    public int bestScore = 0;
    public int currentScore = 0;

    public int shotsFired = 0;
    public int shotsSuccessful = 0;

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

    void Start()
    {
        EventHandler.instance.Subscribe(GameEventType.OnAsteroidDestroyed, IncrementScore);
        EventHandler.instance.Subscribe(GameEventType.OnBulletShotChanged, BulletShotChanged);

    }

    void OnDestroy()
    {
        EventHandler.instance.Unsubscribe(GameEventType.OnAsteroidDestroyed, IncrementScore);
        EventHandler.instance.Unsubscribe(GameEventType.OnBulletShotChanged, BulletShotChanged);

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
    }

    public void SetShotsFired(int value)
    {
        this.shotsFired = value;
    }

    public void SetShotsSuccessful(int value)
    {
        this.shotsSuccessful = value;
    }
    private void BulletShotChanged(string[] values)
    {
        shotsFired++;
    }
    public void IncrementScore(string[] values)
    {
        this.currentScore += 1;
        EventHandler.instance.PublishGameEvent(GameEventType.OnScoreChanged, new string[] { currentScore.ToString() });
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("bestScore", bestScore);
            EventHandler.instance.PublishGameEvent(GameEventType.OnBestScoreChanged, new string[] { bestScore.ToString() });
        }
    }

}
