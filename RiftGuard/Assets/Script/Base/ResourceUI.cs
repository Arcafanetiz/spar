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
        ShowMoney();
        ShowHealth();
        ShowMana();
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
        float  _currentMana = this.GetComponent<BaseScript>().mana;
        float _maxMana = this.GetComponent<BaseScript>().maxMana;
        manaUI.text = "MANA : " + _currentMana.ToString("F1") + " / " + _maxMana.ToString("F1");
    }
}
