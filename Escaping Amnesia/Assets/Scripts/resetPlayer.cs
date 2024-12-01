using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPlayer : MonoBehaviour
{
    public GameObject playerHealth;
    public GameObject player;
    public Transform resetPosition;
    public GameObject playerCoin;

    // Start is called before the first frame update
    void Start()
    {
        // Resets player Health
        playerHealth = GameObject.Find("Player Health");
        playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth = playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth;

        // Resets player Position
        player.transform.position = resetPosition.position;

        // Resets palyer Coins
        playerHealth = GameObject.Find("Player Coins");
        playerCoin.GetComponent<playerCoinCounter>().currentCoinCount = 10;
    }
}