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
            GameManage.currentGameStatus != GameManage.GameStatus.PAUSE)
        {
            CheckMana();

            if (Input.GetKey(KeyCode.M))
            {
                money += 10;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            CheckHealth();

            collision.GetComponent<EnemyControl>().GetRefWaveControl().GetComponent<WaveControl>().DecreaseEnemy();
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
        if (health <= 0)
        {
            GameManage.currentGameStatus = GameManage.GameStatus.GAMEOVER;
        }
    }

    // Mana
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

    public void AddMana(int _mana)
    {
        currentMana += _mana;
    }

}
