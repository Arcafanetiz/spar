using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameObject gamePlaySound; // Sound store in Camera
    private bool MuteBackgroundSound = false;

    public void MuteUnmuteSound()
    {
        if (MuteBackgroundSound)
        {
            gamePlaySound.GetComponent<AudioSource>().volume = 0.7f;
            MuteBackgroundSound = false;
        }
        else
        {
            gamePlaySound.GetComponent<AudioSource>().volume = 0.0f;
            MuteBackgroundSound = true;
        }
    }

}
