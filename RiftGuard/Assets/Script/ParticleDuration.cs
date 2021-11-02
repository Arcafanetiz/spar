using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDuration : MonoBehaviour
{
    float time;
    float currentTime;

    void Start()
    {
        time = GetComponent<ParticleSystem>().main.duration;
        print(time);
    }

    void Update()
    {
        if (currentTime <= time)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
