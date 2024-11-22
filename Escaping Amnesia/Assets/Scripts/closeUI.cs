using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeUI : MonoBehaviour
{
    public GameObject CardShop;      // Reference to the CardShop GameObject (the shop UI)
    public GameObject UpgradeOptions; // Reference to the UpgradeOptions GameObject

    public void CloseShop()
    {
        // Disable the CardShop UI (hides the shop)
        CardShop.SetActive(false);

        // Show the UpgradeOptions UI (enables the other upgrade options)
        UpgradeOptions.SetActive(true);
    }
}
