using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBlink : MonoBehaviour
{
    [SerializeField] private float blinkTime;
    private bool Blink;
    private bool raise;

    private Color color;

    private void Awake()
    {
        Blink = false;
        raise = true;
        color = this.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (Blink)
        {
            CountTime();
        }
    }

    private void CountTime()
    {
        color = GetComponent<SpriteRenderer>().color;

        if (raise)
        {
            color.a += Time.deltaTime * blinkTime;
        }
        else
        {
            color.a -= Time.deltaTime * blinkTime;
        }

        if (color.a >= 1.0f)
        {
            raise = false;
        }
        else if(color.a <= 0.0f)
        {
            raise = true;
        }
        this.GetComponent<SpriteRenderer>().color = color;
    }

    public void Activate()
    {
        Blink = true;
        color.a = 0.0f;
        this.GetComponent<SpriteRenderer>().color = color;
    }

    public bool isActivate()
    {
        return Blink;
    }

    public void Deactivate()
    {
        Blink = false;
        raise = true;
        color.a = 0.0f;
        this.GetComponent<SpriteRenderer>().color = color;
    }
}
