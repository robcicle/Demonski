using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool objActiveYes = true;
    public GameObject mC;

    Scene m_Scene;
    string sceneName;

    private void Start()
    {
        {
            m_Scene = SceneManager.GetActiveScene();
            sceneName = m_Scene.name;
        }

        Cursor.lockState = CursorLockMode.Confined;   
    }

    // Start is called before the first frame update
    void Update()
    {
        if (sceneName == "Menu")
        {
            if (objActiveYes == true)
            {
                ActiveYes();
            }
            else
            {
                ActiveNo();
            }
        }
    }

    // Update is called once per frame
    public void ActiveYes()
    {
        mC.SetActive(true);
    }

    public void ActiveNo()
    {
        mC.SetActive(false);
    }
}
