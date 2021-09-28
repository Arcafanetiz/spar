using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private Text waveText;

    void Update()
    {
        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
           GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            ShowNumWave();
        }
    }

    private void ShowNumWave()
    {
        waveText.text = this.GetComponent<WaveControl>().currentWave.ToString() + " / " + this.GetComponent<WaveControl>().maxWave.ToString();
    }
}
