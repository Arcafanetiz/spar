using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour
{
    [System.Serializable] private class WaveInfo
    {
        public int amount;
        public float speed;
    }

    // Collect Direction of spawner
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    };

    [SerializeField] public Direction direction;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector2[] path;
    [SerializeField] private List<WaveInfo> wave;
    private GameObject refWaveControl;
    private GameObject baseScript;
    private float timeCount;
    private bool readyToSpawn = false;
    private int currentWave = 0;
    private int checkEnemy = 0;

    [HideInInspector] public float slowRate = 1.0f;

    private void Update()
    {
        if(readyToSpawn) // Receive from WaveControl.cs
        {
            // Spawn Enemy
            GenerateEnemy(currentWave);
        }
    }

    private void GenerateEnemy(int index)
    {
        // if this Spawner spawn all enemy then stop and wait for next Wave
        if(checkEnemy == wave[index].amount)
        {
            readyToSpawn = false;
            checkEnemy = 0;
        }

        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            // Use Time.deltaTime to count time (including slowRate)
            if (timeCount >= wave[index].speed * slowRate)
            {
                Vector3 targetOffset;
                targetOffset = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), 0);
                GameObject spawnEnemy = Instantiate(enemy, transform.position + targetOffset, Quaternion.identity);
                // Access EnemyControl to set value
                spawnEnemy.GetComponent<EnemyControl>().SetOffset(targetOffset);
                spawnEnemy.GetComponent<EnemyControl>().SetPath(path);
                spawnEnemy.GetComponent<EnemyControl>().SetRefWaveControl(refWaveControl);
                spawnEnemy.GetComponent<EnemyControl>().SetBaseScript(baseScript);
                // Increase checkEnemy
                checkEnemy++;
                //Reset time to 0
                timeCount = 0;
            }
            else
            {
                timeCount += Time.deltaTime;
            }
        }
    }


    // GET/SET value
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
