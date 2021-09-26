using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    // Scipt about UI and Game Control

    [SerializeField] private Camera mainCamera;


    [Header("UI Setting")]
    // Create TowerUI
    [SerializeField] private Canvas createTowerCanvas;
    [SerializeField] private RectTransform createTowerImage;

    // Upgrade TowerUI
    [SerializeField] private Canvas upgradeTowerCanvas;
    [SerializeField] private RectTransform upgradeTowerImage;

    // PauseUI
    [SerializeField] private GameObject pauseUI;

    // GameOverUI
    [SerializeField] private GameObject gameoverUI;

    // GameStage
    /*
    PLAY : while playing game
    CREATE : when click to create tower
    UPGRADE : when click to upgrade tower
    PAUSE : when Press ESC to pause game
    GAMEOVER : when enemies go through tower over limit
    */
    public enum GameStatus
    {
        PLAY,
        CREATE,
        UPGRADE,
        PAUSE,
        GAMEOVER
    };

    public static GameStatus currentGameStatus = GameStatus.PLAY; // Track GameStage
    public static Vector3 clickPos; // Track position of tile which have been clicked

    private void Update()
    {
        // Check If GAMEOVER no need to update
        if (currentGameStatus != GameStatus.GAMEOVER)
        {
            CheckEscButton();
            CheckUI();
        }
        else
        {
            gameoverUI.SetActive(true);
        }
    }

    private void CheckUI()
    {
        if (currentGameStatus == GameStatus.PAUSE)
        {
            pauseUI.SetActive(true);
            createTowerCanvas.gameObject.SetActive(false);
            upgradeTowerCanvas.gameObject.SetActive(false);
        }
        else if (currentGameStatus == GameStatus.CREATE)
        {
            MoveCanvas(createTowerImage);
            createTowerCanvas.gameObject.SetActive(true);
        }
        else if (currentGameStatus == GameStatus.UPGRADE)
        {
            upgradeTowerCanvas.gameObject.SetActive(true);
        }
        else
        {
            pauseUI.SetActive(false);
            createTowerCanvas.gameObject.SetActive(false);
            upgradeTowerCanvas.gameObject.SetActive(false);
        }
    }

    private void CheckEscButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }
    
    public void CloseUI()
    {
        if (GameManage.currentGameStatus == GameManage.GameStatus.CREATE || GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE)
        {
            GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
        }
        else if (GameManage.currentGameStatus == GameManage.GameStatus.PAUSE)
        {
            GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
        }
        else
        {
            GameManage.currentGameStatus = GameManage.GameStatus.PAUSE;
        }
    }

    /* UI Script */
    private void MoveCanvas(RectTransform _Image)
    {
        // Show Canvas to the position that was click
        Vector3 viewPos = mainCamera.WorldToViewportPoint(clickPos);
        _Image.GetComponent<RectTransform>().anchorMin = viewPos;
        _Image.GetComponent<RectTransform>().anchorMax = viewPos;
    }

}
