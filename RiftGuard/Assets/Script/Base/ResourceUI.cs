using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [Header("UI Setting")]
        [SerializeField] private Text moneyUI;
        [SerializeField] private Text healthUI;
        [SerializeField] private Text manaUI;


    void Update()
    {
        if (GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER &&
            GameManage.currentGameStatus != GameManage.GameStatus.PAUSE)
        {
            ShowMoney();
            ShowHealth();
            ShowMana();
        }
    }

    public void ShowMoney()
    {
        moneyUI.text = "$ " + this.GetComponent<BaseScript>().money.ToString();
    }

    public void ShowHealth()
    {
        healthUI.text = this.GetComponent<BaseScript>().health.ToString();
    }

    public void ShowMana()
    {
        int _currentMana = (int)this.GetComponent<BaseScript>().mana;
        int _maxMana = (int)this.GetComponent<BaseScript>().maxMana;
        manaUI.text = "MANA : " + _currentMana.ToString() + " / " + _maxMana.ToString("0");
    }
}
