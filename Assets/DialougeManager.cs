using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    public Text nameText;
    public Text dialougeText;
    public Image dialougeHead;
    public Sprite knightHead;
    public Sprite playerHead;

    public GameObject player;
    public GameObject nameInput;
    public GameObject nameTextLengthError;
    public Text nameInputText;
    public Text continueText;

    public Animator animator;
    public float skipTime;
    public float delay;

    private Queue<string> sentences;
    private Queue<string> names;

    public bool isOpen = false;
    public bool dialougeEnded;

    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        dialougeEnded = false;

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            continueText.text = "tap to continue";
        }
        else
        {
            continueText.text = "press space to continue";
            GetComponentInChildren<Button>().enabled = false;
        }
    }

    public void ButtonPressed()
    {
        if (skipTime == 1)
        {
            DisplayNextSentence();
            skipTime = 0;
        }
    }

    public void NameInput()
    {
        if (nameInputText.text.Length >= 2)
        {
            nameInput.SetActive(false);
            nameTextLengthError.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = true;
            Time.timeScale = 1f;
        }
        else if (nameInputText.text.Length <= 1)
        {
            nameTextLengthError.SetActive(true);
        }
    }

    private void Update()
    {
        if (nameInput.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Confined;

            Time.timeScale = 0f;
            player.GetComponent<PlayerMovement>().enabled = false;
            if (Input.GetKeyDown("return"))
            {
                NameInput();
            }
        }

        if (Input.GetKeyDown("space") && isOpen && skipTime == 1)
        {
            DisplayNextSentence();
            skipTime = 0;
        }

        if (nameText.text == "Knight")
        {
            dialougeHead.sprite = knightHead;
        }
        else if (nameText.text == "???" & player.GetComponent<PlayerInfo>().nameSet == false)
        {
            dialougeHead.sprite = playerHead;
        }
        else if (nameText.text == player.GetComponent<PlayerInfo>().playerName)
        {
            dialougeHead.sprite = playerHead;
        }
    }

    public void StartDialouge (Dialouge dialouge)
    {
        dialougeEnded = false;
        animator.SetBool("IsOpen", true);
        isOpen = true;

        sentences.Clear();
        names.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in dialouge.names)
        {
            names.Enqueue(name);
        }

        if (nameText.text == "???" && player.GetComponent<PlayerInfo>().nameSet == true)
        {
            nameText.text = player.GetComponent<PlayerInfo>().playerName;
        }
        else
        {
        }

        StartCoroutine(SkipDelay());
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialouge();
            return;
        }

        string name = names.Dequeue();
        nameText.text = name;

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if (nameText.text == "???")
        {
            nameText.text = player.GetComponent<PlayerInfo>().playerName;
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        StartCoroutine(SkipDelay());
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSecondsRealtime(delay);
            dialougeText.text += letter;
            yield return null;
        }
    }

    void EndDialouge()
    {
        animator.SetBool("IsOpen", false);
        isOpen = false;
        dialougeEnded = true;
    }

    IEnumerator SkipDelay()
    {
        yield return new WaitForSeconds(0.8f);
            skipTime = 1;
    }
}
