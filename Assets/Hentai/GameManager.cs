using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public bool isPaused { get; private set; }
    public bool isGameActive { get; private set; }

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    
    private void Awake() {
        instance = this;
    }

    private void Start()
    {
        isGameActive = false;
        UpdateTimescale();
    }

    public void StartGame()
    {
        Debug.Log("Game start!");
        // Linked to start button on startMenu
        isGameActive = true;
        mainMenu.SetActive(false);
        UpdateTimescale();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        UpdateTimescale();
    }

    public void UnpauseGame()
    {
        // Linked to resume button on pauseMenu
        isPaused = false;
        pauseMenu.SetActive(false);
        UpdateTimescale();
    }

    public void ExitGame()
    {
        // Linked to exit button on pauseMenu
        Application.Quit();
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverMenu.SetActive(true);
        UpdateTimescale();
    }

    private void UpdateTimescale()
    {
        if (isGameActive && !isPaused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
