using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoundary : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<PlayerHealth>())
        {
            other.GetComponent<PlayerHealth>().TakeDamage(5000, gameObject);
        }
    }
}
