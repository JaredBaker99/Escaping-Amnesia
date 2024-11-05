using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthBarSlider;

    public GameObject playerHealth;

    public void SetMaxHealth(int maxHealth){
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
    }

    public void Start()
    {
        playerHealth = GameObject.Find("Player Health");        
    }

    public void Update()
    {
        if (playerHealth != null)
        {
            SetMaxHealth(playerHealth.GetComponent<playerHealthTracker>().maxPlayerHealth);
            SetHealth(playerHealth.GetComponent<playerHealthTracker>().currentPlayerHealth);
        }
        else
        {
            playerHealth = GameObject.Find("Player Health");
        }
    }


}
