using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Cards/Abilities/Tower/All_Stat")]
public class IncreaseAllStat_Card : CardAbilities
{
    [SerializeField] float _atk;
    [SerializeField] float _range;
    [SerializeField] float _speed;

    public override void ActivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().ATK += (_tower.GetComponent<TowerControl>().Base_ATK * (_atk / 100));

        _tower.GetComponent<TowerControl>().Range += (_tower.GetComponent<TowerControl>().Base_Range * (_range / 100));
        _tower.GetComponent<TowerControl>().DrawCircle(_tower.GetComponent<TowerControl>().Range, 0.05f);

        _tower.GetComponent<TowerControl>().Cooldown -= (_tower.GetComponent<TowerControl>().Base_Cooldown * (_speed / 100));
    }

    public override void DeactivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().ATK -= (_tower.GetComponent<TowerControl>().Base_ATK * (_atk / 100));

        _tower.GetComponent<TowerControl>().Range -= (_tower.GetComponent<TowerControl>().Base_Range * (_range / 100));
        _tower.GetComponent<TowerControl>().DrawCircle(_tower.GetComponent<TowerControl>().Range, 0.05f);

        _tower.GetComponent<TowerControl>().Cooldown += (_tower.GetComponent<TowerControl>().Base_Cooldown * (_speed / 100));
    }
}
