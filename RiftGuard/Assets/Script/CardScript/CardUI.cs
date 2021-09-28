using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Text _cardName;
    [SerializeField] private Text _cardType;

    private void Start()
    {
        Card_SO cardInfoRef = this.GetComponent<DragDrop>().cardInfo;
        _cardName.text = cardInfoRef.cardName;
        if (cardInfoRef.cardType == Card_SO.Type.BASE)
            _cardType.text = "BASE";
        else if (cardInfoRef.cardType == Card_SO.Type.TOWER)
            _cardType.text = "TOWER";
        else
            _cardType.text = "SPAWNER";
    }
}
