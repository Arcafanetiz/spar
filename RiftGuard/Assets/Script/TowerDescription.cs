using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDescription : MonoBehaviour
{
    [SerializeField] private GameObject des_Tower_1;
    [SerializeField] private GameObject des_Tower_2;
    [SerializeField] private GameObject des_Tower_3;
    [SerializeField] private GameObject des_Tower_4;

    public void Show1()
    {
        des_Tower_1.SetActive(true);
    }
    public void UnShow1()
    {
        des_Tower_1.SetActive(false);
    }

    public void Show2()
    {
        des_Tower_2.SetActive(true);
    }
    public void UnShow2()
    {
        des_Tower_2.SetActive(false);
    }

    public void Show3()
    {
        des_Tower_3.SetActive(true);
    }
    public void UnShow3()
    {
        des_Tower_3.SetActive(false);
    }

    public void Show4()
    {
        des_Tower_4.SetActive(true);
    }
    public void UnShow4()
    {
        des_Tower_4.SetActive(false);
    }
}
