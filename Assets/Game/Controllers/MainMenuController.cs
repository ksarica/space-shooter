using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CoreGame");
        GameUIController.Instance.currentScore = 0;
        GameUIController.Instance.shotsFired = 0;
        GameUIController.Instance.goldEarned = 0;
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
