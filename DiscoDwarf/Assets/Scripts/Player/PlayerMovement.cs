using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IMusicListener
{
    [SerializeField]
    private float movementTime = 0.1f;
    [SerializeField]
    private float movementSpeed = 5.0f;
    [SerializeField]
    private float speedDrop = 1.5f;
    [SerializeField]
    private float minimumSpeed = 2.0f;

    private float currentMovementSpeed = 0.0f;

    private CharacterController characterController = null;
    private Vector3 movementDirectionVector = Vector3.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        currentMovementSpeed = movementSpeed;
    }

    private bool canChangeDirection = true;

    public void Move(MovementDirection movementDirection)
    {
        if (!MusicManager.Instance.CanDoAction || !canChangeDirection)
            return;

        switch (movementDirection)
        {
            case MovementDirection.Forward:
                movementDirectionVector = Vector3.forward;
                break;
            case MovementDirection.Backward:
                movementDirectionVector = Vector3.back;
                break;
            case MovementDirection.Left:
                movementDirectionVector = Vector3.left;
                break;
            case MovementDirection.Right:
                movementDirectionVector = Vector3.right;
                break;
        }

        canChangeDirection = false;

        currentMovementSpeed = movementSpeed;
    }

    private void Update()
    {
        characterController?.Move(movementDirectionVector * Time.deltaTime * currentMovementSpeed);

        currentMovementSpeed -= Time.deltaTime * speedDrop;

        if (currentMovementSpeed < minimumSpeed)
            currentMovementSpeed = minimumSpeed;
    }

    public void OnBeatStart()
    {
        canChangeDirection = true;
    }

    public void OnBeatCenter()
    {

    }

    public void OnBeatFinished()
    {
        canChangeDirection = false;
    }
}

public enum MovementDirection
{
    Forward,
    Backward,
    Left,
    Right
}

/*
 *             Vector3 direction = (target.transform.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            float angle = Quaternion.Angle(transform.rotation, lookRotation);
            float timeToComplete = angle / rotationSpeed;
            float donePercentage = Mathf.Min(1.0f, Time.deltaTime / timeToComplete);

            this.transform.rotation = Quaternion.Euler(0.0f, Quaternion.Slerp(transform.rotation, lookRotation, donePercentage).eulerAngles.y, 0.0f);
            */


