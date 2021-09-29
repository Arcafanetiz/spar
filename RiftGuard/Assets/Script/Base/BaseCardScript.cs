using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardScript : MonoBehaviour
{
    [SerializeField] private int _cardCapacity;
    [SerializeField] private GameObject gameManager;
    public GameObject objectCardUI;
    private int _currentCardCapacity;
    private List<GameObject> card;
    private bool[] cardCheck;

    private bool doOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        card = new List<GameObject>();
        cardCheck = new bool[_cardCapacity];
    }

    // Update is called once per frame
    void Update()
    {
        if (doOnce && GameManage.currentGameStatus == GameManage.GameStatus.BASE)
        {
            GenCard();
            doOnce = false;
        }

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
            if(cardCheck[j])
            {
                continue;
            }
            GameObject cardGen = Instantiate(card[j], objectCardUI.transform);
            cardGen.GetComponent<DragDrop>().parentRef = card[j];
            cardGen.transform.localScale = new Vector2(0.75f, 0.75f);
            cardGen.transform.SetParent(objectCardUI.transform);
            cardGen.GetComponent<DragDrop>().SetCanvas(objectCardUI.transform.parent.gameObject.GetComponent<Canvas>());
            cardGen.GetComponent<DragDrop>().SetGameManage(gameManager);
            cardCheck[j] = true;
        }
    }

    public void ClearCardUI()
    {
        foreach (Transform child in objectCardUI.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int j = 0; j < card.Count; j++)
        {
            cardCheck[j] = false;
        }
    }

    public void Add(GameObject _object)
    {
        _currentCardCapacity++;
        card.Add(_object);
    }

    public bool Check()
    {
        return (_currentCardCapacity < _cardCapacity) ? true : false;
    }
    public void Remove(GameObject _card)
    {
        GameObject temp = _card.GetComponent<DragDrop>().parentRef;
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
}
