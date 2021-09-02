using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject target;
    private float speed = 10f;
    [SerializeField] private int damage;


    public void Seek(GameObject _target)
    {
        target = _target;
    }


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
        target.GetComponent<EnemyControl>().AddHealth(-damage);
        Destroy(gameObject);
    }
}
