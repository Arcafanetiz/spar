using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBulletSoundController : MonoBehaviour
{
    float time;
    float currentTime;

    private void Start()
    {
        time = 3.0f;
        currentTime = 0.0f;
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (currentTime >= time)
        {
            Destroy(gameObject);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
