using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTower : MonoBehaviour
{
    [SerializeField] private GameObject RotateSprite;

    void Update()
    {
        RotateSprite.transform.Rotate(0, 0, Time.deltaTime*-100);
    }
}
