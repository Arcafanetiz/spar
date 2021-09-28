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
        [SerializeField] private float manaRegen;
        private float currentMana;

    public int health => _health;
    public int money => _money;
    public float mana => currentMana;
    public float maxMana => _maxMana;

    private void Update()
    {
        // Check If GAMEOVER no need to update
        if (GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER &&
            GameManage.currentGameStatus != GameManage.GameStatus.PAUSE)
        {
            CheckMana();

            if (Input.GetKey(KeyCode.M))
            {
                _money += 10;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            _health--;
            CheckHealth();

            collision.GetComponent<EnemyControl>().GetRefWaveControl().GetComponent<WaveControl>().DecreaseEnemy();
            Destroy(collision.gameObject);
        }
    }


    // Money
    public void AddMoney(int val)
    {
        _money += val;
    }

    // Health
    public void AddHealth(int val)
    {
        _health += val;
    }

    public void CheckHealth()
    {
        if (_health <= 0)
        {
            GameManage.currentGameStatus = GameManage.GameStatus.GAMEOVER;
        }
    }

    // Mana
    private void CheckMana()
    {
        if (currentMana < _maxMana)
        {
            currentMana += manaRegen * Time.deltaTime;
        }
        else if (currentMana > _maxMana)
        {
            currentMana = _maxMana;
        }
    }

    public void AddMana(int _mana)
    {
        currentMana += _mana;
    }

}
