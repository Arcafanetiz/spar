using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCardScript : MonoBehaviour
{
    [SerializeField] private int _cardCapacity;
    public GameObject gameManager;
    public GameObject objectCardUI;
    private int _currentCardCapacity;
    private List<GameObject> card;

    private bool doOnce = false;

    // Start is called before the first frame update
    void Awake()
    {
        card = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doOnce && GameManage.currentGameStatus == GameManage.GameStatus.UPGRADE)
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
            GameObject cardGen = Instantiate(card[j], objectCardUI.transform);
            cardGen.GetComponent<DragDrop>().parentRef = card[j];
            cardGen.transform.localScale = new Vector2(0.75f, 0.75f);
            cardGen.transform.SetParent(objectCardUI.transform);
            cardGen.GetComponent<DragDrop>().SetCanvas(objectCardUI.transform.parent.gameObject.GetComponent<Canvas>());
            cardGen.GetComponent<DragDrop>().SetGameManage(gameManager);
        }
    }

    public void ClearCardUI()
    {
        foreach (Transform child in objectCardUI.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void DelAllCard()
    {
        foreach (GameObject _object in card)
        {
            Destroy(_object.gameObject);
        }

    }

    public void CardActivate()
    {
        foreach (GameObject _object in card)
        {
            _object.GetComponent<DragDrop>().cardInfo._abilities.ActivateAbility(this.gameObject);
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

    public List<GameObject> GetListCard()
    {
        return card;
    }
    public void SetListCard(List<GameObject> _card)
    {
        card = _card;
    }
    public void Remove(GameObject _card)
    {
        print("HELLO");
        GameObject temp = _card.GetComponent<DragDrop>().parentRef;
        card.Remove(temp);
        temp.GetComponent<DragDrop>().cardInfo._abilities.DeactivateAbility(this.gameObject);
        Destroy(temp);
    }
}
