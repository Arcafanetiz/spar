using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Tower/Range")]
public class Range_Card : CardAbilities
{
    [SerializeField] float _range;

    public override void ActivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().Range *= (_range/100);
        _tower.GetComponent<TowerControl>().UpdateRangeArea();
    }

    public override void DeactivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().Range /= (_range / 100);
        _tower.GetComponent<TowerControl>().UpdateRangeArea();
    }
}
