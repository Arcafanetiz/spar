using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Tower/Piercing")]
public class Piercing_Card : CardAbilities
{
    public override void ActivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().piercing = true;
    }

    public override void DeactivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().piercing = false;
    }
}
