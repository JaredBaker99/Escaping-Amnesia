using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //NEW

public class enemyMovement : MonoBehaviour
{
    public GameObject player;
    public float patrolSpeed ;
    public  float chaseSpeed ;

    public float tracking ;
    public Transform[] moveSpots ;
    public float startWaitTime; 

    private float distance ;
    private bool found ;
    private int randSpot ;
    private float waitTime ;
    
   

    void Start() {
        randSpot = Random.Range(0, moveSpots.Length) ; 
        waitTime = startWaitTime ;
    }
    void Update()
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;

            if(found || distance < tracking) {
                if(!found) {
                    found = true ;
                }
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
            }
            else if(!found) {
                

                transform.position = Vector2.MoveTowards(transform.position , moveSpots[randSpot].position, patrolSpeed * Time.deltaTime);

                if(Vector2.Distance(transform.position, moveSpots[randSpot].position) < 0.2f) {
                    if(waitTime <= 0) {
                        randSpot = Random.Range(0, moveSpots.Length) ; 
                        waitTime =  startWaitTime ;
                    }
                    else {
                        waitTime -= Time.deltaTime ;
                    }
                }
            }
            

        }

}