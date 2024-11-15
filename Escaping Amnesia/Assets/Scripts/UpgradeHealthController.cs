using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealthController : MonoBehaviour
{
    public GameObject upgradeHealthCanvas; // Assign the card shop canvas in the Inspector
    public GameObject UpgradeOptions;

    public void upgradeShop()
    {
        UnityEngine.Debug.Log("Upgrade Health");
        upgradeHealthCanvas.SetActive(true);
    }
}
