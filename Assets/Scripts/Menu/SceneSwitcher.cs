using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    Scene m_Scene;
    string sceneName;
    public Animator animator;

    private void Start()
    {
        {
            m_Scene = SceneManager.GetActiveScene();
            sceneName = m_Scene.name;
        }
    }

    public void GotoMainScene()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("newScene");
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void GotoMenuScene()
    {
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        animator.SetTrigger("newScene");
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        StartCoroutine(Quit());
    }

    IEnumerator Quit()
    {
        animator.SetTrigger("newScene");
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
        Debug.Log("Game is exiting");
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}