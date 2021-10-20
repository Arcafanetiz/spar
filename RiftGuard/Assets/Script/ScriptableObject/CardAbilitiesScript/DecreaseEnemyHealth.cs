using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Abilities/Spawner/Decrease Enemy Health")] // Get more money when kill enemy
public class DecreaseEnemyHealth : CardAbilities
{
    [SerializeField] float _health;

    public override void ActivateAbility(GameObject _spawner)
    {
        _spawner.GetComponent<SpawnerControl>().healthRate += _health;
    }
    public override void DeactivateAbility(GameObject _spawner)
    {
        _spawner.GetComponent<SpawnerControl>().healthRate -= _health;
    }
}
