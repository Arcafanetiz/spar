using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundGameUI : MonoBehaviour
{
    [SerializeField] Sprite Unmute;
    [SerializeField] Sprite Mute;


    private void Update()
    {
        if(!SoundController.MuteSound)
        {
            this.GetComponent<Image>().sprite = Unmute;
        }
        else
        {
            this.GetComponent<Image>().sprite = Mute;
        }
    }
}
