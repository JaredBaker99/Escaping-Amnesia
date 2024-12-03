using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUIScript : MonoBehaviour
{
    public TMP_Text coinText;
    public GameObject coinCount;

    void Start()
    {
        coinText = GameObject.Find("CoinCounter").GetComponent<TMP_Text>();
        coinCount = GameObject.Find("Player Coins");
    }

    void Update()
    {
        if(coinCount != null) 
        {
            coinText.text = coinCount.GetComponent<playerCoinCounter>().currentCoinCount.ToString();
        }
    }
}
