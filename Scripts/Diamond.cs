﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public ParticleSystem pickupEffect;
    public int score = 50;

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