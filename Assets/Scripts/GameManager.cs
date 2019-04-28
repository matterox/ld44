using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform sunTransform;
    [SerializeField]
    private float sunAngleStart;
    [SerializeField]
    private float sunAngleStop;
    [SerializeField]
    private float timeSpeed = 10;
    [SerializeField]
    private float dayLength = 180;
    [SerializeField]
    private TextMeshProUGUI toolTipText;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform workPosition;
    [SerializeField]
    private GameObject inGameUi;
    [SerializeField]
    private GameObject dayOverUi;
    [SerializeField]
    private GameObject gameOverUi;
    [SerializeField]
    private TextMeshProUGUI daysSurvived;

    private Vector3 sunEuler;
    private float currentTime;
    public static bool isWorkStarted = false;
    private string currentDayStartText;
    public static bool gameOver = false;
    public static bool dayOver = false;
    public static int currentDay = 1;

    private string[] lines = new string[] {
        "Here we go again.",
        "Hope it's not last.",
        "Again.",
        "What i'm doing here?",
        "What will happen today? Right, nothing..."
    };
    private string[] workLines = new string[] {
        "My hands are ready.",
        "Should work now.",
        "Another day at work...",
        "What i'm doing here?"
    };

    private void Start()
    {
        dayOver = false;
        gameOver = false;
        isWorkStarted = false;
        PauseMenu.isPaused = false;

        sunEuler = sunTransform.eulerAngles;
        sunEuler.x = sunAngleStart;
        if (currentDay == 1)
        {
            currentDayStartText = "First day at work. Better go to my table and start working...";
        } else
        {
            currentDayStartText = "Day " + currentDay + ". " + lines[Random.Range(0, lines.Length)];
        }
    }

    private void Update()
    {
        sunTransform.eulerAngles = sunEuler;
        if (!dayOver && !gameOver)
            currentTime += timeSpeed * Time.deltaTime;

        if (currentTime >= dayLength && !dayOver)
        {
            DayIsOver();
        } else
        {
            UpdateSunPosition();
        }
    }

    private void DayIsOver()
    {
        Debug.Log("Day is over!");
        dayOver = true;
        gameOver = true;
        inGameUi.SetActive(false);
        dayOverUi.SetActive(true);
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public void StartWork()
    {
        if (!isWorkStarted)
        {
            if (currentDay == 1)
            {
                SetTooltipText("My job is simple: Press button to work. Ocasionally i will get phone calls and e-mails. I need to answer them to get bonus LIFE. That's all...");
            }
            else
            {
                SetTooltipText(workLines[Random.Range(0, workLines.Length)]);
            }
            isWorkStarted = true;
            player.position = workPosition.position;
            player.rotation = workPosition.rotation;
            player.GetComponent<CharacterController>().enabled = false;
        }
    }

    public void StartNewDay()
    {
        currentDay++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateSunPosition()
    {
        sunEuler.x = Mathf.Lerp(sunAngleStart, sunAngleStop, currentTime / dayLength);
    }

    public void PlayerHealthDepleted()
    {
        if (currentDay == 1)
        {
            daysSurvived.text = "You survived " + currentDay + " day.";
        } else
        {
            daysSurvived.text = "You survived " + currentDay + " days.";
        }
        gameOverUi.SetActive(true);
        inGameUi.SetActive(false);
        gameOver = true;
    }

    public void SetTooltipText(string text)
    {
        toolTipText.text = text;
    }

    public void SetTooltipCurrentDayText()
    {
        SetTooltipText(currentDayStartText);
    }
}
