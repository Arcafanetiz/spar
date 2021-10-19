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
    [SerializeField] private GameObject DeckUIManage;

    [SerializeField] private GameObject DeckUI;

    [SerializeField] private GameObject _objectCard;
    public GameObject objectCard => _objectCard;

    // Upgrade TowerUI
    [SerializeField] private GameObject UpgradeUI;

    // Tower UI
    [SerializeField] private GameObject baseUI;

    // Spawner UI
    [SerializeField] private GameObject spawnerUI;

    // Shop UI
    [SerializeField] private GameObject shopUI;

    // PauseUI
    [SerializeField] private GameObject pauseUI;

    // GameOverUI
    [SerializeField] private GameObject gameoverUI;

    // Clicking for closeing UI in Tower,Base,SpawnerUI
    [SerializeField] private GameObject closeUI;

    [Header("UI Reference")]

    [SerializeField] private GameObject OptionUI;


    // GameStage
    /*
    PLAY : while playing game
    CREATE : when click to create tower
    UPGRADE : when click to upgrade tower
    BASE : when click at base -> Card UI appear
    SPAWNER : when click at spawner -> Card UI appear
    SHOP : Every n wave shop UI will appear
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
        SHOP,
        PAUSE,
        GAMEOVER,
    };

    public static GameStatus currentGameStatus = GameStatus.PLAY; // Track GameStage
    public static Vector3 clickPos; // Track position of tile which have been clicked

    private bool doOnce = true;

    private void Start()
    {
        objectCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-objectCard.GetComponent<RectTransform>().rect.width / 2, -objectCard.GetComponent<RectTransform>().rect.height + 10);
        UpgradeUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-UpgradeUI.GetComponent<RectTransform>().rect.width / 2, UpgradeUI.GetComponent<RectTransform>().rect.height / 2);
        spawnerUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-spawnerUI.GetComponent<RectTransform>().rect.width / 2, spawnerUI.GetComponent<RectTransform>().rect.height / 2);
        baseUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(-baseUI.GetComponent<RectTransform>().rect.width / 2, baseUI.GetComponent<RectTransform>().rect.height / 2);
    }

    private void Update()
    {
        // Check If GAMEOVER no need to update
        if (currentGameStatus != GameStatus.GAMEOVER)
        {
            CheckEscButton();

            if (Input.GetKeyDown(KeyCode.C))
            {
                DeckUI.SetActive((DeckUI.activeSelf) ? false : true);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                currentGameStatus = GameStatus.SHOP;
            }

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
            closeUI.SetActive(true);
        }
        else if(currentGameStatus == GameStatus.BASE)
        {
            baseUI.SetActive(true);
            _objectCard.SetActive(true);
            closeUI.SetActive(true);
        }
        else if (currentGameStatus == GameStatus.SPAWNER)
        {
            spawnerUI.SetActive(true);
            _objectCard.SetActive(true);
            closeUI.SetActive(true);
        }
        else if (currentGameStatus == GameStatus.SHOP)
        {
            shopUI.SetActive(true);
            DeckUI.SetActive(true);
            if(DeckUIManage.GetComponent<CardCollectorUIManage>().isHide && doOnce)
            {
                DeckUIManage.GetComponent<CardCollectorUIManage>().CloseOpenUI();
                doOnce = false;
            }
            createTowerCanvas.gameObject.SetActive(false);
        }
        else
        {
            // Set all UI (except ResoureUI) to be false
            createTowerCanvas.gameObject.SetActive(false);
            closeUI.SetActive(false);
            shopUI.SetActive(false);
            pauseUI.SetActive(false);
            doOnce = true;
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
            if(OptionUI.activeSelf)
            {
                OptionUI.SetActive(false);
            }
            else
            {
                GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
            }
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
