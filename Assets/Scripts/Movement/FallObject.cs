using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    private Rigidbody2D rB2D;
    public FallTrigger fT;

    public float fallTime;

    // Start is called before the first frame update
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(fT.fall == true)
        {
            StartCoroutine("Fall");
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallTime);
            rB2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
}
