using System.Collections;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public FallTrigger fallTrigger;
    public Animator groundMovement;
    public bool grndMove;
    public bool disableScript;

    private void Start()
    {
        grndMove = false;
    }

    private void Update()
    {
        if (fallTrigger.fall == true)
        {
            StartCoroutine("grndMoveBool");
            //grndMove = true;
            
        }
    }

    IEnumerator grndMoveBool()
    {
        yield return new WaitForSeconds(1);
            groundMovement.SetBool("GroundMovement", true);
        if (disableScript == true)
        {
            this.enabled = false;
        }
        else
        {
        }
    }
}


