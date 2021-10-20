using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Spawner/Decrease Enemy Defense")]
public class DecreaseEnemyDefense_Card : CardAbilities
{
    [SerializeField] float _defRate;

    public override void ActivateAbility(GameObject _spawner)
    {
        _spawner.GetComponent<SpawnerControl>().defRate += _defRate;
    }
    public override void DeactivateAbility(GameObject _spawner)
    {
        _spawner.GetComponent<SpawnerControl>().defRate -= _defRate;
    }
}
