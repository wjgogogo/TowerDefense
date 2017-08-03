using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject endPanel;
    public Text endText;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void GameFailed()
    {
        SendMessage("StopSpawn");
        endPanel.SetActive(true);
        endText.text = "You Lose";
        Time.timeScale = 0;
    }

    public void GameWin()
    {
        endPanel.SetActive(true);
        endText.text = "You Win";
        Time.timeScale = 0;
    }

    public void OnRestartBtnDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuBtnDown()
    {
        SceneManager.LoadScene(0);
    }
}