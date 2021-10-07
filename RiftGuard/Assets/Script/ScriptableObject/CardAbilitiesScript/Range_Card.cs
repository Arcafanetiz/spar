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
        _tower.GetComponent<TowerControl>().DrawCircle(_tower.GetComponent<TowerControl>().Range, 0.05f);
    }

    public override void DeactivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().Range /= (_range / 100);
        _tower.GetComponent<TowerControl>().DrawCircle(_tower.GetComponent<TowerControl>().Range, 0.05f);
    }
}
