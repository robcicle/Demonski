using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject deathMenuUI;
    public GameObject pauseMenuUI;
    private PlayerHealth playerHealth;
    public AudioDirector audioDirector;

    private void Start()
    {
        GameObject playerHealthGO = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerHealthGO.GetComponent<PlayerHealth>();
        audioDirector = GameObject.Find("GameController").GetComponent<AudioDirector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && deathMenuUI.activeInHierarchy == false && playerHealth.dead == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        audioDirector.SelectSFX();
        GameIsPaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Settings()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
