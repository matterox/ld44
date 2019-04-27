using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private AnimationCurve jumpFalloff;
    [SerializeField]
    private float jumpMultiplier;

    private bool jumping;
    private CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!GameManager.isWorkStarted)
        {
            MovementController();
            JumpController();
        }
    }

    private void MovementController()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
        float yMovement = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 forwardMovement = transform.forward * yMovement;
        Vector3 sideMovement = transform.right * xMovement;

        controller.SimpleMove(forwardMovement + sideMovement);
    }

    private void JumpController()
    {
        if (Input.GetButtonDown("Jump") && !jumping)
        {
            jumping = true;
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        controller.slopeLimit = 90;
        float airTime = 0;
        do
        {
            float jumpForce = jumpFalloff.Evaluate(airTime);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            airTime += Time.deltaTime;
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);
        controller.slopeLimit = 45;
        jumping = false;
    }
}
