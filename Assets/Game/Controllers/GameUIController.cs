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

    public int comboMultiplier { get; set; } = 1;

    [SerializeField] private int comboMultiplierMax;
    public int ComboMultiplierMax { get => ComboMultiplierMax; }

    [SerializeField] private float comboCountdown;
    public float ComboCountDown { get => comboCountdown; }

    public int currentScore { get; set; } = 0;
    public int bestScore { get; set; } = 0;

    public int totalGold { get; set; } = 0;
    public int goldEarned { get; set; } = 0;
    public int shotsFired { get; set; } = 0;
    public int shotsSuccessful { get; set; } = 0;


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
        EventHandler.instance.Subscribe(GameEventType.OnAsteroidDestroyed, IncrementComboMultiplier);
        //EventHandler.instance.Subscribe(GameEventType.OnResetComboMultiplierRequired, ResetComboMultiplier);
        EventHandler.instance.Subscribe(GameEventType.OnBulletShotChanged, BulletShotChanged);
        EventHandler.instance.Subscribe(GameEventType.OnGoldCollected, IncrementGold);
        EventHandler.instance.Subscribe(GameEventType.OnConvertScoreToGoldTime, UpdateTotalGold);
    }


    void OnDestroy()
    {
        EventHandler.instance.Unsubscribe(GameEventType.OnAsteroidDestroyed, IncrementScore);
        EventHandler.instance.Unsubscribe(GameEventType.OnAsteroidDestroyed, IncrementComboMultiplier);
        //EventHandler.instance.Unsubscribe(GameEventType.OnResetComboMultiplierRequired, ResetComboMultiplier);
        EventHandler.instance.Unsubscribe(GameEventType.OnBulletShotChanged, BulletShotChanged);
        EventHandler.instance.Unsubscribe(GameEventType.OnGoldCollected, IncrementGold);
        EventHandler.instance.Unsubscribe(GameEventType.OnConvertScoreToGoldTime, UpdateTotalGold);
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

        if (PlayerPrefs.HasKey("totalGold"))
        {
            totalGold = PlayerPrefs.GetInt("totalGold");
        }
        else
        {
            PlayerPrefs.SetInt("totalGold", totalGold);
        }
    }

    private void BulletShotChanged(string[] values)
    {
        shotsFired++;
        //Debug.Log("Ateş edilen mermi sayısı: " + shotsFired.ToString());
    }

    public void IncrementScore(string[] values)
    {
        //Debug.Log("Şuanki Skor: " + currentScore + " Kombo çarpanı: " + comboMultiplier);
        if (values[0] == "combo")
        {
            this.currentScore += (1 * comboMultiplier);
        }
        else
        {
            this.currentScore += int.Parse(values[0]);
        }
        EventHandler.instance.PublishGameEvent(GameEventType.OnScoreChanged, new string[] { currentScore.ToString() });
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("bestScore", bestScore);
            EventHandler.instance.PublishGameEvent(GameEventType.OnBestScoreChanged, new string[] { bestScore.ToString() });
        }
    }

    private void IncrementComboMultiplier(string[] values)
    {
        if (values[0] == "combo")
        {
            comboMultiplier += 1; // 3 ==> 4
            comboMultiplier = Mathf.Min(comboMultiplier, 3); // Min(4,3) ==> 3
        }
    }

    private void ResetComboMultiplier(string[] values)
    {
        comboMultiplier = 1;
    }

    private IEnumerator ResetComboTime(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        comboCountdown = 1.25f;
        yield return null;
    }

    public void IncrementGold(string[] values)
    {
        this.goldEarned += 1;
        this.totalGold += 1;
        EventHandler.instance.PublishGameEvent(GameEventType.OnGoldScoreChanged, new string[] { this.goldEarned.ToString(), this.totalGold.ToString() });
        PlayerPrefs.SetInt("totalGold", totalGold);
    }

    public void UpdateTotalGold(string[] values)
    {
        this.totalGold += Convert.ToInt32(values[0]);
    }

}
