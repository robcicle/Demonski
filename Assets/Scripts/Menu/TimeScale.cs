using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private MainMenu mainMenu;

    public void TimeStart()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        MainMenu.objActiveYes = true;
    }

    public void TimeStop()
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        MainMenu.objActiveYes = false;
    }
}
