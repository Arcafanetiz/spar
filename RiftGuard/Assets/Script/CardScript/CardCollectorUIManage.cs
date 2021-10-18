using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectorUIManage : MonoBehaviour
{
    public GameObject button;
    public Sprite UpButton;
    public Sprite DownButton;

    [HideInInspector] public bool isHide;

    private float UIheight;
    private float buttonHeight;

    private void Awake()
    {
        DOTween.SetTweensCapacity(2000, 100);
        UIheight = this.GetComponent<RectTransform>().rect.height;
        buttonHeight = button.GetComponent<RectTransform>().rect.height;
    }

    public void CloseOpenUI()
    {
        if(!isHide)
        {
            isHide = true;
            this.GetComponent<RectTransform>().DOAnchorPosY(-UIheight, 1.0f, true);
            button.GetComponent<RectTransform>().DOAnchorPosY(buttonHeight / 2 , 1.0f, true);
            button.GetComponent<Image>().sprite = UpButton;
        }
        else
        {
            isHide = false;
            this.GetComponent<RectTransform>().DOAnchorPosY(0.0f, 1.0f, true);
            button.GetComponent<RectTransform>().DOAnchorPosY(buttonHeight / 2 + UIheight, 1.0f, true);
            button.GetComponent<Image>().sprite = DownButton;
        }
    }

}
