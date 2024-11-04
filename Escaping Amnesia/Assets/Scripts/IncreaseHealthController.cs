using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class IncreaseHealthController : MonoBehaviour
{
    // Increase Player Health
    public bool selectIncreaseHealth;
    public GameObject playerHealth;

    public void increaseHealth()
    {
        if(!selectIncreaseHealth)
        {
            selectIncreaseHealth = true;
            playerHealth = GameObject.Find("Player Health");
            playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth += 5;
            if(playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth > playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth)
            {
                playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth = playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth;
            }
            UnityEngine.Debug.Log("Select Health");
        }
    }
}
