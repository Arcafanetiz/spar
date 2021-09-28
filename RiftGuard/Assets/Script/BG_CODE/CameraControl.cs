using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject[] camList;
    private int index;

    private void Start()
    {
        index = 0;

        camList[0].SetActive(true);
        for (int j = 1; j < camList.Length; j++)
        {
            camList[j].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManage.currentGameStatus != GameManage.GameStatus.GAMEOVER &&
            GameManage.currentGameStatus != GameManage.GameStatus.PAUSE)
        {
            GetInput();
        }
    }

    private void GetInput()
    {
        // Pressed Q to Rotate Counter-Clockwise
        if (Input.GetKeyDown(KeyCode.Q))
        {
            camList[index].SetActive(false);
            index = (index - 1 < 0) ? camList.Length - 1 : index - 1;
            camList[index].SetActive(true);
        }

        // Pressed R to Rotate Clockwise
        if (Input.GetKeyDown(KeyCode.E))
        {
            camList[index].SetActive(false);
            index = (index + 1) % camList.Length;
            camList[index].SetActive(true);
        }
    }

}
