using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public LayerMask enemyLayer;
    public ParticleSystem swordTrail;
    public ParticleSystem playerTrail;

    private PlayerMovement playerMovement;
    public Transform hitPoint;

    float runSpeedBefore;

    public int attackDamage = 40;
    public float attackRange = 0.5f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        runSpeedBefore = playerMovement.runSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e") && playerMovement.jump == false && playerMovement.crouch == false)
        {
            StartCoroutine(Attack());
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void JoystickAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (this.enabled == true)
            {
                if (playerMovement.jump == false && playerMovement.crouch == false)
                {
                    StartCoroutine(Attack());
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        swordTrail.Play();
        playerTrail.Play();
        animator.SetTrigger("Attack");
        playerMovement.runSpeed = 0f;
        playerMovement.jumpReady = false;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.isTrigger)
            {
                enemy.GetComponentInParent<Enemy>().TakeDamage(attackDamage);

            }
        }
        yield return new WaitForSecondsRealtime(0.1f);
        playerMovement.runSpeed = runSpeedBefore;
        yield return new WaitForSecondsRealtime(0.15f);
        playerMovement.jumpReady = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;

        Gizmos.DrawWireSphere(hitPoint.position, attackRange);
    }
}
