using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUDManager : MonoBehaviour
{
    // Get our HP from our GameManager
    public GameManager gameManager;
    public Enemy enemy;

    public TMP_Text playerHP;
    
    public TMP_Text playerEnergy;

    public TMP_Text enemyHP;

    public TMP_Text enemyEnergy;
    
    void Update()
    {
        UpdateHUDManager();
    }

    public void UpdateHUDManager()
    {
        playerHP.text = gameManager.PlayerHealth.ToString();
        playerEnergy.text  = gameManager.currentEnergy.ToString();

        enemyHP.text = enemy.EnemyHealth.ToString();
        enemyEnergy.text = enemy.enemyCurrentEnergy.ToString();
    }

    public void StartHUD()
    {
        playerHP.text = gameManager.PlayerHealth.ToString();
        playerEnergy.text  = gameManager.currentEnergy.ToString();

        enemyHP.text = enemy.EnemyHealth.ToString();
        enemyEnergy.text = enemy.startingEnergy.ToString();
    }

}
