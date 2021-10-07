using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Array of GameObject for storing all camera
    [SerializeField] private GameObject[] camList;
    private int index;

    private void Start()
    {
        // Set index to 0 (set to first cam in array)
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
            // Set previous cam to be false
            camList[index].SetActive(false);

            // If index is < 0 then go to camList.Length-1
            index = (index - 1 < 0) ? camList.Length - 1 : index - 1;

            // Set now cam to true
            camList[index].SetActive(true);
        }

        // Pressed R to Rotate Clockwise
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Set previous cam to be false
            camList[index].SetActive(false);

            // If index is > camList.Length-1 then go to index 0 
            index = (index + 1) % camList.Length;

            // Set now cam to true
            camList[index].SetActive(true);
        }
    }

}
