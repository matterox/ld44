using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSens = 100;
    [SerializeField]
    private Transform playerTransform;

    private float xLock;
    private float yLock;

    private void Start()
    {
        xLock = 0;
        LockCoursour();
    }

    private void Update()
    {
        RotateCamera();
    }

    private void LockCoursour()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RotateCamera()
    {
        float mouseXRotation = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseYRotation = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        xLock += mouseYRotation;

        if (GameManager.dayOver)
            return;

        if (!GameManager.isWorkStarted)
        {
            if (xLock > 90)
            {
                xLock = 90;
                mouseYRotation = 0;
                ClampX(270);
            }

            else if (xLock < -90)
            {
                xLock = -90;
                mouseYRotation = 0;
                ClampX(90);
            }
        }
        else
        {
            if (xLock > 30)
            {
                xLock = 30;
                mouseYRotation = 0;
                ClampX(330);
            }

            else if (xLock < -45)
            {
                xLock = -45;
                mouseYRotation = 0;
                ClampX(45);
            }
        }

        transform.Rotate(Vector3.left * mouseYRotation);
        playerTransform.Rotate(Vector3.up * mouseXRotation);

        if (GameManager.isWorkStarted)
        {
            Vector3 eulerRot = playerTransform.eulerAngles;
            if (eulerRot.y > 220) eulerRot.y = 220;
            if (eulerRot.y < 140) eulerRot.y = 140;
            playerTransform.eulerAngles = eulerRot;
        }
    }

    private void ClampX(float value)
    {
        Vector3 eulerRot = transform.eulerAngles;
        eulerRot.x = value;
        transform.eulerAngles = eulerRot;
    }
}
