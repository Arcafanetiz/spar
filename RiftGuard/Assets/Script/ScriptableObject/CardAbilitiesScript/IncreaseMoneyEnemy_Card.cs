using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Base/Increase Enemy Money")] // Get more money when kill enemy
public class IncreaseMoneyEnemy_Card : CardAbilities
{
    [SerializeField] float _rate;

    public override void ActivateAbility(GameObject _base)
    {
        _base.GetComponent<BaseScript>().enemyKillRate += _rate;
    }
    public override void DeactivateAbility(GameObject _base)
    {
        _base.GetComponent<BaseScript>().enemyKillRate -= _rate;
    }
}
