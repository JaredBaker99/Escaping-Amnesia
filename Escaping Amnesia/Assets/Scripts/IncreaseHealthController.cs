using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class IncreaseHealthController : MonoBehaviour
{
    public GameObject UpgradeOptions;
    public GameObject playerHealth;
    public GameObject coinCount;

    public void increaseHealth()
    {
        UpgradeOptions.SetActive(false);
        coinCount = GameObject.Find("Player Coins");
        playerHealth = GameObject.Find("Player Health");

        while(coinCount.GetComponent<playerCoinCounter>().currentCoinCount >= 0 && (playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth < playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth))
        {
            coinCount.GetComponent<playerCoinCounter>().currentCoinCount -= 1;
            playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth += 5;

            if(playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth > playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth)
            {
                playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth = playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth;
            }
        }

        UnityEngine.Debug.Log("Select Health");
    }
}
