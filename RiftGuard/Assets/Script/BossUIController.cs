using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIController : MonoBehaviour
{

    [SerializeField] private GameObject BossUI;
    [SerializeField] private GameObject HealthUI;
    [SerializeField] private GameObject ArmorUI;

    public void ActivateBossUI()
    {
        BossUI.SetActive(true);
    }

    public void DeactivateBossUI()
    {
        BossUI.SetActive(false);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        HealthUI.GetComponent<Image>().fillAmount = (currentHealth / maxHealth);
    }
    public void UpdateArmor(float currentArmor, float maxArmor)
    {
        ArmorUI.GetComponent<Image>().fillAmount = (currentArmor / maxArmor);
    }
}
