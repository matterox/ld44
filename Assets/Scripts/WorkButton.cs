using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType { Button, Mail, Phone }
public class WorkButton : MonoBehaviour
{
    
    [SerializeField]
    private ButtonType buttonType;
    [SerializeField]
    private float waitTime = 0.2f;
    [SerializeField]
    private bool timedActivation;
    [SerializeField]
    private GameObject activateObject;

    private bool isPressed;
    private Animator animator;
    private PlayerController player;
    private float activeCounter;
    private float activeIn;
    private bool canActivate;

    private void Start()
    {
        if (timedActivation)
        {
            ResetActiveCounter();
            activateObject.SetActive(false);
        }
        animator = GetComponent<Animator>();
        transform.tag = "Button";
    }
    public ButtonType GetButtonType()
    {
        return buttonType;
    }

    public bool CanPress()
    {
        if (!GameManager.isWorkStarted || GameManager.gameOver)
            return false;
        if (buttonType != ButtonType.Button)
        {
            return !isPressed && canActivate;
        } else
        {
            return !isPressed;
        }
    }
    private void Update()
    {
        if (GameManager.gameOver)
            return;
        if (player == null && !GameManager.gameOver)
        {
            GameObject pl = GameObject.FindGameObjectWithTag("Player");
            if (pl != null)
                player = pl.GetComponent<PlayerController>();
        }
        if (GameManager.isWorkStarted)
            activeCounter += Time.deltaTime;
        if (buttonType != ButtonType.Button)
        {

        }

        if (timedActivation)
        {
            if (activeCounter > activeIn)
            {
                canActivate = true;
                activateObject.SetActive(true);
                ResetActiveCounter();
            }
        }
    }

    private void ResetActiveCounter()
    {
        activeIn = Random.Range(5, 20);
        activeCounter = 0;
    }

    public void EneablePress()
    {
        isPressed = false;

        if (timedActivation)
        {
            activateObject.SetActive(false);
            canActivate = false;
        }

        switch (buttonType)
        {
            case ButtonType.Button:
                player.ReplenishHealth(5);
                break;
            case ButtonType.Mail:
                player.ReplenishHealth(20);
                break;
            case ButtonType.Phone:
                player.ReplenishHealth(12);
                break;
        }

    }

    public void ActivateButton()
    {
        if (!isPressed && GameManager.isWorkStarted)
        {
            if (timedActivation && !canActivate)
                return;
            isPressed = true;
            animator.SetTrigger("Pressed");
            canActivate = false;
            Debug.Log("Pressed: " + buttonType);
        }
    }
}
