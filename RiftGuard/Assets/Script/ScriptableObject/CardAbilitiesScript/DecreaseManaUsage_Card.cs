using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Base/Decrease Mana Usage")] // Decrease mana usage when drag card out or create tower
public class DecreaseManaUsage_Card : CardAbilities
{
    [SerializeField] float _manaUsage;

    public override void ActivateAbility(GameObject _base)
    {
        _base.GetComponent<BaseScript>().manaUsage -= _manaUsage;
    }
    public override void DeactivateAbility(GameObject _base)
    {
        _base.GetComponent<BaseScript>().manaUsage += _manaUsage;
    }
}
