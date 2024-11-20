using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //NEW
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    public GameObject me ;
    public GameObject player;
    public GameObject stats ;
    public bool isAlive ;
    public float patrolSpeed ;
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
    public GameObject toBattle ;
    private int numKilled ;
    private int numSkipped ;
    private float originalSpeed ;
    private float originalAccel ;
    public float speedPercent ;
    public float radiusPercent ;
    NavMeshAgent agent; 
    
   

    void Start() {
        randSpot = Random.Range(0, moveSpots.Length) ; 
        waitTime = startWaitTime ;
        previousPosition =  transform.position ;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false ;
        agent.updateUpAxis = false ;
        originalSpeed = agent.speed ;
        originalAccel = agent.acceleration ;

        toBattle = GameObject.Find("To Battle") ;
        stats = GameObject.Find("Enemy Stats") ;
        if(stats != null) {
            isAlive = stats.GetComponent<EnemyStats>().getIsAlive(enemyName) ;
            if(toBattle.GetComponent<ToBattleArea>().toBattleArea == false) {
                stats.GetComponent<EnemyStats>().enemiesSpawned++ ;
            }
        }
        if(!isAlive) {
            me.SetActive(false) ;
        }
    }
    void Update()
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;

            if(stats != null) {
                numKilled = stats.GetComponent<EnemyStats>().enemiesKilled ;
                numSkipped = stats.GetComponent<EnemyStats>().enemiesSpawned ;
            }
            else {
                numKilled = 0 ;
                numSkipped = 0 ;
            }
            

            if(found || distance < (tracking + ((numSkipped - numKilled)*radiusPercent))) {
                if(!found) {
                    found = true ;
                }
                agent.speed = originalSpeed + (numSkipped - numKilled) * speedPercent ;
                agent.acceleration = originalAccel + (numSkipped - numKilled) * speedPercent ;
                agent.SetDestination(player.transform.position) ;
            }
            else if(!found) {
                


                transform.position = Vector2.MoveTowards(transform.position , moveSpots[randSpot].position, (patrolSpeed * Time.deltaTime));

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

        private void OnTriggerEnter2D(Collider2D collision) {
            // Check if the collision is with the player
            if (collision.gameObject.CompareTag("Player")) {
                if(toBattle != null) {
                    toBattle.GetComponent<ToBattleArea>().setToBattle(true) ;
                }
                if(stats != null) {
                    stats.GetComponent<EnemyStats>().setDead(enemyName) ;
                }
                SceneManager.LoadScene ("BattleArea") ;
            }
        }

}