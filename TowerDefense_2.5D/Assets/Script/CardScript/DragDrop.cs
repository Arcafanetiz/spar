using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject refGameManage;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform parentToReturnTo = null;

    private bool endDrag = false;
    private bool hit = false;
    private bool doOnce = false;
    private bool drag = false;
    private int frame = 0;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void FixedUpdate()
    {
        if(hit)
        {
            Destroy(this.gameObject);
        }
        else if(doOnce && !drag && frame > 1)
        {
            frame = 0;
            this.transform.SetParent(parentToReturnTo);
        }
        else if(doOnce && !drag)
        {
            frame++;
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
            && collision.gameObject.CompareTag("Platform") 
            && endDrag)
        {
            hit = true;
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
}
