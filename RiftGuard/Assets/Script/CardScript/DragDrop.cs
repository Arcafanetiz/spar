using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Used as reference
    private Canvas canvas;
    private GameObject refGameManage;
    [HideInInspector] public GameObject debugText;
    [HideInInspector] public GameObject cardKeeper;
    [HideInInspector] public Card_SO cardInfo;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform parentToReturnTo = null;

    [HideInInspector] public GameObject attachWith = null;
    public GameObject baseRef;

    // For Super Cheking many things
    private bool endDrag = false;
    private bool hit = false;
    private bool doOnce = false;
    [HideInInspector] public bool drag = false;
    [HideInInspector] public bool onDeck = false;
    [HideInInspector] public bool attach = true;
    [HideInInspector] public bool inShop = false;

    // For Checking Parent (card is in Tower/Base/Spawner or Deck)
    [HideInInspector] public GameObject parentCard;
    [HideInInspector] public GameObject parentObject;

    private void Start()
    {
        // Set Component
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void FixedUpdate()
    {
        // Trigger with Base/Tower/Spawner
        if (hit && GameManage.currentGameStatus != GameManage.GameStatus.SHOP)
        {
            Check();
        }
        else if(doOnce && !drag)
        {
            this.transform.SetParent(parentToReturnTo);
            this.gameObject.GetComponent<CardUI>().HideCost();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Faded card
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // Set Parent to outside
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        // Set variable for checking when endDrag
        endDrag = false;
        doOnce = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // rectTransform relate with mouse cursor position
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        ShowCost();

        // drag = true(mouse cursor are OnDrag)
        drag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Faded card back to normal
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        // Set variable for Checking 
        endDrag = true;
        drag = false;
    }

    // For Checking when Trigger with event Object
    // Green/Red Light appear for GuideLine
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (parentObject != null)
        {
            return;
        }

        // Collision Only Tile
        // Check if That grid have tower and EndDrag

        if (collision.transform.position.x < 0 || collision.transform.position.y > 0)
        {
            return;
        }

        // Attach With Tower through Platform (endDrag)
        if (refGameManage.GetComponent<MapGenerator>().CheckMap((int)(collision.transform.position.x), (int)(-collision.transform.position.y))
            && collision.gameObject.CompareTag("Platform") && endDrag && cardInfo.cardType == Card_SO.Type.TOWER)
        {
            attachWith = collision.gameObject;
            hit = true;
            onDeck = false;
        }
        // Attach with Tower through Platform -> Green/Red Light appear
        else if (refGameManage.GetComponent<MapGenerator>().CheckMap((int)(collision.transform.position.x), (int)(-collision.transform.position.y))
            && collision.gameObject.CompareTag("Platform"))
        {
            attachWith = collision.gameObject;
            int x = (int)attachWith.transform.position.x;
            int y = -(int)attachWith.transform.position.y;

            GameObject Tower = refGameManage.GetComponent<MapGenerator>().GetTowerData(x, y);
            if (Tower == null  || inShop || GameManage.currentGameStatus == GameManage.GameStatus.SHOP)
            {
                return;
            }

            if (cardInfo.cardType != Card_SO.Type.TOWER || !Tower.GetComponent<TowerCardScript>().Check())
            {
                Tower.GetComponent<TowerCardScript>().CardActive(false);
                attachWith = null;
            }
            else
            {
                Tower.GetComponent<TowerCardScript>().CardActive(true);
            }
            attach = true;
        }


        // Attach With Base (endDrag)
        if (collision.gameObject.CompareTag("Base") && endDrag && cardInfo.cardType == Card_SO.Type.BASE)
        {
            attachWith = collision.gameObject;
            hit = true;
            onDeck = false;
        }
        // Attach with Base -> Green/Red Light appear
        else if (collision.gameObject.CompareTag("Base"))
        {
            if (inShop || GameManage.currentGameStatus == GameManage.GameStatus.SHOP)
            {
                return;
            }

            if(cardInfo.cardType != Card_SO.Type.BASE || !collision.gameObject.GetComponent<BaseCardScript>().Check())
            {
                collision.gameObject.GetComponent<BaseCardScript>().CardActive(false); 
                attachWith = null;
            }
            else
            {
                collision.gameObject.GetComponent<BaseCardScript>().CardActive(true);
            }
            attach = true;
        }

        // Attach With Spawner (endDrag)
        if (collision.gameObject.CompareTag("Spawner") && endDrag && cardInfo.cardType == Card_SO.Type.SPAWNER)
        {
            attachWith = collision.gameObject;
            hit = true;
            onDeck = false;
        }
        // Attach with Spawner -> Green/Red Light appear
        else if (collision.gameObject.CompareTag("Spawner"))
        {
            if (inShop || GameManage.currentGameStatus == GameManage.GameStatus.SHOP)
            {
                return;
            }

            if (cardInfo.cardType != Card_SO.Type.SPAWNER || !collision.gameObject.GetComponent<SpawnerCardScript>().Check())
            {
                collision.gameObject.GetComponent<SpawnerCardScript>().CardActive(false);
                attachWith = null;
            }
            else
            {
                collision.gameObject.GetComponent<SpawnerCardScript>().CardActive(true);
            }
            attach = true;
        }

        if (endDrag && attachWith == null)
        {
            debugText.GetComponent<TextAlert>().Alert("Mismatch card type", 2.0f);
            endDrag = false;
        }
    }

    // Green/Red Light be disappear
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (parentObject != null)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Platform") && attach)
        {
            attachWith = collision.gameObject;
            int x = (int)attachWith.transform.position.x;
            int y = -(int)attachWith.transform.position.y;

            GameObject Tower = refGameManage.GetComponent<MapGenerator>().GetTowerData(x, y);
            if (Tower == null)
            {
                return;
            }
            Tower.GetComponent<TowerCardScript>().cardInteract.SetActive(false);
            attach = false;
        }
        else if (collision.gameObject.CompareTag("Base") && attach)
        {
            attachWith = collision.gameObject;
            attachWith.GetComponent<BaseCardScript>().cardInteract.SetActive(false);
            attach = false;
        }
        else if (collision.gameObject.CompareTag("Spawner") && attach)
        {
            attachWith = collision.gameObject;
            attachWith.GetComponent<SpawnerCardScript>().cardInteract.SetActive(false);
            attach = false;
        }
    }

    void Check()
    {
        if(attachWith.CompareTag("Platform"))
        {
            int x = (int)attachWith.transform.position.x;
            int y = -(int)attachWith.transform.position.y;
            GameObject Tower = refGameManage.GetComponent<MapGenerator>().GetTowerData(x,y);
            TowerCardScript TCS = Tower.GetComponent<TowerCardScript>();
            TCS.cardInteract.SetActive(false);

            // Check that Tower card is full or not
            // Full -> return 
            // Not Full -> Abilities activate
            if (TCS.Check())
            {
                // Activate abilities
                cardInfo._abilities.ActivateAbility(Tower);
                attach = false;
                // If card was attach from deck to tower
                if (parentObject == null)
                {
                    // Add this card to that tower
                    TCS.Add(this.gameObject);
                    // If player was open UpgradeUI so showing card too 
                    if (GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE)
                    {
                        TCS.GenCard();
                    }
                    // Current Capacity increase
                    DeckController.currentCapacity--;
                }
                // If mana was not enough to transfer card from one tower to another
                else if (baseRef.GetComponent<BaseScript>().mana < 1.0f)
                {
                    debugText.GetComponent<TextAlert>().Alert("Don't have enough mana to transfer card", 2.5f);
                    // Set back to default
                    hit = false;
                    attach = false;
                    attachWith = null;
                    return;
                }
                // If mana was enough to transfer from one tower to another tower
                else
                {
                    // Decrease mana
                    baseRef.GetComponent<BaseScript>().AddMana(-1);
                    // Add card to another tower
                    TCS.Add(this.gameObject);
                    // Remove this card from this towers
                    parentObject.GetComponent<TowerCardScript>().RemoveDel(this.gameObject);
                }
                // Set to cardKeeper
                this.transform.SetParent(cardKeeper.transform);

            }
            else
            {
                debugText.GetComponent<TextAlert>().Alert("Cards in this tower was full", 2.5f);
                hit = false;
                return;
            }
        }
        else if(attachWith.CompareTag("Base"))
        {
            BaseCardScript BCS = attachWith.GetComponent<BaseCardScript>();
            BCS.cardInteract.SetActive(false);

            // Check that Base card is full or not
            // Full -> return 
            // Not Full -> Abilities activate
            if (BCS.Check())
            {
                // Activate abilities
                cardInfo._abilities.ActivateAbility(attachWith);
                attach = false;
                // If card was attach from deck to base
                if (parentObject == null)
                {
                    // Add this card to that tower
                    BCS.Add(this.gameObject);
                    // If player was open BaseUI so showing card too 
                    if (GameManage.currentGameStatus == GameManage.GameStatus.BASE)
                    {
                        BCS.GenCard();
                    }
                    // Current Capacity increase
                    DeckController.currentCapacity--;
                }
                // If mana was not enough to transfer card from one base to another
                else if (baseRef.GetComponent<BaseScript>().mana < 1.0f)
                {
                    // Set back to default
                    hit = false;
                    attach = false;
                    attachWith = null;
                    return;
                }
                // If mana was enough to transfer from one base to another base
                else
                {
                    // Decrease mana
                    baseRef.GetComponent<BaseScript>().AddMana(-1);
                    // Add card to another base
                    BCS.Add(this.gameObject);
                    // Remove this card from this base
                    parentObject.GetComponent<BaseCardScript>().RemoveDel(this.gameObject);
                }
                // Set to cardKeeper
                this.transform.SetParent(cardKeeper.transform);
            }
            else
            {
                debugText.GetComponent<TextAlert>().Alert("Cards in this base was full", 2.5f);
                hit = false;
                return;
            }
        }
        else if (attachWith.CompareTag("Spawner"))
        {
            SpawnerCardScript SCS = attachWith.GetComponent<SpawnerCardScript>();
            SCS.cardInteract.SetActive(false);

            // Check that Spawner card is full or not
            // Full -> return 
            // Not Full -> Abilities activate
            if (SCS.Check())
            {
                // Activate abilities
                cardInfo._abilities.ActivateAbility(attachWith);
                attach = false;
                // If card was attach from deck to spawner
                if (parentObject == null)
                {
                    // Add this card to that spawner
                    SCS.Add(this.gameObject);
                    // If player was open SpawnerUI so showing card too 
                    if (GameManage.currentGameStatus == GameManage.GameStatus.SPAWNER)
                    {
                        SCS.GenCard();
                    }
                    // Current Capacity increase
                    DeckController.currentCapacity--;
                }
                // If mana was not enough to transfer card from one spawner to another spawner
                else if (baseRef.GetComponent<BaseScript>().mana < 1.0f)
                {
                    debugText.GetComponent<TextAlert>().Alert("Don't have enough mana to transfer card", 2.5f);
                    // Set back to default
                    hit = false;
                    attach = false;
                    attachWith = null;
                    return;
                }
                // If mana was enough to transfer from one spawner to another spawner
                else
                {
                    // Decrease mana
                    baseRef.GetComponent<BaseScript>().AddMana(-1);
                    // Add card to another spawner
                    SCS.Add(this.gameObject);
                    // Remove this card from this spawner
                    parentObject.GetComponent<SpawnerCardScript>().RemoveDel(this.gameObject);
                }
                // Set to cardKeeper
                this.transform.SetParent(cardKeeper.transform);
            }
            else
            {
                debugText.GetComponent<TextAlert>().Alert("Cards in this spawner was full", 2.5f);
                hit = false;
                return;
            }
        }
    }

    void ShowCost()
    {
        if (GameManage.currentGameStatus == GameManage.GameStatus.SHOP)
        {
            if (inShop)
            {
                this.GetComponent<CardUI>().ShowCostBuy(cardInfo.buy.ToString());
            }
            else
            {
                this.GetComponent<CardUI>().ShowCostSell(cardInfo.sell.ToString());
            }
        }
    }

    // SET/GET value in class
    public void SetCanvas(Canvas _canvas)
    {
        canvas = _canvas;
    }

    public void SetGameManage(GameObject _object)
    {
        refGameManage = _object;
    }

    public void SetCardKeeper(GameObject _object)
    {
        cardKeeper = _object;
    }

    public void SetCardInfo(Card_SO _cardInfo)
    {
        cardInfo = _cardInfo;
    }
}
