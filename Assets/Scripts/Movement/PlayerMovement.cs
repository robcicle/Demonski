using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public GameObject runningParticle;
    public Transform particlePos;

    public Joystick joystick;

    public float runSpeed = 40f;

    Scene m_Scene;
    string sceneName;

    float particleTimer;
    public float horizontalMove = 0f;
    public bool jump = false;
    public bool joystickControls;
    public bool jumpReady = true;
    public bool crouch = false;
    bool menu = false;

    private void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        particleTimer = 1f;
    }

    private void Awake()
    {
        GameObject joystickGO = GameObject.FindGameObjectWithTag("Joystick");
        joystick = joystickGO.transform.GetChild(0).gameObject.GetComponent<Joystick>();
    }

    IEnumerator RunParticle()
    {
        particleTimer = 0f;
        GameObject RunningParticleGO = Instantiate(runningParticle);
        RunningParticleGO.transform.position = particlePos.transform.position;
        RunningParticleGO.transform.localScale = new Vector2(particlePos.localScale.x, transform.localScale.y);
        RunningParticleGO.SetActive(true);
        Destroy(RunningParticleGO, 1f);
        yield return new WaitForSeconds(1);
        particleTimer = 1f;
    }


    void Update()
    {
        GameObject joystickGO = GameObject.FindGameObjectWithTag("Joystick");

        if (joystickGO.transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            joystickControls = true;
        }
        else
        {
            joystickControls = false;
        }

        if (sceneName == "Menu")
        {
            menu = true;
        }
        else
        {
            menu = false;
        }

        particlePos.transform.localScale = gameObject.transform.localScale;

        if (SystemInfo.deviceType == DeviceType.Desktop && joystickControls == false)
        {
            horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

            if (horizontalMove >= 25 && jump == false)
            {
                if (particleTimer == 1f && crouch == false)
                {
                    StartCoroutine(RunParticle());
                }
            }
            else if (horizontalMove <= -25 && jump == false)
            {
                if (particleTimer == 1f && crouch == false)
                {
                    StartCoroutine(RunParticle());
                }
            }
            else
            {
            }

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump") && menu == false)
            {
                if (controller.m_Grounded == true)
                {
                    if (jump == false && jumpReady == true)
                    {
                        jump = true;
                        crouch = false;
                        animator.SetBool("IsJumping", true);
                        FindObjectOfType<AudioManager>().Play("PlayerJump");
                    }
                }
            }

            if (jump == false)
            {

                if (Input.GetButtonDown("Crouch"))
                {
                    jumpReady = false;
                    crouch = true;
                    jump = false;
                    animator.SetBool("IsCrouching", true);
                }
                else if (Input.GetButtonUp("Crouch"))
                {
                    jumpReady = true;
                    crouch = false;
                    animator.SetBool("IsCrouching", false);
                }
            }
        }
        else if (joystickControls == true)
        {
            if (joystick.Horizontal >= .2f)
            {
                horizontalMove = runSpeed;
                if (jump == false)
                {
                    if (particleTimer == 1f && crouch == false)
                    {
                        StartCoroutine(RunParticle());
                    }
                }
            }
            else if (joystick.Horizontal <= -.2f)
            {
                horizontalMove = -runSpeed;
                if (jump == false)
                {
                    if (particleTimer == 1f && crouch == false)
                    {
                        StartCoroutine(RunParticle());
                    }
                }
            }
            else 
            {
                horizontalMove = 0f;
            }

            float verticalMove = joystick.Vertical;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (verticalMove >= .5f && menu == false && jumpReady == true)
            {
                if (controller.m_Grounded == true)
                {
                    if (jump == false)
                    {
                        jump = true;
                        crouch = false;
                        animator.SetBool("IsJumping", true);
                        FindObjectOfType<AudioManager>().Play("PlayerJump");
                        jumpReady = false;
                    }
                }
            }
            else if (verticalMove <= .5f)
            {
                jumpReady = true;
            }

            if (jump == false)
            {

                if (verticalMove <= -.5f && jump == false)
                {
                    if (jump == false)
                    {
                        crouch = true;
                        animator.SetBool("IsCrouching", true);
                    }
                }
                else
                {
                    crouch = false;
                    animator.SetBool("IsCrouching", false);
                }
            }
        }
    }

    public void OnLanding ()
    {
        jump = false;
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    }
}