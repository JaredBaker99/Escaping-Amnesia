using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class IncreaseHealthController : MonoBehaviour
{
    public bool selectIncreaseHealth;

    public void increaseHealth()
    {
        if(!selectIncreaseHealth)
        {
            selectIncreaseHealth = true;
            UnityEngine.Debug.Log("Select Health");
        }
    }
}
