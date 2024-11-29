using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPlayerHealth : MonoBehaviour
{
    public GameObject playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player Health");

        playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth = playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth;
    }
}
