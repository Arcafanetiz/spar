using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private GameObject CamObject;
    [SerializeField] private float CamSpeed;
    [SerializeField] private float tolerance;

    void Start()
    {
        CamObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (GameManage.currentGameStatus == GameManage.GameStatus.PLAY)
        {
            GetInput();
        }
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            CamObject.transform.eulerAngles = new Vector3(0, 0, CamObject.transform.eulerAngles.z + Time.deltaTime * CamSpeed / Time.timeScale);
        }
        if (Input.GetKey(KeyCode.E))
        {
            CamObject.transform.eulerAngles = new Vector3(0, 0, CamObject.transform.eulerAngles.z - Time.deltaTime * CamSpeed / Time.timeScale);
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        {
            if (Mathf.Abs(CamObject.transform.eulerAngles.z) <= tolerance)
            {
                CamObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Mathf.Abs(CamObject.transform.eulerAngles.z - 90) <= tolerance)
            {
                CamObject.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (Mathf.Abs(CamObject.transform.eulerAngles.z - 180) <= tolerance)
            {
                CamObject.transform.eulerAngles = new Vector3(0, 0, 180);
            }
            else if (Mathf.Abs(CamObject.transform.eulerAngles.z - 270) <= tolerance)
            {
                CamObject.transform.eulerAngles = new Vector3(0, 0, 270);
            }
        }
    }
}
