using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Spawner/Delay Enemy")]
public class DelayEnemy : CardAbilities
{
    [SerializeField] float _slow;

    public override void ActivateAbility(GameObject _spawner)
    {
        _spawner.GetComponent<SpawnerControl>().slowRate += _slow;
    }
    public override void DeactivateAbility(GameObject _spawner)
    {
        _spawner.GetComponent<SpawnerControl>().slowRate -= _slow;
    }
}
