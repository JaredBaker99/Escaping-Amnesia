using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDamageController : MonoBehaviour
{
    public GameObject upgradeDamageCanvas; // Assign the card shop canvas in the Inspector
    public GameObject UpgradeOptions;

    public void upgradeShop()
    {
        UnityEngine.Debug.Log("Upgrade Health");
        upgradeDamageCanvas.SetActive(true);
    }
}
