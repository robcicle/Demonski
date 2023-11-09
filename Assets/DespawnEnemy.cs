using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnEnemy : MonoBehaviour
{
    private Enemy enemy;
    public GameObject objectParent;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    private void OnBecameInvisible()
    {
        if (enemy.dead == true)
        {
            Destroy(objectParent);
        }
    }
}
