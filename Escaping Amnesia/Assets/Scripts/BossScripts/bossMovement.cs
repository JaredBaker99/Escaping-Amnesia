using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //NEW
using UnityEngine.AI;

public class bossMovement : MonoBehaviour
{
    public GameObject me;
    public GameObject player;
    public GameObject stats;
    public GameObject toBattle;
    public GameObject bossTrigger;
    public GameObject dialogue;
    public bool isAlive;
    public bool isActive;
    public bool isAtPause;
    public Transform pauseSpot;
    public float startWaitTime;
    private float waitTime;
    private Vector2 previousPosition;
    public int moveSpeed;
    public int attackSpeed;
    public string enemyName;
    private float distance;
    private bool found;
    private bool dialoguRunning = false;
    public Animator animator;



    void Start()
    {
        stats = GameObject.Find("Enemy Stats");
        toBattle = GameObject.Find("To Battle");


        waitTime = startWaitTime;
        previousPosition = transform.position;
        isAlive = true;
        if (stats != null)
        {
            isAlive = stats.GetComponent<EnemyStats>().getBossIsAlive(enemyName);
        }
        //animator.Play(enemyName + "IdleLeft") ;
        if (!isAlive)
        {
            me.SetActive(false);
        }
    }

    void Update()
    {

        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, pauseSpot.position, (moveSpeed * Time.deltaTime));
            if (Vector2.Distance(transform.position, pauseSpot.position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    waitTime = startWaitTime;
                    isAtPause = true;
                    isActive = false;
                }
                else
                {
                    //Run dialogue

                    if (dialoguRunning == false)
                    {
                        dialoguRunning = true;
                        runDialogue();

                    }
                    else
                    {

                        waitTime -= Time.deltaTime;
                    }
                }
            }
        }
        else if (isAtPause)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (attackSpeed * Time.deltaTime));
        }
        movementAnim();
    }

    public void runDialogue()
    {
        Time.timeScale = 0f;
        dialogue.GetComponent<InGameDialogue>().StartDialogue();
    }
    public void playAttack()
    {
        isActive = true;
    }

    public void movementAnim()
    {
        // Calculate the movement direction
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 movementDirection = (currentPosition - previousPosition) / Time.deltaTime;

        // Check if the object is moving
        if (movementDirection != Vector2.zero)
        {
            animator.SetBool("Moving", true);
            // Determine the direction of movement
            if (Mathf.Abs(movementDirection.x) > Mathf.Abs(movementDirection.y))
            {
                // Moving horizontally
                if (movementDirection.x > 0)
                {
                    animator.Play(enemyName + "WalkingRight");
                }
                else
                {
                    animator.Play(enemyName + "WalkingLeft");
                }
            }
            else
            {
                // Moving vertically
                if (movementDirection.y > 0)
                {
                    animator.Play(enemyName + "WalkingUp");
                }
                else
                {
                    animator.Play(enemyName + "WalkingDown");
                }
            }
        }
        else
        {
            // Object is not moving, reset animation
            animator.SetBool("Moving", false);
        }
        previousPosition = currentPosition;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            if (stats != null)
            {
                stats.GetComponent<EnemyStats>().setBossDead(enemyName);
            }
            bossTrigger.SetActive(false);
            player.GetComponent<PlayerController>().turnOnMovement();
            SceneManager.LoadScene("BattleArea");
        }
    }

}