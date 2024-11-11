using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShopController : MonoBehaviour
{
    public GameObject cardShopCanvas; // Assign the card shop canvas in the Inspector
    public bool selectCardShop;

    public void cardShop()
    {
        if (!selectCardShop)
        {
            selectCardShop = true;
            UnityEngine.Debug.Log("Card Shop");

            // Show the card shop canvas
            cardShopCanvas.SetActive(true);
        }
    }
}
