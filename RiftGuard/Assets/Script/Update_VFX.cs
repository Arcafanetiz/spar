using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_VFX : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Slider>().value = SoundController.VFX_Volume;
    }
}
