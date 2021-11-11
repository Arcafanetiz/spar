using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSoundUI : MonoBehaviour
{
    [SerializeField] Sprite Unmute;
    [SerializeField] Sprite Mute;


    private void Update()
    {
        if (!SoundController.MuteBackgroundSound)
        {
            this.GetComponent<Image>().sprite = Unmute;
        }
        else
        {
            this.GetComponent<Image>().sprite = Mute;
        }
    }
}
