using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenuGodMode : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public int HealthBeforeGod;
    public bool God;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject playerHealthGO = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerHealthGO.GetComponent<PlayerHealth>();

        God = false;
    }

    void OnEnable()
    {
        HealthBeforeGod = playerHealth.currentHealth;
        God = true;
    }

    void OnDisable()
    {
        God = false;
        playerHealth.currentHealth = HealthBeforeGod;
    }

    void Update()
    {
        if (God == true)
        {
            playerHealth.currentHealth = 1000000000;
        }

        GameObject playerHealthGO = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerHealthGO.GetComponent<PlayerHealth>();
    }
}
