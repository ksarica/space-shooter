using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsFiredText : MonoBehaviour
{
    Text shotsFiredText; // STEP 8
    Text shotsSuccessfulText; // STEP 8

    // Start is called before the first frame update
    void Start()
    {
        shotsFiredText = this.gameObject.GetComponent<Text>(); // STEP 8
        shotsSuccessfulText = this.gameObject.GetComponent<Text>(); // STEP 8
        EndGameController.Instance.bulletShotEvent += OnShotsFiredChanged; // STEP 8
    }

    private void OnShotsFiredChanged(int score)
    {
        shotsFiredText.text = score.ToString(); // STEP 9
    }

    private void OnShotsSuccessfulChanged(int score)
    {
        //shotsSuccessfulText.text = score.ToString();
    }
    
}
