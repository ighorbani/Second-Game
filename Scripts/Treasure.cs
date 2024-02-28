using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject key;
    public GameObject particleEffect;
    private Animator treasureAnimator;

    private bool treasureOpened = false;
    private AudioSource audioSource;

    private void Start()
    {
        treasureAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !treasureOpened)
        {
            treasureAnimator.SetTrigger("open");
            Invoke("InstantiateStar", .5f);
            treasureOpened = true;
            audioSource.PlayOneShot(audioSource.clip); }
    }
    
    private void InstantiateStar()
    {
        Instantiate(particleEffect, transform.position, Quaternion.identity);
        Instantiate(key, transform.position, Quaternion.identity);
    }
}