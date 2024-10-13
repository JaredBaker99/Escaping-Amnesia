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
    public string enemyName; 

    private float distance ;
    private bool found ;
    private int randSpot ;
    private float waitTime ;
    private Vector2 previousPosition;

    
    public Animator animator ;
    
   

    void Start() {
        randSpot = Random.Range(0, moveSpots.Length) ; 
        waitTime = startWaitTime ;
        previousPosition =  transform.position ;

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
            movementAnim() ;
            

        }

        public void movementAnim() {
            // Calculate the movement direction
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 movementDirection = (currentPosition - previousPosition) / Time.deltaTime;

            // Check if the object is moving
            if (movementDirection != Vector2.zero) {
                animator.SetBool("Moving", true);
                // Determine the direction of movement
                if (Mathf.Abs(movementDirection.x) > Mathf.Abs(movementDirection.y)) {
                    // Moving horizontally
                    if (movementDirection.x > 0) {
                        animator.Play(enemyName + "WalkingRight");
                    } else {
                        animator.Play(enemyName + "WalkingLeft");
                    }
                } else {
                    // Moving vertically
                    if (movementDirection.y > 0) {
                        animator.Play(enemyName + "WalkingUp");
                    } else {
                        animator.Play(enemyName + "WalkingDown");
                    }
                }
            } else {
                // Object is not moving, reset animation
                animator.SetBool("Moving", false);
            }

            // Update previous position
            previousPosition = currentPosition;
        }

}