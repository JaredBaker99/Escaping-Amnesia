using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the player movement
    public Rigidbody2D rb;        // Reference to the Rigidbody2D component
    public Animator animator ;

    Vector2 movement;  // Store the player's movement input

    // Update is called once per frame
    void Update()
    {
        // Get input from player for horizontal (left/right) and vertical (up/down) movement
        movement.x = Input.GetAxisRaw("Horizontal");  // A/D or Left/Right Arrow
        movement.y = Input.GetAxisRaw("Vertical");    // W/S or Up/Down Arrow
        
        if(movement.x == 0 && movement.y == 0) {
            animator.SetBool("Moving", false);
            
        }
        else {
            animator.SetBool("Moving", true);
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

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        // Apply the movement to the player's Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}