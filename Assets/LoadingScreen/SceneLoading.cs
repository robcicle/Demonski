using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
        
        while (gameLevel.progress < 1)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
