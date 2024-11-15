using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShopController : MonoBehaviour
{
    public GameObject cardShopCanvas; // Assign the card shop canvas in the Inspector
    public GameObject UpgradeOptions;

    public void cardShop()
    {
        UnityEngine.Debug.Log("Card Shop");
        cardShopCanvas.SetActive(true);
    }
}
