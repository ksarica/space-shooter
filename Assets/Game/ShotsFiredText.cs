using System;
using UnityEngine;
using UnityEngine.UI;
using KS.Common.GameEvents;
using EventHandler = KS.Common.GameEvents.EventHandler;

public class ShotsFiredText : MonoBehaviour
{
    Text shotsFiredText; // STEP 8

    void Start()
    {
        shotsFiredText = this.gameObject.GetComponent<Text>(); // STEP 8
        shotsFiredText.text = GameUIController.Instance.shotsFired.ToString();
    }

    
}
