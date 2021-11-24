using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    [Header("Money Setting")]
        [SerializeField] private float _money;

    [Header("Health Setting")]
        [SerializeField] private int _health;

    [Header("Mana Setting")]
        [SerializeField] private float _maxMana;
        public float RefmanaRegen;
        private float currentMana;

    [HideInInspector] public int health;
    [HideInInspector] public float money;
    [HideInInspector] public float manaRegen;
    [HideInInspector] public float maxMana;
    [HideInInspector] public float moneyRegen = 0; // using with card -> used to slightly increase money
    [HideInInspector] public float enemyKillRate = 0; // using with card -> get more money when killing enemy
    [HideInInspector] public float manaUsage = 1.0f; // Use as reference -> effect with card that will decrease mana usage

    public float mana => currentMana;

    private void Awake()
    {
        health = _health;
        money = _money;
        manaRegen = RefmanaRegen;
        maxMana = _maxMana;
    }

    private void Update()
    {
        // Check If GAMEOVER no need to update
        if (GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER &&
            GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.SHOP &&
            GameManage.currentGameStatus != GameManage.GameStatus.TUTORIAL_PAUSE)
        {
            // Increase Mana
            CheckMana();

            // Check if have card money regen -> regen money every frame
            if(moneyRegen != 0)
            {
                money += (moneyRegen * Time.deltaTime);
            }

            // For Debuging
            if (Input.GetKey(KeyCode.M))
            {
                money += 10;
            }
            if (Input.GetKey(KeyCode.N) && currentMana < 10.0f)
            {
                currentMana += 0.2f;
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
            health = 0;
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

    public void AddMana(float _mana)
    {
        currentMana += _mana;
    }

}
