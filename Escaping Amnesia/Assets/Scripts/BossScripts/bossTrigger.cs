using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player") && boss.GetComponent<bossMovement>().isAlive) {
            player.GetComponent<PlayerController>().turnOffMovement() ;
            boss.GetComponent<bossMovement>().playAttack() ;
        }
    }
}
