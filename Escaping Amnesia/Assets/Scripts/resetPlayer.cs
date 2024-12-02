using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPlayer : MonoBehaviour
{
    public GameObject playerHealth;
    public GameObject player;
    public GameObject sceneCounter ;
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

        sceneCounter = GameObject.Find("Scene Counter");
        sceneCounter.GetComponent<SceneCounter>().reset() ;

        // Resets palyer Coins
        playerCoin = GameObject.Find("Player Coins");
        playerCoin.GetComponent<playerCoinCounter>().currentCoinCount = 0;
    }
}