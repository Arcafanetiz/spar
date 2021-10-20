using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveControl : MonoBehaviour
{
    [SerializeField] private GameObject Base;
    [SerializeField] private List<GameObject> allSpawn;
    [SerializeField] private float timePerWave;

    private int _currentWave = 0; // Start with Index (0-99)
    private int _maxWave = 0;
    private bool waveReady = true;
    private int waveAmount = 0; // Number of All enemy in each wave

    [HideInInspector] public bool top = false;
    [HideInInspector] public bool bot = false;
    [HideInInspector] public bool left = false;
    [HideInInspector] public bool right = false;

    public int currentWave => _currentWave;
    public int maxWave => _maxWave;

    private bool doOnce = false;
    private float timeCount = 0;

    private void Awake()
    {
        for (int j = 0; j < allSpawn.Count; j++)
        {
            int temp = allSpawn[j].GetComponent<SpawnerControl>().GetSpawnLength();
            allSpawn[j].GetComponent<SpawnerControl>().SetRefWaveControl(this.gameObject);
            allSpawn[j].GetComponent<SpawnerControl>().SetBaseScript(Base);
            allSpawn[j].GetComponent<SpawnerCardScript>().baseRef = Base;
            // Find maxWave using ternary operator
            _maxWave = (temp > _maxWave) ? temp : _maxWave;
        }
    }


    void Update()
    {
        if (waveReady && GameManage.currentGameStatus != GameManage.GameStatus.SHOP)
        {
            // if all wave are done
            if (_currentWave == maxWave)
            {
                return;
            }

            waveAmount = 0;
            for (int j = 0; j < allSpawn.Count; j++)
            {
                SpawnerControl spawnControl = allSpawn[j].GetComponent<SpawnerControl>();
                // waveAmount -> Contain all of enemy in each wave
                spawnControl.SetCurrentWave(_currentWave);
                waveAmount += spawnControl.GetAmount(_currentWave);

                // If there are enemy check that direction
                if(spawnControl.GetAmount(_currentWave) > 0)
                {
                    Check(spawnControl);
                }
                // Set current wave to each spawner
                // Set Ready to Spawn = true -> make Enemy appear
                spawnControl.ReadyToSpawn(true);
            }
            waveReady = false;
            _currentWave++;
            doOnce = true;
            timeCount = 0;
        }
        else if(doOnce && waveAmount == 0)
        {
            if (_currentWave % 5 == 0)
            {
                GameManage.currentGameStatus = GameManage.GameStatus.SHOP;
            }

            // Set all direction to be false
            top = false;
            bot = false;
            right = false;
            left = false;

            // Do Only one time
            doOnce = false;
        }
        else if (waveAmount == 0)
        {
            if(timeCount >= timePerWave)
            {
                waveReady = true;
            }
            else
            {
                timeCount += Time.deltaTime;
            }
        }
    }

    public void DecreaseEnemy()
    {
        waveAmount--;
    }

    // Check for direction
    public void Check(SpawnerControl _spawnControl)
    {
        if(_spawnControl.direction == SpawnerControl.Direction.Top)
        {
            top = true;
        }
        else if (_spawnControl.direction == SpawnerControl.Direction.Bottom)
        {
            bot = true;
        }
        else if (_spawnControl.direction == SpawnerControl.Direction.Left)
        {
            left = true;
        }
        else if (_spawnControl.direction == SpawnerControl.Direction.Right)
        {
            right = true;
        }
    }
}
