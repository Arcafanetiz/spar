using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardScript : MonoBehaviour
{
    // number of capacity that card can hold
    [SerializeField] private int _cardCapacity;
    // Use as reference
    [SerializeField] private GameObject gameManager;

    public GameObject objectCardUI;
    private int _currentCardCapacity;
    private List<GameObject> card;
    private bool[] cardCheck;

    public GameObject cardInteract; // Can drag -> Green Color / Can't drag Red Color

    private bool doOnce = false;


    void Start()
    {
        // Initialize card
        card = new List<GameObject>();
        cardCheck = new bool[_cardCapacity];
        cardInteract.SetActive(false);
    }


    void Update()
    {
        // When Click at base show card in base !! ONLY 1 TIME !!
        if (doOnce && GameManage.currentGameStatus == GameManage.GameStatus.BASE)
        {
            GenCard();
            doOnce = false;
        }

        // When Click out from base hide status and Clear card that show
        // !! CLEAR ONLY 1 TIME !!
        if (GameManage.currentGameStatus == GameManage.GameStatus.PLAY)
        {
            ClearCardUI();
            doOnce = true;
        }
    }

    public void GenCard()
    {
        for (int j = 0; j < card.Count; j++)
        {
            // if card[j] has been Generate -> Skipped it
            if(cardCheck[j])
            {
                continue;
            }
            
            // Generate this card to be display on screen
            GameObject cardGen = Instantiate(card[j], objectCardUI.transform);

            // Name card (easy to check)
            cardGen.gameObject.name = "(" + this.gameObject.name + ")#" + j;

            // Change Scale of card and set parent
            cardGen.transform.localScale = new Vector2(0.75f, 0.75f);
            cardGen.transform.SetParent(objectCardUI.transform);

            // Set variable(parent of Card) to check that it is on deck or on object
            cardGen.GetComponent<DragDrop>().parentCard = card[j];
            cardGen.GetComponent<DragDrop>().parentObject = this.gameObject;

            // Set other GameObject things to use as reference
            cardGen.GetComponent<DragDrop>().cardKeeper = card[j].GetComponent<DragDrop>().cardKeeper;
            cardGen.GetComponent<DragDrop>().SetCanvas(objectCardUI.transform.parent.gameObject.GetComponent<Canvas>());
            cardGen.GetComponent<DragDrop>().SetGameManage(gameManager);
            cardGen.GetComponent<DragDrop>().baseRef = this.gameObject;

            // this card has been generated
            cardCheck[j] = true;
        }
    }

    public void ClearCardUI()
    {
        // foreach object to destroy the card when click off the base
        foreach (Transform child in objectCardUI.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        // Make this card to be ungenerated (use to check)
        for (int j = 0; j < card.Count; j++)
        {
            cardCheck[j] = false;
        }
    }

    public void Add(GameObject _object)
    {
        // Add card to this base
        _currentCardCapacity++;
        card.Add(_object);
    }

    public bool Check()
    {
        // Check that card is full or not
        return (_currentCardCapacity < _cardCapacity) ? true : false;
    }

    public void RemoveDel(GameObject _card)
    {
        // Removecard from base and delete them
        GameObject temp = _card.GetComponent<DragDrop>().parentCard;
        for (int j = 0; j < card.Count; j++)
        {
            if (card[j] == temp)
            {
                cardCheck[j] = false;
                break;
            }
        }
        card.Remove(temp);
        _currentCardCapacity--;
        temp.GetComponent<DragDrop>().cardInfo._abilities.DeactivateAbility(this.gameObject);
        Destroy(temp);
    }

    public void CardActive(bool value)
    {
        // Check that those card that will be drag in are mistype or not
        cardInteract.SetActive(true);
        if (value)
        {
            cardInteract.GetComponent<SpriteRenderer>().color = new Vector4(0.0f, 1.0f, 0.0f, 0.5f);
        }
        else
        {
            cardInteract.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 0.5f);
        }
    }
}
