using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    private static EndGameController instance;
    public static EndGameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<EndGameController>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    //public static EndGameController Instance = null;

    public delegate void OnStatsChanged(int score); // STEP 6
    public event OnStatsChanged bulletShotEvent; // STEP 6
    public event OnStatsChanged bulletSuccessfulEvent; // STEP 6

    public int shotsFired = 0;
    public int shotsSuccessful = 0;

    //private void Awake() // singleton pattern
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(this.gameObject); // this GameUIController component should not be destroyed when scenes are changed it controlls the other scenes also. that's why we do it.
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    public void SetShotsFired(int value)
    {
        this.shotsFired = value;
        if (bulletShotEvent != null && bulletShotEvent.GetInvocationList().Length > 0)
        {
            bulletShotEvent(shotsFired); // STEP 7
        }
    }

    public void SetShotsSuccessful(int value)
    {
        this.shotsSuccessful = value;
        if (bulletShotEvent != null && bulletShotEvent.GetInvocationList().Length > 0)
        {
            bulletShotEvent(shotsSuccessful);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
