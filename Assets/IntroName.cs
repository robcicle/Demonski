using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroName : MonoBehaviour
{
    public GameObject nameInput;
    public GameObject block;
    public Enemy enemyOne;
    public Enemy enemyTwo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Character")
        {
            StartCoroutine(NameOpen());
        }
    }

    private void Update()
    {
        if (enemyOne.dead && enemyTwo.dead)
        {
            Destroy(block);
        }
    }

    IEnumerator NameOpen()
    {
        nameInput.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        this.enabled = false;
    }
}
