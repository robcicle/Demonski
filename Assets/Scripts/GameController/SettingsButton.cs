using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsCanvasParent;
    public GameObject settingsCanvas;

    void Awake()
    {
        settingsCanvasParent = GameObject.FindWithTag("Settings");
    }

    // Update is called once per frame
    public void AwakeSettings()
    {
        settingsCanvasParent.transform.GetChild(0).gameObject.SetActive(true);
        MainMenu.objActiveYes = false;
    }

}
