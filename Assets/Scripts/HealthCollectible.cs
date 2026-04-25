using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    AudioSource audioSource;
    Collider2D collider2d;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collider2d = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null && controller.health < controller.maxHealth)
        {
            controller.ChangeHealth(1);

            // desativa visual e colisão
            collider2d.enabled = false;
            spriteRenderer.enabled = false;

            // toca o som
            audioSource.Play();

            // destrói depois do som
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}