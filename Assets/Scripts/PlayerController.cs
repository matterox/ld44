using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private Transform workPlace;
    [SerializeField]
    private MouseLook mouseLook;
    [SerializeField]
    private Slider healthUiSlider;
    [SerializeField]
    private Image targetImage;
    [SerializeField]
    private TextMeshProUGUI tooltipMouse;
    [SerializeField]
    private float playerMaxHealth = 100;
    [SerializeField]
    private GameObject gameOverRagdoll;

    private float healthDecreaseSpeed = 5;
    private float playerLive = 100;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (GameManager.gameOver)
            return;
        healthUiSlider.value = playerLive / playerMaxHealth;
        //if (gameManager)
        //    return;
        if (!PauseMenu.isPaused)
        {
            if (GameManager.currentDay != 13)
            {
                playerLive -= Time.deltaTime * healthDecreaseSpeed * (0.54f + GameManager.currentDay * 0.1f);
            }
            else
            {
                playerLive -= Time.deltaTime * healthDecreaseSpeed/2;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.transform.tag == "Button")
                {
                    WorkButton button = hit.transform.GetComponent<WorkButton>();
                    if (!button.CanPress())
                        return;
                    switch (button.GetButtonType())
                    {
                        case ButtonType.Button:
                            tooltipMouse.text = "Press button.";
                            break;
                        case ButtonType.Phone:
                            tooltipMouse.text = "Answer phone.";
                            break;
                        case ButtonType.Mail:
                            tooltipMouse.text = "Write reply.";
                            break;
                    }
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Debug.DrawLine(ray.origin, hit.point, Color.red);
                        button.ActivateButton();
                    }
                    else
                    {
                        targetImage.color = Color.red;
                    }
                }
                else
                {
                    tooltipMouse.text = "";
                    targetImage.color = Color.white;
                }
            }

            if (Vector3.Distance(transform.position, workPlace.position) < 3 && !GameManager.isWorkStarted)
            {
                gameManager.SetTooltipText("Press 'e' to start working.");
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2))
                {
                    gameManager.StartWork();
                }
            }
            else if (Vector3.Distance(transform.position, workPlace.position) >= 3 && !GameManager.isWorkStarted)
            {
                gameManager.SetTooltipCurrentDayText();
            }
        }

        if (playerLive <= 0)
        {
            gameManager.PlayerHealthDepleted();
            Instantiate(gameOverRagdoll, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (playerLive >= playerMaxHealth)
        {
            playerLive = playerMaxHealth;
        }
    }

    public void ReplenishHealth(float amount)
    {
        if (GameManager.gameOver)
            return;
        playerLive += amount;
    }
}
