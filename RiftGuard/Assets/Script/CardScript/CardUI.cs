using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    // Keep Text for showing on Card Prefab
    [SerializeField] private Text _cardName;
    [SerializeField] private Text _cardType;

    private void Start()
    {
        Card_SO cardInfoRef = this.GetComponent<DragDrop>().cardInfo;
        // Set Card name UI
        _cardName.text = cardInfoRef.description;

        // Set Card type UI
        if (cardInfoRef.cardType == Card_SO.Type.BASE)
            _cardType.text = "BASE";
        else if (cardInfoRef.cardType == Card_SO.Type.TOWER)
            _cardType.text = "TOWER";
        else
            _cardType.text = "SPAWNER";
    }
}
