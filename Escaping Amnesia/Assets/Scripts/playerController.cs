using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the player movement
    public Rigidbody2D rb;        // Reference to the Rigidbody2D component
    public Animator animator ;
    public GameObject toBattle; 
    public GameObject sceneCounter;
    public string SceneName ;
    public bool isActive ;

    Vector2 movement;  // Store the player's movement input

    void Start() {
        toBattle = GameObject.Find("To Battle") ;
        isActive = true; 
        if(toBattle != null) {
            toBattle.GetComponent<ToBattleArea>().sceneName = SceneManager.GetActiveScene().name;
            if(toBattle.GetComponent<ToBattleArea>().toBattleArea) {
                rb.transform.position =  toBattle.GetComponent<ToBattleArea>().playerPosition ;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive) {
            // Get input from player for horizontal (left/right) and vertical (up/down) movement
            movement.x = Input.GetAxisRaw("Horizontal");  // A/D or Left/Right Arrow
            movement.y = Input.GetAxisRaw("Vertical");    // W/S or Up/Down Arrow
            
            if(movement.x == 0 && movement.y == 0) {
                animator.SetBool("Moving", false);
                
            }
            else {
                animator.SetBool("Moving", true);
                if(toBattle != null) {
                    toBattle.GetComponent<ToBattleArea>().setPlayerPosition(rb.transform.position) ;
                }
                if(movement.x > 0) {
                    animator.Play("022WalkingRight") ;
                }
                else if(movement.x < 0){
                    animator.Play("022WalkingLeft") ;
                }
                else if(movement.y > 0) {
                    animator.Play("022WalkingUp") ;
                }
                else if(movement.y < 0) {
                    animator.Play("022WalkingDown") ;
                }
            }
        }

        
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        // Apply the movement to the player's Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void turnOffMovement() {
        isActive = false;
        movement.x = 0 ;
        movement.y = 0 ;
        animator.SetBool("Moving", false);
    }

    public void turnOnMovement() {
        isActive = true ;
    }
}