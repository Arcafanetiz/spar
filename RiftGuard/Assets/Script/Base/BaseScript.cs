using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    [Header("Money Setting")]
        [SerializeField] private int _money;

    [Header("Health Setting")]
        [SerializeField] private int _health;

    [Header("Mana Setting")]
        [SerializeField] private float _maxMana;
        [SerializeField] private float _manaRegen;
        private float currentMana;

    [HideInInspector] public int health;
    [HideInInspector] public int money;
    [HideInInspector] public float manaRegen;
    [HideInInspector] public float maxMana;
    public float mana => currentMana;

    private void Awake()
    {
        health = _health;
        money = _money;
        manaRegen = _manaRegen;
        maxMana = _maxMana;
    }

    private void Update()
    {
        // Check If GAMEOVER no need to update
        if (GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER &&
            GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.SHOP)
        {
            // Increase Mana
            CheckMana();

            // For Debuging
            if (Input.GetKey(KeyCode.M))
            {
                money += 10;
            }
            if (Input.GetKey(KeyCode.N))
            {
                money -= 10;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy collide with base
        if(collision.gameObject.CompareTag("Enemy"))
        {
            // Health decrease and Check health
            health--;
            CheckHealth();
            
            // Decrease number(for checking) of enemy in each wave
            collision.GetComponent<EnemyControl>().GetRefWaveControl().GetComponent<WaveControl>().DecreaseEnemy();
            // Detroy enemy
            Destroy(collision.gameObject);
        }
    }


    // Money
    public void AddMoney(int val)
    {
        money += val;
    }

    // Health
    public void AddHealth(int val)
    {
        health += val;
    }

    public void CheckHealth()
    {
        // if Health = 0 -> Change gameState = GAMEOVER
        if (health <= 0)
        {
            GameManage.currentGameStatus = GameManage.GameStatus.GAMEOVER;
        }
    }

    // Mana
    private void CheckMana()
    {
        // Mana increase every time until it reach max
        if (currentMana < maxMana)
        {
            currentMana += manaRegen * Time.deltaTime;
        }
        else if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void AddMana(int _mana)
    {
        currentMana += _mana;
    }

}
