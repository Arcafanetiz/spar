using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCardLayer : MonoBehaviour
{

    DragDrop _DG;
    private void Start()
    {
        _DG = this.gameObject.GetComponent<DragDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManage.currentGameStatus == GameManage.GameStatus.SHOP)
        {
            if(_DG.inShop && _DG.drag)
            {
                this.transform.parent.GetComponent<Canvas>().sortingOrder = 59;
            }
            else if(_DG.onDeck && _DG.drag)
            {
                this.transform.parent.GetComponent<Canvas>().sortingOrder = 59;
            }
        }
    }
}
