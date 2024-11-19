using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //NEW
using UnityEngine.AI;

public class trueBossMovement : MonoBehaviour
{
    public GameObject me ;
    public GameObject player;
    public GameObject stats;
    public GameObject toBattle;
    public GameObject bossTrigger ;
    public GameObject opacityTube ;
    public GameObject tube ;
    public bool isAlive ;
    public Transform[] moveSpots ;
    public Transform[] flySpots ;
    public Transform landSpot ;
    public float startWaitTime ;
    private float waitTime ;
    private Vector2 previousPosition;
    public float floatSpeed; 
    public float flySpeed ;
    public float moveSpeed ;
    public int attackSpeed ;
    private int moveSpot ;
    private int flySpot ;
    public string enemyName; 
    private float distance ;
    private bool contained ;
    private bool flying ;
    private bool landing ;
    private bool charging ;
    private int playTube ;
    public float startCrackTime ;
    private float crackTime ;
    public Animator tubeAnimator ;
    public Animator animator ;

    

    void Start() {
        stats = GameObject.Find("Enemy Stats") ;
        toBattle = GameObject.Find("To Battle") ;
        contained = true ;
        flying = false ;
        landing = false ;
        charging = false ;
        playTube = 0 ;
        moveSpot = 0 ;
        waitTime = startWaitTime ;
        previousPosition = transform.position ;
        isAlive = true ;
        if(stats != null) {
            isAlive = stats.GetComponent<EnemyStats>().trueBoss ;
        }
        //animator.Play(enemyName + "IdleLeft") ;
        if(!isAlive) {
            me.SetActive(false) ;
        }
    }

    void Update() {
        //transform.position = Vector2.MoveTowards(transform.position , moveSpots[moveSpot].position, (floatSpeed * Time.deltaTime));


        if(contained) {
            transform.position = Vector2.MoveTowards(transform.position , moveSpots[moveSpot].position, (floatSpeed * Time.deltaTime));

            if(Vector2.Distance(transform.position, moveSpots[moveSpot].position) < 0.2f) {
                if(waitTime <= 0) {
                    moveSpot = (moveSpot+1)%2 ;
                    waitTime =  startWaitTime ;
                }
                else {
                    waitTime -= Time.deltaTime ;
                }
            }
            if(playTube == 1) {
                tubeAnimator.Play("TubeCrack1") ;
                if(crackTime <= 0) {
                    crackTime =  startCrackTime ;
                    playTube++ ;
                }
                else {
                    crackTime -= Time.deltaTime ;
                }
            }
            else if(playTube == 2) {
                tubeAnimator.Play("TubeCrack2") ;
                if(crackTime <= 0) {
                    crackTime =  startCrackTime ;
                    playTube++ ;
                }
                else {
                    crackTime -= Time.deltaTime ;
                }
            }
            else if(playTube == 3) {
                tubeAnimator.Play("TubeCrack3") ;
                if(crackTime <= 0) {
                    crackTime =  startCrackTime ;
                    playTube++ ;
                }
                else {
                    crackTime -= Time.deltaTime ;
                }
            }
            else if(playTube == 4) {
                tubeAnimator.Play("TubeBroken") ;
                opacityTube.SetActive(false) ;
                contained = false ;
                flying = true ;
            }
        }
        else if(flying) {
            transform.position = Vector2.MoveTowards(transform.position , flySpots[flySpot].position, (flySpeed * Time.deltaTime));

            if(Vector2.Distance(transform.position, flySpots[flySpot].position) < 0.2f) {
                flySpot++;
                if(flySpot >= flySpots.Length) {
                    flying = false ;
                    landing = true ;
                    waitTime =  startWaitTime*5 ;
                    
                }
            }
        }
        else if(landing) {
            transform.position = Vector2.MoveTowards(transform.position , landSpot.position, (moveSpeed * Time.deltaTime));
            

            if(Vector2.Distance(transform.position, landSpot.position) < 0.2f) {
                animator.Play(enemyName + "IdleRight") ;
                if(waitTime <= 0) {
                    landing = false ;
                    charging = true ;
                }
                else {
                    waitTime -= Time.deltaTime ;
                }
            }
        }
        else if(charging) {
            transform.position = Vector2.MoveTowards(transform.position , player.transform.position, (attackSpeed * Time.deltaTime));

            movementAnim() ;

        }

    }

    public void playAttack() {
        playTube = 1 ;
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
            } 
            else {
                // Object is not moving, reset animation
                animator.SetBool("Moving", false);
            }
            previousPosition = currentPosition;

        }

        private void OnTriggerEnter2D(Collider2D collision) {
            // Check if the collision is with the player
            if (collision.gameObject.CompareTag("Player")) {
                if(stats != null) {
                    stats.GetComponent<EnemyStats>().trueBoss = false ;
                }
                if(toBattle != null) {
                    toBattle.GetComponent<ToBattleArea>().setToBattle(true) ;
                }
                bossTrigger.SetActive(false) ;
                player.GetComponent<PlayerController>().turnOnMovement() ;
                SceneManager.LoadScene ("BattleArea") ;
            }
        }

}