using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Base/Money Regeneration")]
public class MoneyRegen_Card : CardAbilities
{
    [SerializeField] float _moneyRegen;

    public override void ActivateAbility(GameObject _base)
    {
        _base.GetComponent<BaseScript>().moneyRegen += _moneyRegen;
    }
    public override void DeactivateAbility(GameObject _base)
    {
        _base.GetComponent<BaseScript>().moneyRegen -= _moneyRegen;
    }
}
