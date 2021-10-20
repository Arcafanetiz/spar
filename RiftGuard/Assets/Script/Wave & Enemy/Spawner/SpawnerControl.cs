using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour
{

    [System.Serializable] struct Enemy
    {
        public GameObject enemy;
        public int amount;
    };


    [System.Serializable] private class WaveInfo
    {
        public Enemy[] _enemy;
        public float speedPerEnemy;
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
    [SerializeField] private Vector2[] path;
    [SerializeField] private List<WaveInfo> wave;
    private GameObject refWaveControl;
    private GameObject baseScript;
    private float timeCount;
    private bool readyToSpawn = false;
    private int currentWave = 0;
    private int checkEnemy = 0;
    private int allEnemy = 0;

    private int currentType = 0;

    [HideInInspector] public float slowRate;
    [HideInInspector] public float healthRate;
    [HideInInspector] public float speedRate;
    [HideInInspector] public float defRate;

    private void Start()
    {
        slowRate = 0.0f;
        healthRate = 0.0f;
        speedRate = 0.0f;
        defRate = 0.0f;
    }

    private void Update()
    {
        if (wave.Count == 0)
        {
            return;
        }

        if(readyToSpawn) // Receive from WaveControl.cs
        {
            if(allEnemy == 0)
            {
                for (int j = 0; j < wave[currentWave]._enemy.Length; j++)
                {
                    allEnemy += wave[currentWave]._enemy[j].amount;
                }
            }
            // Spawn Enemy
            GenerateEnemy(currentWave);
        }
    }

    private void GenerateEnemy(int index)
    {
        // if this Spawner spawn all enemy then stop and wait for next Wave
        if(checkEnemy == allEnemy)
        {
            readyToSpawn = false;
            checkEnemy = 0;
            allEnemy = 0;
            currentType = 0;
        }

        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            // Use Time.deltaTime to count time (including slowRate)
            if (timeCount >= wave[index].speedPerEnemy * (1 + slowRate / 100))
            {
                //print(wave[currentWave]._enemy[currentType].amount + " " + currentType);
                if(wave[index]._enemy[currentType].amount == 0)
                {
                    currentType++;
                }

                wave[index]._enemy[currentType].amount--;
                GameObject enemy = wave[index]._enemy[currentType].enemy;

                Vector3 targetOffset;
                targetOffset = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), 0);
                GameObject spawnEnemy = Instantiate(enemy, transform.position + targetOffset, Quaternion.identity);
                // Access EnemyControl to set value
                spawnEnemy.GetComponent<EnemyControl>().SetOffset(targetOffset);
                spawnEnemy.GetComponent<EnemyControl>().HP -= (spawnEnemy.GetComponent<EnemyControl>().HP * (healthRate / 100));
                spawnEnemy.GetComponent<EnemyControl>().SPD -= (spawnEnemy.GetComponent<EnemyControl>().SPD * (speedRate / 100));
                spawnEnemy.GetComponent<EnemyControl>().DEF -= (spawnEnemy.GetComponent<EnemyControl>().DEF * (defRate / 100));
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
        if(wave.Count == 0)
        {
            return 0;
        }

        int cnt = 0;
        for (int j = 0; j < wave[index]._enemy.Length; j++)
        {
            cnt += wave[index]._enemy[j].amount;
        }
        return cnt;
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
