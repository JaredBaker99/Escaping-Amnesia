using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUDManager : MonoBehaviour
{
    // Get our HP from our GameManager
    public GameManager gameManager;

    // public MainManager mainManager;
    
    public playerHealthTracker playerhealthTracker;

    // public int maxPlayerHealth;
    // public int currentPlayerHealth;

    public Enemy enemy;

    public TMP_Text playerHP;
    
    public TMP_Text playerEnergy;

    public TMP_Text enemyHP;

    public TMP_Text enemyEnergy;

    void Awake()
    {
        //playerhealthTracker = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        UpdateHUDManager();
    }
    

    public void UpdateHUDManager()
    {
        // mainManager = FindObjectOfType<MainManager>();
        playerhealthTracker = FindObjectOfType<playerHealthTracker>();
        
        // This is the old way to get HP for testing in battle area purposes
        // playerHP.text = gameManager.PlayerHealth.ToString();
        // This one below is grabing from the PlayerHealthTracker Script
        playerHP.text = playerhealthTracker.currentPlayerHealth.ToString(); //.GetComponent<playerHealthTracker>()
        playerEnergy.text = gameManager.currentEnergy.ToString();

        enemyHP.text = enemy.EnemyHealth.ToString();
        enemyEnergy.text = enemy.enemyCurrentEnergy.ToString();
    }

    public void StartHUD()
    {
        playerhealthTracker = FindObjectOfType<playerHealthTracker>();
        // mainManager = FindObjectOfType<MainManager>();
        // This is the old way to get HP for testing in battle area purposes
        // playerHP.text = gameManager.PlayerHealth.ToString();
        // This one below is grabing from the PlayerHealthTracker Script
        playerHP.text = playerhealthTracker.currentPlayerHealth.ToString(); // .GetComponent<playerHealthTracker>()
        playerEnergy.text  = gameManager.currentEnergy.ToString();

        enemyHP.text = enemy.EnemyHealth.ToString();
        enemyEnergy.text = enemy.startingEnergy.ToString();
    }

}
