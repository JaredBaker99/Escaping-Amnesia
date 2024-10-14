using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOptionsController : MonoBehaviour
{
    // References to each upgrade controller
    public UpgradeHealthController upgradeHealthController;
    public IncreaseHealthController increaseHealthController;
    public CardShopController cardShopController;
    public UpgradeDamageController upgradeDamageController;

    void Update()
    {
        // Check if any upgrade has been selected
        if (upgradeHealthController.selectUpgradeHealth ||
            increaseHealthController.selectIncreaseHealth ||
            cardShopController.selectCardShop ||
            upgradeDamageController.selectUpgradeDamage)
        {
            // Hide all upgrade options (disable the parent object)
            gameObject.SetActive(false);
        }
    }
}
