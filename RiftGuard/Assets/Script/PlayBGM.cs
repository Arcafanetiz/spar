using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSource>().volume = SoundController.BGM_Volume;
    }
}
