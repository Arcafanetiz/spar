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

        // OtherUI which run while playing
        [SerializeField] private Text moneyUI;
        [SerializeField] private Text healthUI;


    [Header("Money Setting")]
        [SerializeField] private int InitialMoney;

    [Header("Health Setting")]
        [SerializeField] private int health;

    private int money;

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


    private void Start()
    {
        money = InitialMoney;
    }

    private void Update()
    {
        // Check If GAMEOVER no need to update
        if (currentGameStatus != GameStatus.GAMEOVER)
        {
            CheckEscButton();
            CheckUI();
            
            ShowMoney();

            ShowHealth();
            CheckHealth();

            if (Input.GetKey(KeyCode.M))
            {
                money += 10;
            }
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
            MoveCanvas(upgradeTowerImage);
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
    // Money
    private void ShowMoney()
    {
        moneyUI.text = "$ " + money.ToString();
    }

    public void AddMoney(int val)
    {
        money += val;
    }

    public int GetMoney()
    {
        return money;
    }

    // Health
    private void ShowHealth()
    {
        healthUI.text = "Health : " + health.ToString();
    }

    public void AddHealth(int val)
    {
        health += val;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            currentGameStatus = GameStatus.GAMEOVER;
        }
    }

    private void MoveCanvas(RectTransform _Image)
    {
        // Show Canvas to the position that was click
        Vector3 viewPos = mainCamera.WorldToViewportPoint(clickPos);
        _Image.GetComponent<RectTransform>().anchorMin = viewPos;
        _Image.GetComponent<RectTransform>().anchorMax = viewPos;
    }

}
