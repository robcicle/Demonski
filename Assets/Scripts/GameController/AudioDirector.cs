using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDirector : MonoBehaviour
{
    private GameObject AudioManagerObject;
    private AudioManager AudioManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        AudioManagerObject = GameObject.FindGameObjectWithTag("AudioManager");
        AudioManagerScript = AudioManagerObject.GetComponent<AudioManager>();
    }

    public void SelectSFX()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }
}
