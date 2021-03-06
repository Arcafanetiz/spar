using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject AboutPanel;
    public GameObject OptionUI;

    public void ReturnToMainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Tutorial()
    {
        GameManage.currentGameStatus = GameManage.GameStatus.TUTORIAL_PAUSE;
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

    public void ShowOption()
    {
        OptionUI.SetActive(true);
    }
    public void CloseOption()
    {
        OptionUI.SetActive(false);
    }

    public void GoToGameScene()
    {
        GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
        SceneManager.LoadScene(2);
    }

    public void GoToEndCredit()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
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
