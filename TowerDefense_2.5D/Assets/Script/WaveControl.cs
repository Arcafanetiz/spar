using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveControl : MonoBehaviour
{
    [SerializeField] private GameObject Base;
    [SerializeField] private List<GameObject> allSpawn;
    [SerializeField] private Text waveText;

    private int currentWave = 0; // Start with Index (0-99)
    private int maxWave = 0;
    private bool waveReady = true;
    private int waveAmount = 0; // Number of All enemy in each wave

    private void Awake()
    {
        for (int j = 0; j < allSpawn.Count; j++)
        {
            int temp = allSpawn[j].GetComponent<SpawnerControl>().GetSpawnLength();
            allSpawn[j].GetComponent<SpawnerControl>().SetRefWaveControl(this.gameObject);
            allSpawn[j].GetComponent<SpawnerControl>().SetBaseScript(Base);
            maxWave = (temp > maxWave) ? temp : maxWave;
        }
    }


    void Update()
    {
        if (waveReady)
        {
            ShowNumWave();
            waveAmount = 0;
            for (int j = 0; j < allSpawn.Count; j++)
            {
                waveAmount += allSpawn[j].GetComponent<SpawnerControl>().GetAmount(currentWave);
                allSpawn[j].GetComponent<SpawnerControl>().SetCurrentWave(currentWave);
                allSpawn[j].GetComponent<SpawnerControl>().ReadyToSpawn(true);
            }

            waveReady = false;
            currentWave++;
        }
        else if(waveAmount == 0)
        {
            waveReady = true;
        }
        //print(waveAmount);
    }

    private void ShowNumWave()
    {
        waveText.text = (currentWave+1).ToString() + " / " + maxWave.ToString();
    }

    public void DecreaseEnemy()
    {
        waveAmount--;
    }
}
