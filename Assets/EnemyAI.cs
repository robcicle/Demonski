using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    public float distance;
    public float runSpeed;
    public float run;
    public float groundCheckRadius;
    private float horizontalMove;

    public bool hostile;
    private bool grounded;
    private bool facingRight;
    private bool fallCheck;

    private Rigidbody2D rB2D;
    private Animator animator;
    private Enemy enemyHealth;

    public LayerMask playerLayer;
    public LayerMask groundLayer;
    private Vector3 velocity = Vector3.zero;

    public Transform groundCheck;
    public Transform groundCheckRaycast;
    public GameObject alert;
    public GameObject orientation;
    public GameObject target;

    void Start()
    {
        enemyHealth = gameObject.GetComponentInChildren<Enemy>();
        rB2D = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FallCheck();
        groundCheckRaycast.localScale = gameObject.transform.localScale;

        if (enemyHealth.currentHealth <= 0)
        {
            this.enabled = false;
        }

        if (run == 1)
        {
            facingRight = true;
        }
        else if (run == -1)
        {
            facingRight = false;
        }

        float runAnimator = Mathf.Abs(run);
        animator.SetFloat("Speed", runAnimator);

        if (!hostile)
        {
            rB2D.velocity = new Vector2(0, 0);
            run = 0;
        }
    }

    void FixedUpdate()
    {
        horizontalMove = run * runSpeed;
        RaycastHit2D hit = Physics2D.Raycast(orientation.transform.position, Vector2.right, (11f), playerLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(orientation.transform.position, Vector2.left, (2f), playerLayer);
        if (target == null)
        {
            if (hit && hit.collider.gameObject.layer == 8)
            {
                target = hit.collider.gameObject;
            }

            if (hit2 && hit2.collider.gameObject.layer == 8)
            {
                target = hit2.collider.gameObject;
            }
        }

        if (target && hostile)
        {
            distance = (target.transform.position.x - transform.position.x);
            Flip();
            if (distance >= 0.75 && grounded == true && fallCheck == false)
            {
                run = 1;
                Move();
            }
            else if (distance <= -0.75 && grounded == true && fallCheck == false)
            {
                run = -1;
                Move();
            }
            else
            {
                run = 0;
                horizontalMove = 0;
                rB2D.velocity = new Vector2(0, 0);
            }

            if (alert != null)
            {
                Alert();
            }

            if (target.transform.position.y - transform.position.y >= 6)
            {
                target = null;
                rB2D.velocity = new Vector2(0, 0);
            }
            else if (target.transform.position.y - transform.position.y <= -6)
            {
                target = null;
                rB2D.velocity = new Vector2(0, 0);
            }
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
    }

    void Move()
    {
        Vector3 targetVelocity = new Vector2(horizontalMove * 10f, rB2D.velocity.y);
        rB2D.velocity = Vector3.SmoothDamp(rB2D.velocity, targetVelocity, ref velocity, movementSmoothing);
    }

    void FallCheck()
    {
        Vector2 rayRot = new Vector3(groundCheckRaycast.localScale.x, -1, 40);
        RaycastHit2D hit = Physics2D.Raycast(groundCheckRaycast.transform.position, rayRot, 100f, groundLayer);
        if (hit && hit.collider.gameObject.layer == 9)
        {
            fallCheck = false;
        }
        else if (!hit)
        {
            fallCheck = true;
        }
    }

    void Flip()
    {
        if (facingRight == true)
        {
            transform.localScale = new Vector3 (1, 1, 1);
        }
        else if (facingRight == false)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Alert()
    {
        alert.SetActive(true);
        Destroy(alert, 1f);
    }
}
