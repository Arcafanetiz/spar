using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject cam1;
    [SerializeField] private GameObject cam2;
    [SerializeField] private float speed;

    bool check = false;

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if(Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime,transform.position.y);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime,transform.position.y);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if (check)
            {
                cam1.SetActive(false);
                cam2.SetActive(true);
                check = false;
            }
            else
            {
                cam1.SetActive(true);
                cam2.SetActive(false);
                check = true;
            }
        }
    }
}
