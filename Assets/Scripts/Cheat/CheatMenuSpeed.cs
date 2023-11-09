using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenuSpeed : MonoBehaviour
{
    public static CheatMenuSpeed instance;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
                instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    public void TimeScale(float Timescale)
    {
        Time.timeScale = Timescale;
    }
}
