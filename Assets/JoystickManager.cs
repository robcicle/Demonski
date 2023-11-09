using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoystickManager : MonoBehaviour
{
    Scene m_Scene;
    string sceneName;

    public PlayerCombat playerCombat;
    public static JoystickManager instance;
    public bool testing;

    public GameObject joystick;
    public GameObject sword;
    public Transform joystickMenu;
    public Transform joystickPlay;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (SystemInfo.deviceType == DeviceType.Desktop && testing == false)
        {
            joystick.SetActive(false);
        }
        else
        {
            joystick.SetActive(true);
        }
    }

    public void SwordAttack()
    {
        if (playerCombat != null)
        {
            playerCombat.JoystickAttack();
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerCombat = player.GetComponent<PlayerCombat>();
        }
    }

    private void Update()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;

        if (sceneName == "Menu" && testing == true)
        {
            joystick.SetActive(true);
            joystick.transform.position = joystickMenu.position;
            sword.SetActive(false);
        }

        if (sceneName == "Loading Screen" && testing == true)
        {
            joystick.SetActive(false);
            sword.SetActive(false);
        }

        if (sceneName == "Level1" && testing == true)
        {
            joystick.SetActive(true);
            sword.SetActive(true);
            joystick.transform.position = joystickPlay.position;
        }
    }
}
