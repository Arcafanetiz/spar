using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardAbilities : ScriptableObject
{
    public abstract void ActivateAbility(GameObject _obj);
    public abstract void DeactivateAbility(GameObject _obj);
}
