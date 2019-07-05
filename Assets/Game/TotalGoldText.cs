using KS.Common.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalGoldText : MonoBehaviour
{
    Text totalGoldText;

    // Start is called before the first frame update
    void Start()
    {
        totalGoldText = this.gameObject.GetComponent<Text>();
        totalGoldText.text = GameUIController.Instance.totalGold.ToString();
        EventHandler.instance.Subscribe(GameEventType.OnGoldScoreChanged, GoldScoreChanged);
    }

    private void OnDestroy()
    {
        EventHandler.instance.Unsubscribe(GameEventType.OnGoldScoreChanged, GoldScoreChanged);
    }

    public void GoldScoreChanged(string[] values)
    {
        totalGoldText.text = values[1].ToString();
    }

}
