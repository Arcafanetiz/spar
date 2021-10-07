using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAlert : MonoBehaviour
{
    [SerializeField] private WaveControl _waveControl;

    [SerializeField] private GameObject top;
    [SerializeField] private GameObject bot;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    private void Update()
    {
        if (GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
            GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER)
        {
            CheckDirection();
        }
    }

    // Check direction for set Gameobject active
    private void CheckDirection()
    {
        if (_waveControl.top)
        {
            top.SetActive(true);
        }
        else
        {
            top.SetActive(false);
        }

        if (_waveControl.bot)
        {
            bot.SetActive(true);
        }
        else
        {
            bot.SetActive(false);
        }

        if (_waveControl.left)
        {
            left.SetActive(true);
        }
        else
        {
            left.SetActive(false);
        }

        if (_waveControl.right)
        {
            right.SetActive(true);
        }
        else
        {
            right.SetActive(false);
        }
    }
}
