using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private GameObject CamObject;
    [SerializeField] private float CamSpeed;

    void Start()
    {
        CamObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            CamObject.transform.eulerAngles = new Vector3(0, 0, CamObject.transform.eulerAngles.z + Time.deltaTime * CamSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            CamObject.transform.eulerAngles = new Vector3(0, 0, CamObject.transform.eulerAngles.z - Time.deltaTime * CamSpeed);
        }
    }
}
