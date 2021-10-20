using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Cards/Abilities/Tower/ATK")]
public class ATK_Card : CardAbilities
{
    [SerializeField] float _damage;

    public override void ActivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().ATK += (_tower.GetComponent<TowerControl>().Base_ATK * (_damage / 100));
    }

    public override void DeactivateAbility(GameObject _tower)
    {
        _tower.GetComponent<TowerControl>().ATK -= (_tower.GetComponent<TowerControl>().Base_ATK * (_damage / 100));
    }
}
