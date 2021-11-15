using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppear : MonoBehaviour
{
    BossUIController BossUI;

    private void Start()
    {
        BossUI = GameObject.FindObjectOfType<BossUIController>();
        BossUI.ActivateBossUI();
    }

    private void Update()
    {
        BossUI.UpdateArmor(this.GetComponent<EnemyControl>().currentArmor, this.GetComponent<EnemyControl>().Armor);
        BossUI.UpdateHealth(this.GetComponent<EnemyControl>().currentHP, this.GetComponent<EnemyControl>().HP);
    }

    private void OnDestroy()
    {
        BossUI.DeactivateBossUI();
    }
}
