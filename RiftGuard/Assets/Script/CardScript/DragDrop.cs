using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject refGameManage;
    private GameObject cardKeeper;
    public Card_SO cardInfo;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform parentToReturnTo = null;

    private GameObject attachWith;

    private bool endDrag = false;
    private bool hit = false;
    private bool doOnce = false;
    private bool drag = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void FixedUpdate()
    {
        if(hit)
        {
            Check();
        }
        else if(doOnce && !drag)
        {
            this.transform.SetParent(parentToReturnTo);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        endDrag = false;
        doOnce = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        drag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        endDrag = true;
        drag = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Collision Only Tile
        // Check if That grid have tower and EndDrag
        if (refGameManage.GetComponent<MapGenerator>().CheckMap((int)(collision.transform.position.x), (int)(-collision.transform.position.y))
            && collision.gameObject.CompareTag("Platform") && endDrag && cardInfo.cardType == Card_SO.Type.TOWER)
        {
            attachWith = collision.gameObject;
            hit = true;
        }

        if (collision.gameObject.CompareTag("Base") && endDrag && cardInfo.cardType == Card_SO.Type.BASE)
        {
            attachWith = collision.gameObject;
            hit = true;
        }

        if (collision.gameObject.CompareTag("Spawner") && endDrag && cardInfo.cardType == Card_SO.Type.SPAWNER)
        {
            attachWith = collision.gameObject;
            hit = true;
        }
    }

    void Check()
    {
        if(attachWith.CompareTag("Platform"))
        {
            print("Tower");
            int x = (int)attachWith.transform.position.x;
            int y = -(int)attachWith.transform.position.y;
            GameObject Tower = refGameManage.GetComponent<MapGenerator>().GetTowerData(x,y);
            cardInfo._abilities.ActivateAbility(Tower);
            TowerCardScript TCS = Tower.GetComponent<TowerCardScript>();
            if (TCS.Check())
            {
                TCS.Add(this.gameObject);
                TCS.GenCard();
                this.transform.SetParent(cardKeeper.transform);

            }
            else
            {
                hit = false;
                return;
            }
        }
        else if(attachWith.CompareTag("Base"))
        {
            print("Base");
            BaseCardScript BCS = attachWith.GetComponent<BaseCardScript>();
            if(BCS.Check())
            {
                BCS.Add(this.gameObject);
                BCS.GenCard();
                this.transform.SetParent(cardKeeper.transform);

            }
            else
            {
                hit = false;
                return;
            }
        }
        else if (attachWith.CompareTag("Spawner"))
        {
            print("Spawner");
            SpawnerCardScript SCS = attachWith.GetComponent<SpawnerCardScript>();
            if (SCS.Check())
            {
                SCS.Add(this.gameObject);
                SCS.GenCard();
                this.transform.SetParent(cardKeeper.transform);

            }
            else
            {
                hit = false;
                return;
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
