using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    // For Checking that this grid has tower or not
    public static bool[,] mapCheck;
    private GameObject[,] mapData;
    private GameObject[,] towerData;

    private int width;
    private int height;


    private void Start()
    {
        // Set size of 2D Array
        GenerateGrid();
        //Check();
    }

    private void GenerateGrid()
    {
        GameObject[] checkMapGrid;

        checkMapGrid = GameObject.FindGameObjectsWithTag("Platform");

        for (int j = 0; j < checkMapGrid.Length; j++)
        {
            int x = Mathf.Abs((int)checkMapGrid[j].transform.position.x);
            int y = Mathf.Abs((int)checkMapGrid[j].transform.position.y);
            width = (width < x) ? x : width;
            height = (height < y) ? y : height;
        }

        mapData = new GameObject[width+1, height+1];
        mapCheck = new bool[width+1, height+1];
        towerData = new GameObject[width + 1, height + 1];

        for (int j = 0; j < checkMapGrid.Length; j++)
        {
            int x = Mathf.Abs((int)checkMapGrid[j].transform.position.x);
            int y = Mathf.Abs((int)checkMapGrid[j].transform.position.y);
            mapData[x,y] = checkMapGrid[j];
        }
    }

    private void Check()
    {
        print(width + " " + height);
        for(int j=0;j<=width;j++)
        {
            for(int k=0;k<=height;k++)
            {
                print(j + " " + k + "  " + mapData[j,k]);
            }
        }
    }


    
    // Other Function used to SET/GET value
    public Vector2 GetMapSize()
    {
        return new Vector2(width, height);
    }

    public GameObject GetTowerData(int _xPos,int _yPos)
    {
        return towerData[_xPos, _yPos];
    }

    public GameObject GetMapData(int _xPos,int _yPos)
    {
        return mapData[_xPos, _yPos];
    }

    public void SetTower(int _xPos, int _yPos,GameObject _object)
    {
        towerData[_xPos, _yPos] = _object;
    }
}
