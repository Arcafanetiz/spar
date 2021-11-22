using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Update_BGM : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Slider>().value = SoundController.BGM_Volume;
    }
}
