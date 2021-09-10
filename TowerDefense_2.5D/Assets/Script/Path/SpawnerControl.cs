using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector2[] path;
    [SerializeField] private float waveTime;
    private float timeCount;

    private void Update()
    {
        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            GenerateEnemy();
        }
    }

    private void GenerateEnemy()
    {
        if (timeCount >= waveTime)
        {
            GameObject spawnEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            spawnEnemy.GetComponent<EnemyControl>().SetPath(path);
            timeCount = 0;
        }
        else
        {
            timeCount += Time.deltaTime;
        }

    }

}
