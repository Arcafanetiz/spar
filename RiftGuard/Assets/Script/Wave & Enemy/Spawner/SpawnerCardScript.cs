using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCardScript : MonoBehaviour
{
    [SerializeField] private int _cardCapacity;
    [HideInInspector] public GameObject gameManager;
    [HideInInspector] public GameObject objectCardUI;
    [HideInInspector] public GameObject baseRef;


    private int _currentCardCapacity;
    private List<GameObject> card;
    private bool[] cardCheck;

    public GameObject cardInteract; // Can drag -> Green Color / Can't drag Red Color

    private bool doOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        card = new List<GameObject>();
        cardCheck = new bool[_cardCapacity];
    }

    // Update is called once per frame
    void Update()
    {
        if (doOnce && GameManage.currentGameStatus == GameManage.GameStatus.SPAWNER)
        {
            GenCard();
            doOnce = false;
        }

        if (GameManage.currentGameStatus == GameManage.GameStatus.PLAY)
        {
            ClearCardUI();
        }
    }

    public void GenCard()
    {
        for (int j = 0; j < card.Count; j++)
        {
            if (cardCheck[j])
            {
                continue;
            }
            GameObject cardGen = Instantiate(card[j], objectCardUI.transform);
            cardGen.gameObject.name = "(" + this.gameObject.name + ")#" + j;
            cardGen.GetComponent<DragDrop>().parentCard = card[j];
            cardGen.transform.localScale = new Vector2(0.75f, 0.75f);
            cardGen.transform.SetParent(objectCardUI.transform);
            cardGen.GetComponent<DragDrop>().parentObject = this.gameObject;
            cardGen.GetComponent<DragDrop>().cardKeeper = card[j].GetComponent<DragDrop>().cardKeeper;
            cardGen.GetComponent<DragDrop>().SetCanvas(objectCardUI.transform.parent.gameObject.GetComponent<Canvas>());
            cardGen.GetComponent<DragDrop>().SetGameManage(gameManager);
            cardGen.GetComponent<DragDrop>().baseRef = baseRef;

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

    public void DelAllCard()
    {
        foreach (GameObject _object in card)
        {
            Destroy(_object.gameObject);
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

    public void SetActivate()
    {
        doOnce = true;
    }
    public void RemoveDel(GameObject _card)
    {
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
