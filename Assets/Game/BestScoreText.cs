using KS.Common.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreText : MonoBehaviour
{
    Text bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        bestScoreText = this.gameObject.GetComponent<Text>();
        bestScoreText.text = GameUIController.Instance.bestScore.ToString();
        EventHandler.instance.Subscribe(GameEventType.OnBestScoreChanged, BestScoreChanged);
    }

    private void OnDestroy()
    {
        EventHandler.instance.Unsubscribe(GameEventType.OnBestScoreChanged, BestScoreChanged);
    }

    public void BestScoreChanged(string[] values)
    {
        bestScoreText.text = values[0].ToString();
    }
}
