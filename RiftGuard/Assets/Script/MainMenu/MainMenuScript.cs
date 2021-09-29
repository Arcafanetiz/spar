using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject AboutPanel;
    public void ReturnToMainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
        SceneManager.LoadScene(1);
    }
    public void ShowAbout()
    {
        AboutPanel.SetActive(true);
    }
    public void CloseAbout()
    {
        AboutPanel.SetActive(false);
    }
    public void ShowSetting()
    {
        //Credit.SetActive(true);
    }
    public void CloseSetting()
    {
        //Credit.SetActive(false);
    }

    public void Resume()
    {
        GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
    }

    public void Quit()
    {
        Application.Quit();
    }
}