using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    [SerializeField]
    private Transform minuteHandle;
    [SerializeField]
    private Transform hourHandle;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        float currentTime = gameManager.GetCurrentTime() * 5;

        float min = currentTime;
        float hours = currentTime / 60;

        float minAngle = - 360 * (min / 60);
        float hourAngle = - 360 * (hours / 12);

        minuteHandle.localRotation = Quaternion.Euler(minAngle, -90, -90);
        hourHandle.localRotation = Quaternion.Euler(hourAngle, -90, -90);

    }
}
