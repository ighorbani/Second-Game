using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Magician : Enemy
{

    public ParticleSystem dieEffect;
    private Transform player;
    private bool isAttacking = false;
    private float attackCooldownTimer = 0f;
    public Slider healthSlider;
    public GameObject panelObject;
    public GameObject modalObject;
    public GameObject backgroundMusic;
    private int currentHealth;
    
   void Start()
    {
        currentHealth = health;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ContinuousAttack());

        // currentHealth = health;
        // healthSlider.maxValue = health;
        // healthSlider.value = currentHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
            panelObject.SetActive(true);
            modalObject.SetActive(true);
            
            panelObject.GetComponent<Animator>().SetTrigger("intro");
            modalObject.GetComponent<Animator>().SetTrigger("intro");
           
            AudioSource audioSource = backgroundMusic.GetComponent<AudioSource>();

            float startVolume = audioSource.volume;
            float startTime = Time.time;
            
            audioSource.volume = .3f;
        }

        // Start the attack cooldown timer
        attackCooldownTimer = timeBetweenAttacks;
    }
    


    IEnumerator ContinuousAttack()
    {
        while (true)
        {
            yield return StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        // Set attack animation state
        GetComponent<Animator>().SetBool("attack", true);

        // Move towards the player quickly
        while (Vector3.Distance(transform.position, player.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);

            // Flip the magician sprite based on the direction of movement
            float movementDirection = Mathf.Sign(player.position.x - transform.position.x);

            if (movementDirection < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (movementDirection > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            yield return null;
        }

        // Reset attacking state
        isAttacking = false;

        // Wait for a short delay (you can adjust this value)
        yield return new WaitForSeconds(0.1f);

        // Reset the attack animation state
        GetComponent<Animator>().SetBool("attack", false);

        // Set the attack cooldown timer
        attackCooldownTimer = timeBetweenAttacks;

        // Wait for the cooldown period before the next attack
        while (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerHealth = other.GetComponent<Player>();
    
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
