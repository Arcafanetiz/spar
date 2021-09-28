using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
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
}
