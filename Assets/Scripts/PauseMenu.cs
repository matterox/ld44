using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    [SerializeField]
    private GameObject menuUi;
    [SerializeField]
    private GameObject inGameUi;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (GameManager.dayOver || GameManager.gameOver)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuUi.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (!GameManager.gameOver && !GameManager.dayOver)
            inGameUi.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuUi.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    void Pause()
    {
        inGameUi.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuUi.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void QuitGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        Application.Quit();
    }

    public void RestartLevel()
    {
        Resume();
        GameManager.dayOver = false;
        GameManager.gameOver = false;
        GameManager.isWorkStarted = false;
        GameManager.currentDay = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
