using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Animator animator;
    public Transform hitPoint;
    private CircleCollider2D circleCollider;
    public Transform playerPresentGO;
    public LayerMask playerLayer;

    private bool playerPresent;
    bool right;

    public bool attacking;

    public float hitRange = 1f;
    public float attackRate = 2f;
    public float attackRange = 0.5f;

    public int attackDamage = 40;

    float nextAttackTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerPresent = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAI enemyAI = GetComponent<EnemyAI>();

        if (Time.time >= nextAttackTime)
        {
            if (playerPresent == true && enemyAI.hostile == true)
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (attacking == true)
        {
            enemyAI.run = 0;
        }

        if (gameObject.transform.localScale.x >= 1)
        {
            right = true;
        }
        else if (gameObject.transform.localScale.x <= -1)
        {
            right = false;
        }
    }

    private void FixedUpdate()
    {
        if (right == true)
        {
            RaycastHit2D player = Physics2D.Raycast(playerPresentGO.position, Vector2.right, (hitRange), playerLayer);
            if (player && player.collider.gameObject.name == "Character")
            {
                playerPresent = true;
            }
            else
            {
                playerPresent = false;
            }
        }

        if (right == false)
        {
            RaycastHit2D player = Physics2D.Raycast(playerPresentGO.position, Vector2.left, (hitRange), playerLayer);
            if (player && player.collider.gameObject.name == "Character")
            {
                playerPresent = true;
            }
            else
            {
                playerPresent = false;
            }
        }
    }

    public IEnumerator Attack()
    {
        attacking = true;
        animator.SetTrigger("Attack");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(hitPoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            if (player.isTrigger)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage, gameObject);
            }
        }
        yield return new WaitForSecondsRealtime(0.3f);
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;

        Gizmos.DrawWireSphere(hitPoint.position, attackRange);
    }

    
}
