using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trueBossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;
    public GameObject stats ;
    public bool active ;
    public bool triggered ;


    void Start() {
        stats = GameObject.Find("Enemy Stats") ;
        active = false ;
        triggered = false ;
        if(stats != null) {
            active = stats.GetComponent<EnemyStats>().secondBoss ;            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player") && boss.GetComponent<trueBossMovement>().isAlive) {
            if(active) {
                player.GetComponent<PlayerController>().turnOffMovement() ;
                boss.GetComponent<trueBossMovement>().playAttack() ;
            }
            else {
                stats.GetComponent<EnemyStats>().secondBoss = true ;
            }

        }
    }
}
