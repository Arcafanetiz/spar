using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    [Header("UI Setting")]
        [SerializeField] private Text moneyUI;
        [SerializeField] private Text healthUI;
        [SerializeField] private Text manaUI;

    [Header("Money Setting")]
        [SerializeField] private int initialMoney;
        private int money;

    [Header("Health Setting")]
        [SerializeField] private int health;

    [Header("Mana Setting")]
        [SerializeField] private float maxMana;
        [SerializeField] private float manaRegen;
        private float currentMana;

    private void Start()
    {
        money = initialMoney;
        UpdateMoney();
        UpdateHealth();
        UpdateMana();
    }

    private void Update()
    {
        // Check If GAMEOVER no need to update
        if (GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER &&
            GameManage.currentGameStatus != GameManage.GameStatus.PAUSE)
        {
            UpdateMana();
            CheckMana();

            if (Input.GetKey(KeyCode.M))
            {
                money += 10;
                UpdateMoney();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            CheckHealth();
            UpdateHealth();

            collision.GetComponent<EnemyControl>().GetRefWaveControl().GetComponent<WaveControl>().DecreaseEnemy();
            Destroy(collision.gameObject);
        }
    }


    // Money
    public void UpdateMoney()
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
    public void UpdateHealth()
    {
        healthUI.text = health.ToString();
    }

    public void AddHealth(int val)
    {
        health += val;
    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            GameManage.currentGameStatus = GameManage.GameStatus.GAMEOVER;
        }
    }

    // Mana
    public void UpdateMana()
    {
        int _currentMana = (int)currentMana;
        int _maxMana = (int)maxMana;
        manaUI.text = "MANA : " + _currentMana.ToString() + " / " + _maxMana.ToString("0");
    }

    private void CheckMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegen * Time.deltaTime;
        }
        else if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public int GetMana()
    {
        return (int)currentMana;
    }

    public void AddMana(int _mana)
    {
        currentMana += _mana;
    }

}
