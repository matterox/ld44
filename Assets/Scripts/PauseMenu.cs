using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    [SerializeField]
    private GameObject menuUi;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuUi.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuUi.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        Resume();
        GameManager.dayOver = false;
        GameManager.gameOver = false;
        GameManager.isWorkStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
