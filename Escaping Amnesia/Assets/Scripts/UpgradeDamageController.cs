using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDamageController : MonoBehaviour
{
    public bool selectUpgradeDamage;

    public void upgradeDamage()
    {
        if (!selectUpgradeDamage)
        {
            selectUpgradeDamage = true;
            UnityEngine.Debug.Log("Select Upgrade Damage");
        }
    }
}
