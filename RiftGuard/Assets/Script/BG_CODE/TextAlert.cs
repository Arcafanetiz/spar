using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlert : MonoBehaviour
{
    private Text textObject;

    private float countTime;
    private bool doOnce;
    // Use for Count Time
    private float currentTime;

    private void Start()
    {
        textObject = GetComponent<Text>();
    }

    private void Update()
    {
        if(doOnce)
        {
            Color color = textObject.color;
            color.a = (currentTime / countTime);
            textObject.color = color;

            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                textObject.text = "";
                doOnce = false;
            }
        }
    }

    public void Alert(string _text, float time)
    {
        textObject.text = _text;
        countTime = time;
        doOnce = true;
        currentTime = time;
    }
}
