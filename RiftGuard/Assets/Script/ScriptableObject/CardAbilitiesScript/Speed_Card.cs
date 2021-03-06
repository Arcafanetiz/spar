using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Cards/Abilities/Tower/SPD")]
public class Speed_Card : CardAbilities
{
    [SerializeField] float _speed;

    public override void ActivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().Cooldown -= (_tower.GetComponent<TowerControl>().Base_Cooldown * (_speed / 100));
    }
    public override void DeactivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().Cooldown += (_tower.GetComponent<TowerControl>().Base_Cooldown * (_speed / 100));
    }
}
