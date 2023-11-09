using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Image heartOne;
    public Image heartTwo;
    public Image heartThree;

    public Sprite heart;
    public Sprite halfHeart;

    void Start()
    {
        UpdateHearts();
    }

    // Update is called once per frame
    public void UpdateHearts()
    {
        if (playerHealth.currentHealth <= 0)
        {
            heartOne.sprite = null;
            heartOne.color = new Color(255, 255, 255, 0);
            heartTwo.sprite = null;
            heartTwo.color = new Color(255, 255, 255, 0);
            heartThree.sprite = null;
            heartThree.color = new Color(255, 255, 255, 0);
        }
        else if (playerHealth.currentHealth <= 20)
        {
            heartOne.sprite = halfHeart;
            heartOne.color = new Color(255, 255, 255, 255);
            heartTwo.sprite = null;
            heartTwo.color = new Color(255, 255, 255, 0);
            heartThree.sprite = null;
            heartThree.color = new Color(255, 255, 255, 0);
        }
        else if (playerHealth.currentHealth <= 40)
        {
            heartOne.sprite = heart;
            heartOne.color = new Color(255, 255, 255, 255);
            heartTwo.sprite = null;
            heartTwo.color = new Color(255, 255, 255, 0);
            heartThree.sprite = null;
            heartTwo.color = new Color(255, 255, 255, 0);
        }
        else if (playerHealth.currentHealth <= 60)
        {
            heartOne.sprite = heart;
            heartOne.color = new Color(255, 255, 255, 255);
            heartTwo.sprite = halfHeart;
            heartTwo.color = new Color(255, 255, 255, 255);
            heartThree.sprite = null;
            heartThree.color = new Color(255, 255, 255, 0);
        }
        else if (playerHealth.currentHealth <= 80)
        {
            heartOne.sprite = heart;
            heartOne.color = new Color(255, 255, 255, 255);
            heartTwo.sprite = heart;
            heartTwo.color = new Color(255, 255, 255, 255);
            heartThree.sprite = null;
            heartThree.color = new Color(255, 255, 255, 0);
        }
        else if (playerHealth.currentHealth <= 100)
        {
            heartOne.sprite = heart;
            heartOne.color = new Color(255, 255, 255, 255);
            heartTwo.sprite = heart;
            heartTwo.color = new Color(255, 255, 255, 255);
            heartThree.sprite = halfHeart;
            heartThree.color = new Color(255, 255, 255, 255);
        }
        else if (playerHealth.currentHealth >= 120)
        {
            heartOne.sprite = heart;
            heartOne.color = new Color(255, 255, 255, 255);
            heartTwo.sprite = heart;
            heartTwo.color = new Color(255, 255, 255, 255);
            heartThree.sprite = heart;
            heartThree.color = new Color(255, 255, 255, 255);
        }
    }
}
