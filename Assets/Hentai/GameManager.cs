using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // TODO: colocar essa parte no Game Manager
    static public bool isPaused { get; private set;}
    static public bool isGameActive { get; private set;}

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private void Start() {
        Time.timeScale = 0;
        isGameActive = false;
    }

    public void StartGame()
    {
        // Linked to start button on startMenu
        mainMenu.SetActive(false);
        Time.timeScale = 1;
        isGameActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Unpause()
    {
        // Linked to resume button on pauseMenu
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
        isPaused = false;
    }

    public void Exit()
    {
        // Linked to exit button on pauseMenu
        Application.Quit();
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverMenu.SetActive(true);
    }
}
