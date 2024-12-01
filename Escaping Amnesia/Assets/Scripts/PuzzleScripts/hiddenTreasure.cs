using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class hiddenTreasure : MonoBehaviour
{
    public GameObject treasure;
    public GameObject wallet ;
    public int coinLow ;
    public int coinHigh ;
    public bool grabbed ;

    void Start()
    {
        grabbed  = false ;
        wallet = GameObject.Find("Player Coins") ;
    }

    public void reveal() {
        treasure.SetActive(true) ;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player") && !grabbed) {
            grabbed = true ;
            if(wallet != null) { 
                wallet.GetComponent<playerCoinCounter>().currentCoinCount += Random.Range(coinLow, coinHigh) ;
            }
            treasure.SetActive(false) ;
            //update coin count
        }
    }
}

