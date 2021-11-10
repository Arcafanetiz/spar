using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckController : MonoBehaviour, IDropHandler
{
    // Used as reference
    [SerializeField] private GameObject baseRef;
    private BaseScript _baseScript;

    // Prefab use for generate card
    [SerializeField] private GameObject cardPrefab;

    // Used as reference
    [SerializeField] private GameObject gameManager;

    [SerializeField] private GameObject debugText;

    // GameObject (Invisible) for storing card only
    [SerializeField] private GameObject cardStored;

    // GameObject that link with CardList.cs use for storing card
    // Used as reference
    [SerializeField] private GameObject cardList;

    [SerializeField] private int _cardCapacity;

    static public int cardCapacity;
    static public int currentCapacity;

    void Start()
    {
        cardCapacity = _cardCapacity;
        currentCapacity = 0;
        _baseScript = baseRef.GetComponent<BaseScript>();
    }

    void Update()
    {
        //print(currentCapacity + " " + cardCapacity);
        if (Input.GetKeyDown(KeyCode.T) && currentCapacity < cardCapacity)
        {
            GenCard();
        }
    }

    public void GenCard()
    {
        // Increase amount of card in deck
        currentCapacity++;
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
        card.GetComponent<DragDrop>().debugText = debugText;

        // Set card info (use info from CardList)
        card.GetComponent<DragDrop>().SetCardInfo(cardList.GetComponent<CardList>().cardList[index]);
    }

    private void GenCard(Card_SO _card)
    {
        // Increase amount of card in deck
        currentCapacity++;
        // Generate GameObject from cardPrefab
        GameObject card = Instantiate(cardPrefab, transform);
        card.transform.SetParent(card.transform.parent.gameObject.transform);

        // Set refernce for card that has been generate
        card.GetComponent<DragDrop>().SetGameManage(gameManager);
        card.GetComponent<DragDrop>().SetCardKeeper(cardStored);
        card.GetComponent<DragDrop>().SetCanvas(card.transform.parent.parent.gameObject.GetComponent<Canvas>());
        card.GetComponent<DragDrop>().onDeck = true;
        card.GetComponent<DragDrop>().debugText = debugText;

        // Set Card Info
        card.GetComponent<DragDrop>().SetCardInfo(_card);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject _card = eventData.pointerDrag.gameObject;
        DragDrop _DG = _card.GetComponent<DragDrop>();
        BaseScript _Base = baseRef.GetComponent<BaseScript>();


        if (currentCapacity == cardCapacity)
        {
            debugText.GetComponent<TextAlert>().Alert("Card in deck was full", 2.5f);
            return;
        }

        if (_DG.inShop && _Base.money >= _DG.cardInfo.buy)
        {
            if (GameManage.currentGameStatus == GameManage.GameStatus.SHOP)
            {
                _card.transform.parent.GetComponent<Canvas>().sortingOrder = 58;
            }

            // Decrase Money
            _Base.AddMoney(-_DG.cardInfo.buy);
            // Generate this card back to deck
            GenCard(_card.GetComponent<DragDrop>().cardInfo);
            // Destroy card which show in TowerUI
            Destroy(_card);
        }
        else if (_DG.inShop && _Base.money < _DG.cardInfo.buy)
        {
            debugText.GetComponent<TextAlert>().Alert("Don't have enough money to buy card", 2.5f);
        }

        // If card was drag back to deck (must have <= manaUsage)
        else if (!_DG.inShop && !_DG.onDeck && _Base.mana >= _baseScript.manaUsage)
        {
            // Decrease mana
            _Base.AddMana(-_baseScript.manaUsage);
            if (_DG.attachWith.CompareTag("Platform"))
            {
                // Remove card from data
                GameObject towerRef = gameManager.GetComponent<MapGenerator>().GetTowerData((int)_DG.attachWith.gameObject.transform.position.x, -(int)_DG.attachWith.gameObject.transform.position.y);
                towerRef.GetComponent<TowerCardScript>().RemoveDel(_card);
            }
            else if (_DG.attachWith.CompareTag("Base"))
            {
                // Remove card from data
                _DG.attachWith.GetComponent<BaseCardScript>().RemoveDel(_card);
            }
            else if (_DG.attachWith.CompareTag("Spawner"))
            {
                // Remove card from data
                _DG.attachWith.GetComponent<SpawnerCardScript>().RemoveDel(_card);
            }
            // Generate this card back to deck
            GenCard(_card.GetComponent<DragDrop>().cardInfo);
            // Destroy card which show in TowerUI
            Destroy(_card);
        }
        else if (!_DG.inShop && !_DG.onDeck && _Base.mana < _baseScript.manaUsage)
        {
            debugText.GetComponent<TextAlert>().Alert("Don't have enough mana to drag card out", 2.5f);
        }
    }

    public int currentCard()
    {
        return currentCapacity;
    }
}
