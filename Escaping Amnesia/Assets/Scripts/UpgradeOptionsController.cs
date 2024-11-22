using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOptionsController : MonoBehaviour
{
    public bool selectUpgrade = false;

    void Update()
    {
        if (selectUpgrade)
        {
            gameObject.SetActive(false);
        }
    }
}
