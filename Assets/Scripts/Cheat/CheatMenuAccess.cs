using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenuAccess : MonoBehaviour
{
    public GameObject panel;
    public bool panelActive;

    // Start is called before the first frame update
    void Update()
    {
        if (panelActive == false)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        panelActive = true;
    }

    public void Close()
    {
        panel.SetActive(false);
        panelActive = false;
    }
}
