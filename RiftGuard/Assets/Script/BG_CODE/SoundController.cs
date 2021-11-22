using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameObject gamePlaySound; // Sound store in Camera
    [HideInInspector] public static float BGM_Volume = 0.3f;
    [HideInInspector] public static float VFX_Volume = 1.0f;

    [SerializeField] private GameObject BGM;
    [SerializeField] private GameObject VFX;

    public void UpdateSound()
    {
        BGM_Volume = BGM.GetComponent<Slider>().value;
        VFX_Volume = VFX.GetComponent<Slider>().value;

        gamePlaySound.GetComponent<AudioSource>().volume = BGM_Volume;
    }

}
