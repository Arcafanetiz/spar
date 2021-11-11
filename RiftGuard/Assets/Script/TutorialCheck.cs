using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheck : MonoBehaviour
{
    [SerializeField] GameObject Help;
    [SerializeField] GameObject Deck;

    [SerializeField] Text task1;
    [SerializeField] Text task2;
    [SerializeField] Text task3;

    [SerializeField] GameObject Tips;

    bool CheckTask1;
    float timeForPress = 1.25f;
    float currentTime;
    bool CheckTask2;
    bool CheckTask3;
    int cardTask3;

    bool checkOnce = true;
    bool doOnce = true;

    private void Update()
    {
        if(GameManage.currentGameStatus != GameManage.GameStatus.PAUSE &&
           GameManage.currentGameStatus != GameManage.GameStatus.TUTORIAL_PAUSE)
        {
            Help.SetActive(false);

            CheckTips();
            if (CheckTask1)
            {
                task1.GetComponent<Text>().color = Color.green;
            }
            if(CheckTask2)
            {
                task2.GetComponent<Text>().color = Color.green;
            }
            if (CheckTask3)
            {
                task3.GetComponent<Text>().color = Color.green;
            }
            Tutorial();
            GenTask3();
        }

    }

    private void Tutorial()
    {
        if (!CheckTask1)
        {
            if (currentTime >= timeForPress)
            {
                CheckTask1 = true;
            }
            else if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            {
                currentTime += Time.deltaTime;
            }
        }
        if(!CheckTask2)
        {
            if (GameObject.FindGameObjectWithTag("Tower") != null)
            {
                CheckTask2 = true;
            }
        }
        if (!CheckTask3 && (CheckTask1 && CheckTask2) && !doOnce)
        {
            if(cardTask3 != Deck.GetComponent<DeckController>().currentCard())
            {
                CheckTask3 = true;
            }
        }
    }
    private void GenTask3()
    {
        if (CheckTask1 && CheckTask2 && doOnce)
        {
            doOnce = false;
            Deck.GetComponent<DeckController>().GenCard();
            Deck.GetComponent<DeckController>().GenCard();
            Deck.GetComponent<DeckController>().GenCard();
            cardTask3 = Deck.GetComponent<DeckController>().currentCard();
        }
    }

    private void CheckTips()
    {
        if (CheckTask1 && CheckTask2 && CheckTask3 && checkOnce)
        {
            checkOnce = false;
            Tips.SetActive(true);
            GameManage.currentGameStatus = GameManage.GameStatus.TUTORIAL_PAUSE;
        }
    }
}
