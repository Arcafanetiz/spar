using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckController : MonoBehaviour, IDropHandler
{
    // Used as reference
    [SerializeField] private GameObject baseRef;

    // Prefab use for generate card
    [SerializeField] private GameObject cardPrefab;

    // Used as reference
    [SerializeField] private GameObject gameManager;

    // GameObject (Invisible) for storing card only
    [SerializeField] private GameObject cardStored;

    // GameObject that link with CardList.cs use for storing card
    // Used as reference
    [SerializeField] private GameObject cardList;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            GenCard();
        }
    }

    private void GenCard()
    {
        // Generate GameObject from cardPrefab
        GameObject card = Instantiate(cardPrefab, transform);
        // Set parent for showing in UI
        card.transform.SetParent(card.transform.parent.gameObject.transform);

        // Debug use for random cards
        int index = Random.Range(0, cardList.GetComponent<CardList>().cardList.Count);

        // Set refernce for card that has been generate
        card.GetComponent<DragDrop>().baseRef = baseRef;
        card.GetComponent<DragDrop>().SetGameManage(gameManager);
        card.GetComponent<DragDrop>().SetCardKeeper(cardStored);
        card.GetComponent<DragDrop>().SetCanvas(card.transform.parent.parent.gameObject.GetComponent<Canvas>());
        card.GetComponent<DragDrop>().onDeck = true;

        // Set card info (use info from CardList)
        card.GetComponent<DragDrop>().SetCardInfo(cardList.GetComponent<CardList>().cardList[index]);
    }

    private void GenCard(Card_SO _card)
    {
        // Generate GameObject from cardPrefab
        GameObject card = Instantiate(cardPrefab, transform);
        card.transform.SetParent(card.transform.parent.gameObject.transform);

        // Set refernce for card that has been generate
        card.GetComponent<DragDrop>().SetGameManage(gameManager);
        card.GetComponent<DragDrop>().SetCardKeeper(cardStored);
        card.GetComponent<DragDrop>().SetCanvas(card.transform.parent.parent.gameObject.GetComponent<Canvas>());
        card.GetComponent<DragDrop>().onDeck = true;

        // Set Card Info
        card.GetComponent<DragDrop>().SetCardInfo(_card);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject _card = eventData.pointerDrag.gameObject;
        DragDrop _DG = _card.GetComponent<DragDrop>();
        BaseScript _Base = baseRef.GetComponent<BaseScript>();


        if(_DG.inShop && _Base.money >= _DG.cardInfo.buy)
        {
            _Base.AddMoney(-_DG.cardInfo.buy);
            // Generate this card back to deck
            GenCard(_card.GetComponent<DragDrop>().cardInfo);
            // Destroy card which show in TowerUI
            Destroy(_card);
        }

        // If card was drag back to deck (must have at least 1 mana)
        else if(!_DG.inShop && !_DG.onDeck && _Base.mana >= 1.0f)
        {
            // Decrease mana
            _Base.AddMana(-1);
            if(_DG.attachWith.CompareTag("Platform"))
            {
                // Remove card from data
                GameObject towerRef = gameManager.GetComponent<MapGenerator>().GetTowerData((int)_DG.attachWith.gameObject.transform.position.x, -(int)_DG.attachWith.gameObject.transform.position.y);
                towerRef.GetComponent<TowerCardScript>().RemoveDel(_card);
            }
            else if(_DG.attachWith.CompareTag("Base"))
            {
                // Remove card from data
                _DG.attachWith.GetComponent<BaseCardScript>().RemoveDel(_card);
            }
            else if(_DG.attachWith.CompareTag("Spawner"))
            {
                // Remove card from data
                _DG.attachWith.GetComponent<SpawnerCardScript>().RemoveDel(_card);
            }
            // Generate this card back to deck
            GenCard(_card.GetComponent<DragDrop>().cardInfo);
            // Destroy card which show in TowerUI
            Destroy(_card);
        }
    }
}
