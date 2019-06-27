using KS.Common.GameEvents;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreText : MonoBehaviour
{
    Text currentScoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentScoreText = this.gameObject.GetComponent<Text>();
        currentScoreText.text = GameUIController.Instance.currentScore.ToString();
        EventHandler.instance.Subscribe(GameEventType.OnScoreChanged, OnCurrentScoreChanged);
    }

    void OnDestroy()
    {
        EventHandler.instance.Unsubscribe(GameEventType.OnScoreChanged, OnCurrentScoreChanged);
    }

    public void OnCurrentScoreChanged(string[] values)
    {
        currentScoreText.text = values[0];
    }
}
