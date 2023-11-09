using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    public bool fall = false;

    // Start is called before the first frame update
    private IEnumerator Touch()
    {
        fall = true;
        yield return new WaitForSecondsRealtime(2);
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Character")
        {
            StartCoroutine("Touch");
        }
    }
}
