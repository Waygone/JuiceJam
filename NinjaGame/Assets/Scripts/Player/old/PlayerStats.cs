using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int maxHp;

    [SerializeField]
    private GameObject 
        deathPiecesParticle, 
        deathBloodParticle;

    private GameManager GM;
    [NonSerialized] public float currentHp;

    [SerializeField] public HealthBar healthBar;

    private Rigidbody2D rb;

    private bool isDead = false;

    private Color spriteColor;

    private void Start()
    {
        currentHp = maxHp;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteColor = GetComponent<SpriteRenderer>().color;
        rb = GetComponent<Rigidbody2D>();

        healthBar.SetMaxHp(maxHp);
    }
    private void Update()
    {
        if (GM.hasAlreadyRespawned)
        {
            GM.hasAlreadyRespawned = false;
            spriteColor.a = 1f;
            GetComponent<SpriteRenderer>().color = spriteColor;
            rb.bodyType = RigidbodyType2D.Dynamic;
            currentHp = maxHp;
            healthBar.SetHealth((int)currentHp);
            isDead = false;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        healthBar.SetHealth((int)currentHp);

        if (currentHp <= 0.0f)
        {
            if (!isDead)
            {
                isDead = true;
                Die();
            }
        }
    }

    private void Die()
    {
        Instantiate(deathPiecesParticle, transform.position, deathPiecesParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);

        GM.Respawn();

        spriteColor.a = 0f;
        GetComponent<SpriteRenderer>().color = spriteColor;
        rb.bodyType = RigidbodyType2D.Static;
        //Destroy(gameObject);
    }
}
