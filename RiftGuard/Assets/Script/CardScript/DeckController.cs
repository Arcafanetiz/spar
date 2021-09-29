using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckController : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject cardStored;
    [SerializeField] private GameObject cardList;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            GenCard();
        }
    }

    private void GenCard()
    {
        GameObject card = Instantiate(cardPrefab, transform);
        card.transform.SetParent(card.transform.parent.gameObject.transform);

        int index = Random.Range(0, cardList.GetComponent<CardList>().cardList.Count);

        card.GetComponent<DragDrop>().SetCardInfo(cardList.GetComponent<CardList>().cardList[index]);
        card.GetComponent<DragDrop>().SetCanvas(card.transform.parent.parent.gameObject.GetComponent<Canvas>());
        card.GetComponent<DragDrop>().SetGameManage(gameManager);
        card.GetComponent<DragDrop>().SetCardKeeper(cardStored);
    }

    private void GenCard(Card_SO _card)
    {
        GameObject card = Instantiate(cardPrefab, transform);
        card.transform.SetParent(card.transform.parent.gameObject.transform);

        card.GetComponent<DragDrop>().SetCardInfo(_card);
        card.GetComponent<DragDrop>().SetCanvas(card.transform.parent.parent.gameObject.GetComponent<Canvas>());
        card.GetComponent<DragDrop>().SetGameManage(gameManager);
        card.GetComponent<DragDrop>().SetCardKeeper(cardStored);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.gameObject);
        if(!eventData.pointerDrag.gameObject.GetComponent<DragDrop>().onDeck)
        {
            GameObject _card = eventData.pointerDrag.gameObject;
            DragDrop _DG = _card.GetComponent<DragDrop>();
            if(_DG.attachWith.CompareTag("Platform"))
            {
                GameObject towerRef = gameManager.GetComponent<MapGenerator>().GetTowerData((int)_DG.attachWith.gameObject.transform.position.x, -(int)_DG.attachWith.gameObject.transform.position.y);
                towerRef.GetComponent<TowerCardScript>().Remove(_card);
            }
            else if(_DG.attachWith.CompareTag("Base"))
            {
                _DG.attachWith.GetComponent<BaseCardScript>().Remove(_card);
            }
            else if(_DG.attachWith.CompareTag("Spawner"))
            {
                _DG.attachWith.GetComponent<SpawnerCardScript>().Remove(_card);
            }
            GenCard(_card.GetComponent<DragDrop>().cardInfo);
            Destroy(_card);
        }
    }
}
