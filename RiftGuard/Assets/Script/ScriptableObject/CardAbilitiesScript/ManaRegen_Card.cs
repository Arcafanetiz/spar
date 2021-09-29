using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Base/ManaRegen")]
public class ManaRegen_Card : CardAbilities
{
    [SerializeField] float _regenRate;

    public override void ActivateAbility(GameObject _base)
    {
        _base.GetComponent<BaseScript>().manaRegen *= (_regenRate / 100);
    }

    public override void DeactivateAbility(GameObject _base)
    {
        _base.GetComponent<BaseScript>().manaRegen /= (_regenRate / 100);
    }
}
