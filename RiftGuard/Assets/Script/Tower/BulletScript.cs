using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject target;
    private float speed = 10f;
    private float damage;

    [HideInInspector] public bool piercing = false;

    private void Update()
    {
        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            if(target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector2 dir = target.transform.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if(dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

    }

    private void HitTarget()
    {
        if (piercing)
        {
            target.GetComponent<EnemyControl>().PierceHit(-damage);
        }
        else
        { 
            target.GetComponent<EnemyControl>().AddHealth(-damage);
        }
        Destroy(gameObject);
    }


    // Other Functions used to SET/GET value
    public void Seek(GameObject _target)
    {
        target = _target;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
}
