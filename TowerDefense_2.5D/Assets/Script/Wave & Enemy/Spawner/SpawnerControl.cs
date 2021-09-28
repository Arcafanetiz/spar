using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour
{
    [System.Serializable] private class WaveInfo
    {
        public int amount;
        public int speed;
    }

    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector2[] path;
    [SerializeField] private List<WaveInfo> wave;
    private GameObject refWaveControl;
    private GameObject baseScript;
    private float timeCount;
    private bool readyToSpawn = false;
    private int currentWave = 0;
    private int checkEnemy = 0;

    private void Update()
    {
        if(readyToSpawn)
        {
            GenerateEnemy(currentWave);
        }
    }

    private void GenerateEnemy(int index)
    {
        if(checkEnemy == wave[index].amount)
        {
            readyToSpawn = false;
            checkEnemy = 0;
        }

        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            if (timeCount >= wave[index].speed)
            {
                GameObject spawnEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
                spawnEnemy.GetComponent<EnemyControl>().SetPath(path);
                spawnEnemy.GetComponent<EnemyControl>().SetRefWaveControl(refWaveControl);
                spawnEnemy.GetComponent<EnemyControl>().SetBaseScript(baseScript);
                checkEnemy++;
                timeCount = 0;
            }
            else
            {
                timeCount += Time.deltaTime;
            }
        }
    }

    public void ReadyToSpawn(bool _check)
    {
        readyToSpawn = _check;
    }

    public void SetCurrentWave(int _wave)
    {
        currentWave = _wave;
    }

    public int GetSpawnLength()
    {
        return wave.Count;
    }

    public int GetAmount(int index)
    {
        return wave[index].amount;
    }

    public void SetRefWaveControl(GameObject _object)
    {
        refWaveControl = _object;
    }
    
    public void SetBaseScript(GameObject _base)
    {
        baseScript = _base;
    }
}
