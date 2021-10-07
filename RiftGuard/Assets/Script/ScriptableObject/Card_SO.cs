using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Card")]
public class Card_SO : ScriptableObject
{
    public enum Type
    {
        TOWER,
        SPAWNER,
        BASE
    }

    public enum Ability
    {
        SPD_200,
        ATK_200,
        RNG_200
    }

    public string cardName;
    public Type cardType;


    [TextArea]
    public string description;

    public Sprite image;

    public CardAbilities _abilities;

    public int buy;

    public int sell;

}
