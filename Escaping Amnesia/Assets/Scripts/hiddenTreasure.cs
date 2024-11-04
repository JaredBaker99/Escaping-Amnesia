using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class hiddenTreasure : MonoBehaviour
{
    public GameObject treasure;
    public bool grabbed ;

    void Start()
    {
        grabbed  = false ;
    }

    public void reveal() {
        treasure.SetActive(true) ;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player")) {
            grabbed = true ;
            treasure.SetActive(false) ;
            //update coin count
        }
    }
}

