using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private GameObject Sprite;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image armorBar;
    [Header("Enemy Properties")]
        public float SPD;
        public float DEF;
        public float HP;
        [SerializeField] private float Armor;
        [SerializeField] private int getMoney;

    private Vector2[] path;
    private Vector3 pathOffset;
    private GameObject refWaveControl;
    private GameObject baseScript;
    [HideInInspector] public float currentHP;
    [HideInInspector] public float currentArmor;

    private int now = 0;

    private void Start()
    {
        currentHP = HP;
        currentArmor = Armor;

        if (Armor == 0)
        {
            armorBar.enabled = false;
        }
    }

    private void Update()
    {
        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            // Walk until there have no path
            if(now<path.Length-1)
            {
                Walk();
            }
            CheckHealth();
        }
    }

    private void Walk()
    {
        // Make Enemy walk from grid to grid
        Vector2 dif = path[now + 1] - path[now];
        if((int)dif.x > 0)
        {
            Sprite.transform.rotation = Quaternion.Euler(0, 0, -90.0f);
            transform.Translate(Vector3.right * SPD * Time.deltaTime);
            if (transform.position.x >= path[now + 1].x + pathOffset.x)
            {
                now++;
            }
        }
        else if((int)dif.x < 0)
        {
            Sprite.transform.rotation = Quaternion.Euler(0, 0, 90.0f);
            transform.Translate(Vector3.left * SPD * Time.deltaTime);
            if (transform.position.x <= path[now + 1].x + pathOffset.x)
            {
                now++;
            }
        }
        else if ((int)dif.y > 0)
        {
            Sprite.transform.rotation = Quaternion.Euler(0,0,0.0f);
            transform.Translate(Vector3.up * SPD * Time.deltaTime);
            if (transform.position.y >= path[now + 1].y + pathOffset.y)
            {
                now++;
            }
        }
        else if ((int)dif.y < 0)
        {
            Sprite.transform.rotation = Quaternion.Euler(0, 0, 180.0f);
            transform.Translate(Vector3.down * SPD * Time.deltaTime);
            if (transform.position.y <= path[now + 1].y + pathOffset.y)
            {
                now++;
            }
        }

    }

    private void CheckHealth()
    {
        if(currentHP <= 0 )
        {
            float rate = baseScript.GetComponent<BaseScript>().enemyKillRate;
            refWaveControl.GetComponent<WaveControl>().DecreaseEnemy();
            baseScript.GetComponent<BaseScript>().AddMoney(getMoney + (int)((float)getMoney * (rate / 100)));
            Destroy(gameObject);
        }
        healthBar.fillAmount = (currentHP / HP);
        armorBar.fillAmount = (currentArmor / Armor);
    }


    // Other Functions used to SET/GET value
    public void AddHealth(float _health)
    {
        if (currentArmor <= 0)
        {
            currentArmor = 0;
            currentHP += _health;
        }
        else
        {
            currentArmor += _health;
        }
    }

    public void PierceHit(float _health)
    {
        currentHP += _health;
    }

    // Function for Set path (note: SetPath from EnemyPath.cs)
    public void SetPath(Vector2[] pathRef)
    {
        path = pathRef;
    }
    public void SetOffset(Vector3 offset)
    {
        pathOffset = offset;
    }

    public void SetRefWaveControl(GameObject _object)
    {
        refWaveControl = _object;
    }

    public void SetBaseScript(GameObject _base)
    {
        baseScript = _base;
    }

    public GameObject GetRefWaveControl()
    {
        return refWaveControl;
    }


}
