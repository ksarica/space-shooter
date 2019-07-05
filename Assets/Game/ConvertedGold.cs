using KS.Common.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertedGold : MonoBehaviour
{
    Text convertedGoldText;

    // Start is called before the first frame update
    void Start()
    {
        convertedGoldText = this.gameObject.GetComponent<Text>();
        int convertedGoldAmount = (GameUIController.Instance.currentScore / 100);
        convertedGoldText.text = convertedGoldAmount.ToString();
        EventHandler.instance.PublishGameEvent(GameEventType.OnConvertScoreToGoldTime, new string[] { convertedGoldAmount.ToString() });
    }

}
