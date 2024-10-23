using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShopController : MonoBehaviour
{
    public bool selectCardShop;

    public void cardShop()
    {
        if (!selectCardShop)
        {
            selectCardShop = true;
            UnityEngine.Debug.Log("Card Shop");
        }
    }
}
