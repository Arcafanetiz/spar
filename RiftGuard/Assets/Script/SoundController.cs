using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameObject gamePlaySound; // Sound store in Camera
    [HideInInspector] public static bool MuteBackgroundSound = false;
    [HideInInspector] public static bool MuteSound = false;

    private void Start()
    {
        if(MuteBackgroundSound)
        {
            gamePlaySound.GetComponent<AudioSource>().volume = 0.0f;
        }
    }

    public void MuteUnmuteBackgroundSound()
    {
        if (MuteBackgroundSound)
        {
            gamePlaySound.GetComponent<AudioSource>().volume = 0.3f;
            MuteBackgroundSound = false;
        }
        else
        {
            gamePlaySound.GetComponent<AudioSource>().volume = 0.0f;
            MuteBackgroundSound = true;
        }
    }

    public void MuteUnmuteSound()
    {
        if (MuteSound)
        {
            MuteSound = false;
        }
        else
        {
            MuteSound = true;
        }
    }

}
