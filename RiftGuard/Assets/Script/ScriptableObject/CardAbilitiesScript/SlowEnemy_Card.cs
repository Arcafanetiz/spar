using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Spawner/Slowdown")]
public class SlowEnemy_Card : CardAbilities
{
    [SerializeField] float _slow;

    public override void ActivateAbility(GameObject _spawner)
    {
        _spawner.GetComponent<SpawnerControl>().speedRate += _slow;
    }
    public override void DeactivateAbility(GameObject _spawner)
    {
        _spawner.GetComponent<SpawnerControl>().speedRate -= _slow;
    }
}
