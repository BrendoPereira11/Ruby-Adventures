using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    // 🔊 NOVOS CLIPS
    public AudioClip hitClip;
    public AudioClip fixClip;

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
        audioSource = GetComponent<AudioSource>();

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
            position.y += speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x += speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 🔊 SOM DE ACERTO (ANTES DE DESTRUIR)
        if (audioSource != null && hitClip != null)
        {
            audioSource.PlayOneShot(hitClip);
        }

        Destroy(gameObject);
    }

    public void Fix()
    {
        broken = false;

        rigidbody2d.simulated = false;

        // 🔊 PARA som de movimento
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
    }
}