using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenus : MonoBehaviour
{
    public GameObject AboutPanel;
    public void ReturnToMainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
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
    public void Quit()
    {
        Application.Quit();
    }
}
