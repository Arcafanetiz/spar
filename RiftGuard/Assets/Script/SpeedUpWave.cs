using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpWave : MonoBehaviour
{
    [HideInInspector] public bool isSpeedUp = false;
    [SerializeField] private Text SpeedText;

    public void SpeedUp()
    {
        if (!isSpeedUp)
        {
            Time.timeScale = 2.5f;
            SpeedText.text = "x2";
            isSpeedUp = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            SpeedText.text = "x1";
            isSpeedUp = false;
        }
    }
}
