using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlert : MonoBehaviour
{
    private Text textObject;
    [SerializeField] Image debugBG;

    private float countTime;
    private bool doOnce;
    // Use for Count Time
    private float currentTime;

    private void Start()
    {
        textObject = GetComponent<Text>();
        debugBG.enabled = false;
        GetComponent<Text>().enabled = false;
    }

    private void Update()
    {
        if(doOnce)
        {
            Color colorText = textObject.color;
            Color colorBG = debugBG.color;
            colorText.a = (currentTime / countTime);
            colorBG.a = (currentTime / countTime);
            debugBG.color = colorBG;
            textObject.color = colorText;

            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                textObject.text = "";
                doOnce = false;
                GetComponent<Text>().enabled = false;
                debugBG.enabled = false;
            }
        }
    }

    public void Alert(string _text, float time)
    {
        debugBG.enabled = true;
        GetComponent<Text>().enabled = true;
        textObject.text = _text;
        countTime = time;
        doOnce = true;
        currentTime = time;
    }
}
