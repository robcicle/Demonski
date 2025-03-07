using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    public GameObject enemyOne;
    public GameObject enemyTwo;

    public Transform positionOnTrigger;
    public GameObject character;
    public DialougeTrigger dialougeTrigger;
    public DialougeManager dialougeManager;

    bool freeze = false;
    float characterY;

    IEnumerator IntroPlay()
    {
        ScriptFreeze();
        freeze = true;
        yield return new WaitForEndOfFrame();
        enemyOne.transform.localScale = new Vector3(-1, 1, 1);
        enemyTwo.transform.localScale = new Vector3(-1, 1, 1);
        dialougeTrigger.TriggerDialouge();
    }

    private void Update()
    {
        characterY = character.transform.position.y;

        if (freeze == true)
        {
            character.transform.position = new Vector2(positionOnTrigger.transform.position.x, character.transform.position.y);
        }

        if (gameObject.activeInHierarchy && dialougeManager.dialougeEnded == false)
        {
            character.GetComponent<PlayerCombat>().enabled = false;
            character.GetComponent<PlayerMovement>().jumpReady = false;
        }

        if (dialougeManager.dialougeEnded)
        {
            StartCoroutine(IntroEnd());
        }   
    }

    void ScriptFreeze()
    {
        character.GetComponent<PlayerCombat>().enabled = false;
        character.GetComponent<PlayerMovement>().horizontalMove = 0f;
        character.GetComponent<PlayerMovement>().enabled = false;
        character.GetComponent<Animator>().SetBool("IsJumping", false);
        character.GetComponent<Animator>().SetBool("IsCrouching", false);
        character.GetComponent<Animator>().SetBool("Grounded", true);
        character.GetComponent<Animator>().SetFloat("Speed", 0f);
        character.GetComponent<CharacterController2D>().m_Velocity = new Vector2(0, 0);
        character.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    IEnumerator IntroEnd()
    {
        freeze = false;
        character.GetComponent<PlayerCombat>().enabled = true;
        character.GetComponent<PlayerMovement>().enabled = true;
        character.GetComponent<PlayerMovement>().jumpReady = true;
        character.GetComponent<CharacterController2D>().enabled = true;
        enemyOne.GetComponent<EnemyAI>().hostile = true;
        enemyTwo.GetComponent<EnemyAI>().hostile = true;
        enemyOne.GetComponent<EnemyAI>().target = character;
        enemyTwo.GetComponent<EnemyAI>().target = character;
        yield return new WaitForSeconds(0.8f);
        dialougeManager.dialougeEnded = true;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character" && dialougeManager.dialougeEnded == false)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(IntroPlay());
        }
    }
}
