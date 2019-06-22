using KS.Actor.Attack;
using KS.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.instance.OnShoot += OnBulletHit; // STEP 4
        //EventHandler.instance.OnHit += OnSuccessfulShot; // STEP 4
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        EventHandler.instance.OnShoot -= OnBulletHit; // STEP 4
        //EventHandler.instance.OnHit -= OnSuccessfulShot; // STEP 4
    }

    private void OnBulletHit() // STEP 5
    {
        EndGameController.Instance.SetShotsFired(EndGameController.Instance.shotsFired + 1);
    }

    private void OnSuccessfulShot()
    {
        //EndGameController.Instance.SetShotsSuccessful(EndGameController.Instance.shotsSuccessful + 1);
    }
}
