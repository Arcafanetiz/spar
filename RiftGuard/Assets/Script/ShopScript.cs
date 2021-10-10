using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopScript : MonoBehaviour, IDropHandler
{
    [System.Serializable] public class ShopCardData
    {
        public int cardAmount;
        public List<Card_SO> possibleCard;
    }

    [Header("Reference")]
    public GameObject gameManager;
    public GameObject baseRef;
    public GameObject cardStored;

    [Header("Shop Setting")]
    public GameObject cardPrefab;
    public ShopCardData[] shopData;

    [HideInInspector] public int currentShop = 0;
    private bool doOnce = true;

    private Queue<GameObject> allCardInShop;

    private void Awake()
    {
        allCardInShop = new Queue<GameObject>();
    }

    private void Update()
    {
        if(doOnce && this.gameObject.activeSelf)
        {
            for (int j = 0; j < shopData[currentShop].cardAmount; j++)
            {
                int idxCard = Random.Range(0, shopData[currentShop].possibleCard.Count - 1);
                GenCard(shopData[currentShop].possibleCard[idxCard]);
            }
            doOnce = false;
            //currentShop++;
        }
    }

    // GenCard in Shop
    private void GenCard(Card_SO _cardInfo)
    {
        // Generate GameObject from cardPrefab
        GameObject card = Instantiate(cardPrefab, transform);
        // Set parent for showing in UI
        card.transform.SetParent(card.transform.parent.gameObject.transform);

        // Set refernce for card that has been generate
        card.GetComponent<DragDrop>().baseRef = baseRef;
        card.GetComponent<DragDrop>().SetGameManage(gameManager);
        card.GetComponent<DragDrop>().SetCardKeeper(cardStored);
        card.GetComponent<DragDrop>().SetCanvas(card.transform.parent.parent.gameObject.GetComponent<Canvas>());
        card.GetComponent<DragDrop>().inShop = true;
        card.GetComponent<DragDrop>().onDeck = false;

        // Set card info (use info from CardList)
        card.GetComponent<DragDrop>().SetCardInfo(_cardInfo);

        allCardInShop.Enqueue(card);
    }

    public void CloseShop()
    {
        doOnce = true;
        while(allCardInShop.Count > 0)
        {
            GameObject _card = allCardInShop.Peek();
            allCardInShop.Dequeue();
            if (_card != null)
            {
                Destroy(_card);
            }
        }
        GameManage.currentGameStatus = GameManage.GameStatus.PLAY;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject _card = eventData.pointerDrag.gameObject;
        DragDrop _DG = _card.GetComponent<DragDrop>();
        BaseScript _Base = baseRef.GetComponent<BaseScript>();

        if (_DG.onDeck)
        {
            int _money = _DG.cardInfo.sell;
            _Base.AddMoney(_money);
            Destroy(_card);

            DeckController.currentCapacity--;
        }
    }
}
