using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 5f;

    private bool isClimbing = false;
    private Rigidbody2D playerRb;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClimbing = true;
            playerRb = other.GetComponent<Rigidbody2D>();
            playerRb.velocity = Vector2.zero;  // Stop player's current velocity

            // Deactivate colliders with the "Ground" tag
            DeactivateGroundColliders();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClimbing = false;

            // Reactivate colliders with the "Ground" tag
            ReactivateGroundColliders();
        }
    }

    void Update()
    {
        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 climbForce = new Vector2(0f, verticalInput * climbSpeed);

            // Use MovePosition to manually move the player's position
            playerRb.MovePosition(playerRb.position + climbForce * Time.deltaTime);
        }
    }

    void DeactivateGroundColliders()
    {
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");
        
        foreach (GameObject groundObject in groundObjects)
        {
            Collider2D collider = groundObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    void ReactivateGroundColliders()
    {
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");
        
        foreach (GameObject groundObject in groundObjects)
        {
            Collider2D collider = groundObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }
}