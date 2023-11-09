using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    private Image blackFade;
    public GameObject deathMenu;
    public HeartManager heartManager;
    Vector2 playerScale;
    public ParticleSystem blood;


    public int maxHealth = 100;
    public int currentHealth;

    public bool dead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        blackFade = GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();
        blackFade.gameObject.SetActive(false);
        if (currentHealth >= 1)
        {
            dead = false;
        }

    }

    public void TakeDamage(int damage, GameObject enemy)
    {
        currentHealth -= damage;
        blood.Play();
        
        if (currentHealth >= 1)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Die(enemy));
            dead = true;
            
        }

        playerScale = gameObject.transform.localScale;
        blood.transform.localScale = playerScale;

        heartManager.UpdateHearts();
    }

    IEnumerator Die(GameObject enemy)
    {
        Animator enemyAnimator = enemy.GetComponent<Animator>();
        if (enemy.tag == "Enemy")
        {
            enemyAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        blackFade.gameObject.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0f;
        animator.SetBool("IsDead", true);
        yield return new WaitForSecondsRealtime(0.55f);
        enemyAnimator.updateMode = AnimatorUpdateMode.Normal;
        deathMenu.SetActive(true);
        blackFade.gameObject.SetActive(false);
        this.enabled = false;
    }
}
