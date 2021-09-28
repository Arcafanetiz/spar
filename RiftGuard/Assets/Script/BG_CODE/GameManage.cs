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

    [SerializeField] private GameObject _objectCard;
    public GameObject objectCard => _objectCard;

    // Upgrade TowerUI
    [SerializeField] private GameObject UpgradeUI;

    // Tower UI
    [SerializeField] private GameObject towerUI;

    // Spawner UI
    [SerializeField] private GameObject spawnerUI;

    // PauseUI
    [SerializeField] private GameObject pauseUI;

    // GameOverUI
    [SerializeField] private GameObject gameoverUI;

    // GameStage
    /*
    PLAY : while playing game
    CREATE : when click to create tower
    UPGRADE : when click to upgrade tower
    BASE : when click at base -> Card UI appear
    SPAWNER : when click at spawner -> Card UI appear
    PAUSE : when Press ESC to pause game
    GAMEOVER : when enemies go through tower over limit
    */
    public enum GameStatus
    {
        PLAY,
        CREATE,
        UPGRADE,
        BASE,
        SPAWNER,
        PAUSE,
        GAMEOVER,
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
            UpgradeUI.SetActive(false);
        }
        else if (currentGameStatus == GameStatus.CREATE)
        {
            MoveCanvas(createTowerImage);
            createTowerCanvas.gameObject.SetActive(true);
        }
        else if (currentGameStatus == GameStatus.UPGRADE)
        {
            UpgradeUI.SetActive(true);
            _objectCard.SetActive(true);
        }
        else if(currentGameStatus == GameStatus.BASE)
        {
            towerUI.SetActive(true);
            _objectCard.SetActive(true);
        }
        else if (currentGameStatus == GameStatus.SPAWNER)
        {
            spawnerUI.SetActive(true);
            _objectCard.SetActive(true);
        }
        else
        {
            createTowerCanvas.gameObject.SetActive(false);
            _objectCard.SetActive(false);
            UpgradeUI.SetActive(false);
            towerUI.SetActive(false);
            spawnerUI.SetActive(false);
            pauseUI.SetActive(false);
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
        if (GameManage.currentGameStatus == GameManage.GameStatus.CREATE  || 
            GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE ||
            GameManage.currentGameStatus == GameManage.GameStatus.BASE    ||
            GameManage.currentGameStatus == GameManage.GameStatus.SPAWNER)
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