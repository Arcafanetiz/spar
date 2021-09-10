using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyControl : MonoBehaviour
{
    private Vector2[] path;
    [Header("Enemy Properties")]
        [SerializeField] private float SPD;
        [SerializeField] private float DEF;
        [SerializeField] private float HP;
        [SerializeField] private int getMoney;

    private int now = 0;

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
        if(HP <= 0 )
        {
            Destroy(gameObject);
        }
    }


    // Other Functions used to SET/GET value
    public void AddHealth(float _health)
    {
        HP += _health;
    }

    // Function for Set path (note: SetPath from EnemyPath.cs)
    public void SetPath(Vector2[] pathRef)
    {
        path = pathRef;
    }

}
