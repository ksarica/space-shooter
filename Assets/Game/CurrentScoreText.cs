using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        // register to eventhandler for changes in bestScore 
        GameUIController.Instance.currentScoreChangeEvent += OnCurrentScoreChanged;

    }

    private void OnDestroy()
    {
        GameUIController.Instance.currentScoreChangeEvent -= OnCurrentScoreChanged;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnCurrentScoreChanged(int currentScore)
    {
        currentScoreText.text = currentScore.ToString();
    }
}
