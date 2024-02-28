using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject weapon;
    private Weapon weaponController;
    private Rigidbody2D rb;
    private bool isGrounded;
    public float timeBetweenBullets = 0.1f;
    private float nextFireTime;
    public Text scoreText;
    private int score = 0;
    private Animator animator;
    public AudioClip jumpSound;
    public Slider healthSlider;
    public int health = 100;
    private int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponController = weapon.GetComponent<Weapon>();
        
        currentHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = currentHealth;
        
        animator = GetComponent<Animator>();
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
    
    public void TakeDamage(int damage)
    {
        // Reduce health by damage amount
        currentHealth -= damage;

        // Ensure health doesn't go below zero
        healthSlider.value = currentHealth;

        // Check if player is dead
        if (currentHealth <= 0f)
        {
            // Die();
        }
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = rb.IsTouchingLayers(LayerMask.GetMask("Ground", "Box"));

        // Move the player left or right
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // Flip the character sprite based on the direction of movement
        if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (moveDirection.x > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Jump if the player is grounded and the spacebar is pressed
        if (isGrounded && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("jump");
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }

        // Check if the mouse button is held down and if enough time has passed since the last shot
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            animator.SetBool("fire", true);

            weaponController.Shoot();
            nextFireTime = Time.time + timeBetweenBullets;
        }
        else
        {
            animator.SetBool("fire", false);
        }
    }
}