using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [Header("Enemy Properties")]
        [SerializeField] private float SPD;
        [SerializeField] private float DEF;
        [SerializeField] private float HP;
        [SerializeField] private int getMoney;

    private Vector2[] path;
    private GameObject refWaveControl;
    private GameObject baseScript;
    private float currentHP;

    private int now = 0;

    private void Start()
    {
        currentHP = HP;
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
            transform.Translate(Vector3.right * SPD * Time.deltaTime);
            if (transform.position.x >= path[now + 1].x)
            {
                now++;
            }
        }
        else if((int)dif.x < 0)
        {
            transform.Translate(Vector3.left * SPD * Time.deltaTime);
            if (transform.position.x <= path[now + 1].x)
            {
                now++;
            }
        }
        else if ((int)dif.y > 0)
        {
            transform.Translate(Vector3.up * SPD * Time.deltaTime);
            if (transform.position.y >= path[now + 1].y)
            {
                now++;
            }
        }
        else if ((int)dif.y < 0)
        {
            transform.Translate(Vector3.down * SPD * Time.deltaTime);
            if (transform.position.y <= path[now + 1].y)
            {
                now++;
            }
        }

    }

    private void CheckHealth()
    {
        if(currentHP <= 0 )
        {
            refWaveControl.GetComponent<WaveControl>().DecreaseEnemy();
            baseScript.GetComponent<BaseScript>().AddMoney(getMoney);
            baseScript.GetComponent<BaseScript>().UpdateMoney();
            Destroy(gameObject);
        }
        healthBar.fillAmount = (currentHP / HP);
    }


    // Other Functions used to SET/GET value
    public void AddHealth(float _health)
    {
        currentHP += _health;
    }

    // Function for Set path (note: SetPath from EnemyPath.cs)
    public void SetPath(Vector2[] pathRef)
    {
        path = pathRef;
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
