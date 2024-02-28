using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem pickupEffect;
    public int score = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate particle effect
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.IncreaseScore(score);
            }

            Destroy(gameObject);
        }
    }
}