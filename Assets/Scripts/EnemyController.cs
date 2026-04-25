using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public float speed;
    public bool vertical;
    public float changeTime;
    public ParticleSystem smokeEffect;

    // 🔊 NOVOS ÁUDIOS
    public AudioClip hitClip;
    public AudioClip fixClip;

    // Private variables
    Rigidbody2D rigidbody2d;
    Animator animator;
    AudioSource audioSource;

    float timer;
    int direction = 1;
    bool broken = true;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // 🔊 pega o áudio
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    // 🔊 SOM AO SER ATINGIDO (ex: pelo projétil)
    public void PlayHitSound()
    {
        if (audioSource != null && hitClip != null)
        {
            audioSource.PlayOneShot(hitClip);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;

        // 🔊 PARA som contínuo (se tiver)
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // 🔊 SOM DE CONSERTO
        if (audioSource != null && fixClip != null)
        {
            audioSource.PlayOneShot(fixClip);
        }

        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}