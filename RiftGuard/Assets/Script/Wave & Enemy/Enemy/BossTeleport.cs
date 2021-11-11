using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    private EnemyControl enemyProperty;
    private int startTP;

    // Start is called before the first frame update
    void Start()
    {
        enemyProperty = this.GetComponent<EnemyControl>();
        startTP = 75;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyProperty.currentHP < (enemyProperty.HP * startTP) / 100)
        {
            startTP-=25;
            Teleport();
        }
    }

    private void Teleport()
    {
        GameObject _base= enemyProperty.getBaseRef();
        float dist = Vector2.Distance(this.transform.position,_base.transform.position);
        GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject currentEnemy in allEnemy)
        {
            if (currentEnemy == this.gameObject || Vector3.Distance(currentEnemy.transform.position,_base.transform.position) > dist)
            {
                continue;
            }
            else
            {
                enemyProperty.SetPath(currentEnemy.GetComponent<EnemyControl>().GetPath());
                enemyProperty.now = currentEnemy.GetComponent<EnemyControl>().now;
                this.transform.position = currentEnemy.transform.position;
                return;
            }
        }
    }
}
