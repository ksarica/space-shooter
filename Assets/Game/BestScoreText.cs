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
        // register to eventhandler for changes in bestScore 
        GameUIController.Instance.bestScoreChangeEvent += OnBestScoreChanged;

    }
    
    private void OnDestroy()
    {
        GameUIController.Instance.bestScoreChangeEvent -= OnBestScoreChanged;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnBestScoreChanged(int bestScore)
    {
        bestScoreText.text = bestScore.ToString();
    }
}
