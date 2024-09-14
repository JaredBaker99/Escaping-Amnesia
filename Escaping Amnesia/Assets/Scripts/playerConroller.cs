using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //NEW

public class playerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
{
    float horizontalInput = -Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    Vector2 movement = new Vector2(horizontalInput, verticalInput);

    // Calculate the new position
    Vector2 newPosition = (Vector2)transform.position + movement * movementSpeed * Time.deltaTime;

    // Check for collisions
    if (!IsColliding(newPosition))
    {
        // If no collision, move the character
        rb.velocity = movement * movementSpeed;
    }
    else
    {
        // If collision, adjust the movement to slide along the surface
        Vector2 collisionNormal = GetCollisionNormal(newPosition);
        rb.velocity = -Vector2.Reflect(movement, collisionNormal) * movementSpeed;
    }
}

Vector2 GetCollisionNormal(Vector2 newPosition)
{
    // Cast a box cast from the character's current position to the new position
    RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, newPosition - (Vector2)transform.position, 0.01f);

    // Return the normal of the colliding surface
    return hit.normal;
}

    bool IsColliding(Vector2 newPosition)
    {
        // Cast a box cast from the character's current position to the new position
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, newPosition - (Vector2)transform.position, 0.01f);

        // If the cast hits something, return true
        return hit.collider != null;
    }
}