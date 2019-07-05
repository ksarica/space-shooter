using KS.Common.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCollectedText : MonoBehaviour
{
    Text goldCollectedText;

    // Start is called before the first frame update
    void Start()
    {
        goldCollectedText = this.gameObject.GetComponent<Text>();
        goldCollectedText.text = GameUIController.Instance.goldEarned.ToString();
        EventHandler.instance.Subscribe(GameEventType.OnGoldScoreChanged, GoldScoreChanged);
    }

    private void OnDestroy()
    {
        EventHandler.instance.Unsubscribe(GameEventType.OnGoldScoreChanged, GoldScoreChanged);
    }

    public void GoldScoreChanged(string[] values)
    {
        goldCollectedText.text = values[0].ToString();
    }

}
