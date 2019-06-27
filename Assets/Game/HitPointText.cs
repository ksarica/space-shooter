using KS.Common.GameEvents;
using UnityEngine;
using UnityEngine.UI;

public class HitPointText : MonoBehaviour
{
    Text hitPointText;

    void Start()
    {
        hitPointText = this.gameObject.GetComponent<Text>();
        EventHandler.instance.Subscribe(GameEventType.OnPlayerHealthChanged, OnHitPointChanged);
    }

    void OnDestroy()
    {
        EventHandler.instance.Unsubscribe(GameEventType.OnPlayerHealthChanged, OnHitPointChanged);
    }

    private void OnHitPointChanged(string[] values)
    {
        hitPointText.text = values[0];
    }
}
