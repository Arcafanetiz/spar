using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerUIAnimation : MonoBehaviour
{
    public GameObject UpgradeTowerUI;
    public GameObject CardCollect;
    public GameObject BaseUI;
    public GameObject SpawnerUI;

    private float CardCollectWidth;
    private float UpgradeWidth;
    private float BaseUIWidth;
    private float SpawnerUIWidth;

    private bool doOnce = true;

    private enum InfoType
    { 
        TOWER, SPAWNER, BASE
    }
    InfoType infoType;

    private void Awake()
    {
        DOTween.SetTweensCapacity(2000, 100);
        UpgradeWidth = UpgradeTowerUI.GetComponent<RectTransform>().rect.width;
        CardCollectWidth = CardCollect.GetComponent<RectTransform>().rect.width;
        BaseUIWidth = BaseUI.GetComponent<RectTransform>().rect.width;
        SpawnerUIWidth = SpawnerUI.GetComponent<RectTransform>().rect.width;
    }

    private void Update()
    {
        if (doOnce && Check())
        {
            if (GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE)
            {
                UpgradeTowerUI.GetComponent<RectTransform>().DOAnchorPosX(UpgradeWidth / 2, 1.0f, false);
                infoType = InfoType.TOWER;
            }
            else if (GameManage.currentGameStatus == GameManage.GameStatus.BASE)
            {
                BaseUI.GetComponent<RectTransform>().DOAnchorPosX(BaseUIWidth / 2, 1.0f, false);
                infoType = InfoType.BASE;
            }
            else
            {
                SpawnerUI.GetComponent<RectTransform>().DOAnchorPosX(SpawnerUIWidth / 2, 1.0f, false);
                infoType = InfoType.SPAWNER;
            }
            CardCollect.GetComponent<RectTransform>().DOAnchorPosX(CardCollectWidth / 2, 1.0f, false);
            doOnce = false;
        }
        else if (!doOnce && GameManage.currentGameStatus == GameManage.GameStatus.PLAY)
        {
            if(infoType == InfoType.TOWER)
            {
                UpgradeTowerUI.GetComponent<RectTransform>().DOAnchorPosX(-UpgradeWidth / 2, 1.0f, false);
            }
            else if (infoType == InfoType.BASE)
            {
                BaseUI.GetComponent<RectTransform>().DOAnchorPosX(-BaseUIWidth / 2, 1.0f, false);
            }
            else
            {
                SpawnerUI.GetComponent<RectTransform>().DOAnchorPosX(-SpawnerUIWidth / 2, 1.0f, false);
            }
            CardCollect.GetComponent<RectTransform>().DOAnchorPosX(-CardCollectWidth / 2, 1.0f, false);
            doOnce = true;
        }
    }

    private bool Check()
    {
        if(GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE)
        {
            return true;
        }
        else if (GameManage.currentGameStatus == GameManage.GameStatus.SPAWNER)
        {
            return true;
        }
        else if (GameManage.currentGameStatus == GameManage.GameStatus.BASE)
        {
            return true;
        }
        return false;
    }    

}
