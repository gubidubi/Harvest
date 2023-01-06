using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    // TODO: colocar essa parte no Game Manager
    static public bool isPaused { get; private set;}

    public GameObject mainMenu;
    public GameObject pauseMenu;

    public void OnStartClicked()
    {
        mainMenu.SetActive(false);
        // TODO: criar essa função no Game Manager
        //StartGame();
    }

    // TODO: colocar essa parte no Game Manager
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pause();
            isPaused = true;
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void OnResumeClicked()
    {
        //Unpause();
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}