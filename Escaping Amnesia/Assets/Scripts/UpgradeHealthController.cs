using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealthController : MonoBehaviour
{
    public bool selectUpgradeHealth;

    public void upgradeHealth()
    {
        if (!selectUpgradeHealth)
        {
            selectUpgradeHealth = true;
            UnityEngine.Debug.Log("Select Upgrade Health");
        }
    }
}
