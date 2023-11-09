using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public ParticleSystem blood;
    private GameObject enemyGO;
    private GameObject player;
    Vector2 enemyScale;

    public bool dead;
    public int hurtThrust;
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        enemyGO = GetComponentInParent<Transform>().gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        if (currentHealth >= 1)
        {
            dead = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Rigidbody2D RB = gameObject.GetComponent<Rigidbody2D>();
        if (blood != null) 
        {
            blood.Play();
        }
        else
        {
        }
        GetComponent<EnemyAI>().hostile = true;

        if (currentHealth >= 1)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        GetComponent<EnemyCombat>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        animator.SetBool("IsDead", true);
        Rigidbody2D rB2D = GetComponent<Rigidbody2D>();
        Destroy(rB2D);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        if (blood != null)
        {
            Destroy(blood.gameObject);
        }
        gameObject.layer = 0;
        this.enabled = false;
    }

    private void Update()
    {
        enemyScale = enemyGO.transform.localScale;
        blood.transform.localScale = enemyScale;
    }
}
