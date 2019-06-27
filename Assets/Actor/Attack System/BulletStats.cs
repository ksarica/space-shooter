using KS.Common;
using KS.Common.GameEvents;
using UnityEngine;
using UnityEngine.UI;

public class BulletStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //EventHandler.instance.OnShoot += OnBulletShot; // STEP 4
        //EventHandler.instance.OnHit += OnSuccessfulShot; // STEP 4
        //EventHandler.instance.Subscribe(GameEventType.OnBulletShotChanged, OnBulletShot);
    }

    private void OnDestroy()
    {
        //EventHandler.instance.OnShoot -= OnBulletShot; // STEP 4
        //EventHandler.instance.OnHit -= OnSuccessfulShot; // STEP 4
        //EventHandler.instance.Unsubscribe(GameEventType.OnBulletShotChanged, OnBulletShot);
    }

    private void OnBulletShot(string[] values) // STEP 5
    {
        GameUIController.Instance.SetShotsFired(GameUIController.Instance.shotsFired + 1);
    }

    private void OnSuccessfulShot()
    {
        //EndGameController.Instance.SetShotsSuccessful(EndGameController.Instance.shotsSuccessful + 1);
    }
}
