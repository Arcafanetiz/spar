using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    // Keep Text for showing on Card Prefab
    [SerializeField] private Text _cardName;
    [SerializeField] private Text _cardType;
    [SerializeField] private Text _cardPrice;
    [SerializeField] private GameObject _cardImage;

    private void Start()
    {
        Card_SO cardInfoRef = this.GetComponent<DragDrop>().cardInfo;
        // Set Card name UI
        _cardName.text = cardInfoRef.description;
        _cardImage.GetComponent<Image>().sprite = cardInfoRef.image;
        

        // Set Card type UI
        if (cardInfoRef.cardType == Card_SO.Type.BASE)
            _cardType.text = "BASE";
        else if (cardInfoRef.cardType == Card_SO.Type.TOWER)
            _cardType.text = "TOWER";
        else
            _cardType.text = "SPAWNER";
    }

    public void ShowCostSell(string _price)
    {
        _cardPrice.text = _price;
        _cardPrice.enabled = true;
        _cardPrice.color = new Color(1.0f, 0.0f, 0.0f,1.0f);
    }
    
    public void ShowCostBuy(string _price)
    {
        _cardPrice.text = _price;
        _cardPrice.enabled = true;
        _cardPrice.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    }

    public void HideCost()
    {
        _cardPrice.enabled = false;
    }
}
