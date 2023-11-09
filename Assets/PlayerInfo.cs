using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public string playerName;
    internal bool nameSet;

    private void Start()
    {
        if (playerName == null)
        {
            playerName = "???";
            nameSet = false;
        }
        else
        {
            nameSet = true;
        }

        if (playerName == "")
        {
            playerName = "???";
            nameSet = false;
        }
        else
        {
            nameSet = true;
        }
    }

    public void ChangeName(string newName)
    {
        playerName = newName;
        nameSet = true;
    }
}
