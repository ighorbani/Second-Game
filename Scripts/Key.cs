using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public ParticleSystem pickupEffect;
    public int score = 100;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.IncreaseScore(score);
            }

            Destroy(gameObject);
        }
    }
}