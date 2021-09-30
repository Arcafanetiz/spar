using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveControl : MonoBehaviour
{
    [SerializeField] private GameObject Base;
    [SerializeField] private List<GameObject> allSpawn;

    private int _currentWave = 0; // Start with Index (0-99)
    private int _maxWave = 0;
    private bool waveReady = true;
    private int waveAmount = 0; // Number of All enemy in each wave

    public int currentWave => _currentWave;
    public int maxWave => _maxWave;

    private void Awake()
    {
        for (int j = 0; j < allSpawn.Count; j++)
        {
            int temp = allSpawn[j].GetComponent<SpawnerControl>().GetSpawnLength();
            allSpawn[j].GetComponent<SpawnerControl>().SetRefWaveControl(this.gameObject);
            allSpawn[j].GetComponent<SpawnerControl>().SetBaseScript(Base);
            _maxWave = (temp > _maxWave) ? temp : _maxWave;
        }
    }


    void Update()
    {
        if (waveReady)
        {
            if (_currentWave == maxWave)
            {
                return;
            }

            waveAmount = 0;
            for (int j = 0; j < allSpawn.Count; j++)
            {
                waveAmount += allSpawn[j].GetComponent<SpawnerControl>().GetAmount(_currentWave);
                allSpawn[j].GetComponent<SpawnerControl>().SetCurrentWave(_currentWave);
                allSpawn[j].GetComponent<SpawnerControl>().ReadyToSpawn(true);
            }

            waveReady = false;
            _currentWave++;
        }
        else if(waveAmount == 0)
        {
            waveReady = true;
        }
        //print(waveAmount);
    }

    public void DecreaseEnemy()
    {
        waveAmount--;
    }
}
