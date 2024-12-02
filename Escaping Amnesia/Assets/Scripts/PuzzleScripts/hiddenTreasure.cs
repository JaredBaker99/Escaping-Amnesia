using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class hiddenTreasure : MonoBehaviour
{
    public GameObject treasure;
    public GameObject medkit;
    public GameObject obstacle ;
    public Collider2D me ;
    public GameObject wallet ;
    public GameObject hp; 
    public int coinLow ;
    public int coinHigh ;
    public bool grabbed ;
    public bool reveal ;
    public bool coins ;
    public bool health ;
    private int playerHealth ;
    private int maxPlayerHealth; 
    private int rand ;

    void Start()
    {
        playerHealth = 20 ;
        maxPlayerHealth = 20 ;
        grabbed  = false ;
        coins = false ;
        health = false ;
        me.enabled = false ;
        wallet = GameObject.Find("Player Coins") ;
        hp = GameObject.Find("Player Health") ;
        reveal = false ;
        treasure.SetActive(false) ;
        medkit.SetActive(false) ;
        if(hp != null) {
            playerHealth = hp.GetComponent<playerHealthTracker>().currentPlayerHealth ;
            maxPlayerHealth = hp.GetComponent<playerHealthTracker>().maxPlayerHealth ;
        }
        
    }

    public void revealSecret(string type) {
        if(type == "treasure") {
            me.enabled = true ;
            rand = Random.Range(0, maxPlayerHealth) ;
            if(rand > playerHealth) {
                health = true ;
                medkit.SetActive(true) ;
            }
            else {
                coins = true ;
                treasure.SetActive(true) ;
            }
        }
        else if(type == "door") {
            obstacle.SetActive(false) ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player") && !grabbed) {
            grabbed = true ;
            
            if(health) {
                if(hp != null) {
                    hp.GetComponent<playerHealthTracker>().currentPlayerHealth += 5;
                    if(hp.GetComponent<playerHealthTracker>().currentPlayerHealth > hp.GetComponent<playerHealthTracker>().maxPlayerHealth) {
                        hp.GetComponent<playerHealthTracker>().currentPlayerHealth = hp.GetComponent<playerHealthTracker>().maxPlayerHealth ;
                    }
                }
            }
            if(coins) {
                if(wallet != null) { 
                    wallet.GetComponent<playerCoinCounter>().currentCoinCount += Random.Range(coinLow, coinHigh) ;
                }
            }
            medkit.SetActive(false) ;
            treasure.SetActive(false) ;
            //update coin count
        }
    }
}

