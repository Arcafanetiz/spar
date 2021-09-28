using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTower : MonoBehaviour
{
    [System.Serializable] private class TowerData
    {
        public GameObject tower;
        public int cost;
        public int sell;
        public int mana;
    }
    [SerializeField] private BaseScript basePrefab;

    [SerializeField] private Text upgradeUI;
    [SerializeField] private Text sellUI;
    [SerializeField] private Text ATK_Text;
    [SerializeField] private Text Speed_Text;
    [SerializeField] private Text Range_Text;

    // Keep Tower Type (note : maybe can keep in form of array)
    [SerializeField] private TowerData[] Tower_1;
    [SerializeField] private TowerData[] Tower_2;
    [SerializeField] private TowerData[] Tower_3;

    private GameObject mapGenerator;

    private bool doOnce = true;

    private void Start()
    {
        mapGenerator = this.gameObject;
    }

    private void Update()
    {
        if(doOnce && GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE)
        {
            ShowCostSell();
            doOnce = false;
        }
        else if(GameManage.currentGameStatus == GameManage.GameStatus.PLAY)
        {
            doOnce = true;
        }
    }

    // Generate Tower When Clicked in UI
    public void GenTower_1()
    {
        if (basePrefab.money >= Tower_1[0].cost && basePrefab.mana >= Tower_1[0].mana)
        {
            // Initialize Postion X & Y
            int posX = (int)GameManage.clickPos.x;
            int posY = (int)-GameManage.clickPos.y;

            // Create Tower and Set MapCheck to true
            GameObject T1 = Instantiate(Tower_1[0].tower, GameManage.clickPos, Quaternion.identity);
            GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
            mapGenerator.GetComponent<MapGenerator>().SetMapCheck(posX, posY, true);

            // Set TileType in Tower script to Check for Special Buff
            GetComponent<MapGenerator>().SetTower(posX, posY, T1);
            T1.GetComponent<TowerControl>().compTile = GetComponent<MapGenerator>().GetMapData(posX, posY);
            T1.GetComponent<TowerControl>().SetTowerType(1,0);
            T1.GetComponent<TowerCardScript>().gameManager = this.gameObject;
            T1.GetComponent<TowerCardScript>().objectCardUI = this.GetComponent<GameManage>().objectCard;

            // Reduce Money & Mana
            basePrefab.AddMoney(-Tower_1[0].cost);
            basePrefab.AddMana(-Tower_1[0].mana);
        }
    }

    public void GenTower_2()
    {
        if (basePrefab.money >= Tower_2[0].cost && basePrefab.mana >= Tower_2[0].mana)
        {

            // Initialize Postion X & Y
            int posX = (int)GameManage.clickPos.x;
            int posY = (int)-GameManage.clickPos.y;

            // Create Tower and Set MapCheck to true
            GameObject T2 = Instantiate(Tower_2[0].tower, GameManage.clickPos, Quaternion.identity);
            GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
            mapGenerator.GetComponent<MapGenerator>().SetMapCheck(posX, posY, true);

            // Set TileType in Tower script to Check for Special Buff
            GetComponent<MapGenerator>().SetTower(posX, posY, T2);
            T2.GetComponent<TowerControl>().compTile = GetComponent<MapGenerator>().GetMapData(posX, posY);
            T2.GetComponent<TowerControl>().SetTowerType(2,0);
            T2.GetComponent<TowerCardScript>().gameManager = this.gameObject;
            T2.GetComponent<TowerCardScript>().objectCardUI = this.GetComponent<GameManage>().objectCard;

            // Reduce Money & Mana
            basePrefab.AddMoney(-Tower_2[0].cost);
            basePrefab.AddMana(-Tower_2[0].mana);
        }
    }

    public void GenTower_3()
    {
        if (basePrefab.money >= Tower_3[0].cost && basePrefab.mana >= Tower_3[0].mana)
        {

            // Initialize Postion X & Y
            int posX = (int)GameManage.clickPos.x;
            int posY = (int)-GameManage.clickPos.y;

            // Create Tower and Set MapCheck to true
            GameObject T3 = Instantiate(Tower_3[0].tower, GameManage.clickPos, Quaternion.identity);
            GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
            mapGenerator.GetComponent<MapGenerator>().SetMapCheck(posX, posY, true);

            // Set TileType in Tower script to Check for Special Buff
            GetComponent<MapGenerator>().SetTower(posX, posY, T3);
            T3.GetComponent<TowerControl>().compTile = GetComponent<MapGenerator>().GetMapData(posX, posY);
            T3.GetComponent<TowerControl>().SetTowerType(3,0);
            T3.GetComponent<TowerCardScript>().gameManager = this.gameObject;
            T3.GetComponent<TowerCardScript>().objectCardUI = this.GetComponent<GameManage>().objectCard;

            // Reduce Money & Mana
            basePrefab.AddMoney(-Tower_3[0].cost);
            basePrefab.AddMana(-Tower_3[0].mana);
        }
    }


    // Click for Sell & Upgrade
    public void Sell()
    {
        // Initialize Postion X & Y
        int _xPos = (int)GameManage.clickPos.x;
        int _yPos = (int)GameManage.clickPos.y;

        // Delete Tower and Set MapCheck to false
        mapGenerator.GetComponent<MapGenerator>().SetMapCheck(_xPos, -_yPos, false);

        GameObject SellTower = GetComponent<MapGenerator>().GetTowerData(_xPos, -_yPos);

        SellTower.GetComponent<TowerCardScript>().DelAllCard();

        int CheckType = SellTower.GetComponent<TowerControl>().info.Type;
        int CheckLevel = SellTower.GetComponent<TowerControl>().info.Level;
        
        if (CheckType == 1)
        {
            basePrefab.AddMoney(Tower_1[CheckLevel].sell);
        }
        else if(CheckType == 2)
        {
            basePrefab.AddMoney(Tower_2[CheckLevel].sell);
        }
        else if(CheckType == 3)
        {
            basePrefab.AddMoney(Tower_3[CheckLevel].sell);
        }

        Destroy(SellTower);
        GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
    }

    public void Upgrade()
    {
        // Initialize Postion X & Y
        int _xPos = (int)GameManage.clickPos.x;
        int _yPos = (int)GameManage.clickPos.y;

        GameObject SellTower = GetComponent<MapGenerator>().GetTowerData(_xPos, -_yPos);
        int CheckType = SellTower.GetComponent<TowerControl>().info.Type;
        int CheckLevel = SellTower.GetComponent<TowerControl>().info.Level;

        if (CheckType == 1 && basePrefab.money >= Tower_1[CheckLevel].cost)
        {
            UpgradeTower_1(CheckLevel+1);
        }
        else if (CheckType == 2 && basePrefab.money >= Tower_2[CheckLevel].cost)
        {
            UpgradeTower_2(CheckLevel+1);
        }
        else if (CheckType == 3 && basePrefab.money >= Tower_3[CheckLevel].cost)
        {
            UpgradeTower_3(CheckLevel+1);
        }

        GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
    }

    // Show Cost, Sell & Tower Info in UI
    private void ShowCostSell()
    {
        int _xPos = (int)GameManage.clickPos.x;
        int _yPos = (int)GameManage.clickPos.y;

        GameObject tempTower = GetComponent<MapGenerator>().GetTowerData(_xPos, -_yPos);

        tempTower.GetComponent<TowerCardScript>().SetActivate();

        TowerControl towerStat = tempTower.GetComponent<TowerControl>();

        int CheckType = tempTower.GetComponent<TowerControl>().info.Type;
        int CheckLevel = tempTower.GetComponent<TowerControl>().info.Level;

        if(CheckType == 1)
        {
            upgradeUI.text = Tower_1[CheckLevel].cost.ToString();
            sellUI.text = Tower_1[CheckLevel].sell.ToString();
        }
        else if (CheckType == 2)
        {
            upgradeUI.text = Tower_2[CheckLevel].cost.ToString();
            sellUI.text = Tower_2[CheckLevel].sell.ToString();
        }
        else if (CheckType == 3)
        {
            upgradeUI.text = Tower_3[CheckLevel].cost.ToString();
            sellUI.text = Tower_3[CheckLevel].sell.ToString();
        }

        ATK_Text.text = "ATK : " + ((int)towerStat.ATK).ToString();
        Speed_Text.text = "Speed : " + ((int)(1/towerStat.Cooldown)).ToString();
        Range_Text.text = "Range : " + ((int)towerStat.Range).ToString();
    }

    // Use In Script(Function Upgrade) (NOT FOR UI!!)
    private void UpgradeTower_1(int level)
    {
        print("GenTower1.1");
        // Initialize Postion X & Y
        int posX = (int)GameManage.clickPos.x;
        int posY = (int)-GameManage.clickPos.y;

        GameObject oldTower = GetComponent<MapGenerator>().GetTowerData(posX, posY);
        // Create Tower and Set MapCheck to true
        GameObject T1 = Instantiate(Tower_1[level].tower, GameManage.clickPos, Quaternion.identity);
        TowerCardScript TCS = T1.GetComponent<TowerCardScript>();

        // Set TileType in Tower script to Check for Special Buff
        GetComponent<MapGenerator>().SetTower(posX, posY, T1);
        T1.GetComponent<TowerControl>().compTile = GetComponent<MapGenerator>().GetMapData(posX, posY);
        T1.GetComponent<TowerControl>().SetTowerType(1, level);
        TCS.SetListCard(oldTower.GetComponent<TowerCardScript>().GetListCard());
        TCS.gameManager = this.gameObject;
        TCS.objectCardUI = this.GetComponent<GameManage>().objectCard;
        TCS.CardActivate();

        Destroy(oldTower.gameObject);
        // Reduce Money
        basePrefab.AddMoney(-Tower_1[level].cost);
    }

    private void UpgradeTower_2(int level)
    {
        print("GenTower2.2");

        // Initialize Postion X & Y
        int posX = (int)GameManage.clickPos.x;
        int posY = (int)-GameManage.clickPos.y;

        GameObject oldTower = GetComponent<MapGenerator>().GetTowerData(posX, posY);
        // Create Tower and Set MapCheck to true
        GameObject T2 = Instantiate(Tower_2[level].tower, GameManage.clickPos, Quaternion.identity);
        TowerCardScript TCS = T2.GetComponent<TowerCardScript>();

        // Set TileType in Tower script to Check for Special Buff
        GetComponent<MapGenerator>().SetTower(posX, posY, T2);
        T2.GetComponent<TowerControl>().compTile = GetComponent<MapGenerator>().GetMapData(posX, posY);
        T2.GetComponent<TowerControl>().SetTowerType(2, level);
        TCS.SetListCard(oldTower.GetComponent<TowerCardScript>().GetListCard());
        TCS.gameManager = this.gameObject;
        TCS.objectCardUI = this.GetComponent<GameManage>().objectCard;
        TCS.CardActivate();

        Destroy(oldTower.gameObject);
        // Reduce Money
        basePrefab.AddMoney(-Tower_2[level].cost);
    }

    private void UpgradeTower_3(int level)
    {
        print("GenTower3.3");

        // Initialize Postion X & Y
        int posX = (int)GameManage.clickPos.x;
        int posY = (int)-GameManage.clickPos.y;

        GameObject oldTower = GetComponent<MapGenerator>().GetTowerData(posX, posY);
        // Create Tower and Set MapCheck to true
        GameObject T3 = Instantiate(Tower_3[level].tower, GameManage.clickPos, Quaternion.identity);
        TowerCardScript TCS = T3.GetComponent<TowerCardScript>();

        // Set TileType in Tower script to Check for Special Buff
        GetComponent<MapGenerator>().SetTower(posX, posY, T3);
        T3.GetComponent<TowerControl>().compTile = GetComponent<MapGenerator>().GetMapData(posX, posY);
        T3.GetComponent<TowerControl>().SetTowerType(3, level);
        TCS.SetListCard(oldTower.GetComponent<TowerCardScript>().GetListCard());
        TCS.gameManager = this.gameObject;
        TCS.objectCardUI = this.GetComponent<GameManage>().objectCard;
        TCS.CardActivate();

        Destroy(oldTower.gameObject);
        // Reduce Money
        basePrefab.AddMoney(-Tower_3[level].cost);
    }

}
