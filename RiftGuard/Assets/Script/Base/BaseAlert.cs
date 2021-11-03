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

    private bool[] doOnce;

    private void Start()
    {
        doOnce = new bool[4];
    }

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
        if (_waveControl.top && !top.GetComponent<AlertBlink>().isActivate())
        {
            top.SetActive(true);
            top.GetComponent<AlertBlink>().Activate();
        }
        else if (!_waveControl.top)
        {
            top.GetComponent<AlertBlink>().Deactivate();
            top.SetActive(false);
        }

        if (_waveControl.bot && !bot.GetComponent<AlertBlink>().isActivate())
        {
            bot.SetActive(true);
            bot.GetComponent<AlertBlink>().Activate();
        }
        else if (!_waveControl.bot)
        {
            bot.GetComponent<AlertBlink>().Deactivate();
            bot.SetActive(false);
        }

        if (_waveControl.left && !left.GetComponent<AlertBlink>().isActivate())
        {
            left.SetActive(true);
            left.GetComponent<AlertBlink>().Activate();
        }
        else if (!_waveControl.left)
        {
            left.GetComponent<AlertBlink>().Deactivate();
            left.SetActive(false);
        }

        if (_waveControl.right && !right.GetComponent<AlertBlink>().isActivate())
        {
            right.SetActive(true);
            right.GetComponent<AlertBlink>().Activate();
        }
        else if (!_waveControl.right)
        {
            right.GetComponent<AlertBlink>().Deactivate();
            right.SetActive(false);
        }
    }
}
