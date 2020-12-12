using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementTime = 0.1f;
    [SerializeField]
    private float movementSpeed = 5.0f;

    private CharacterController characterController = null;
    private Vector3 movementDirectionVector = Vector3.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(MovementDirection movementDirection)
    {
        if (!MusicManager.Instance.CanDoAction)
        {
            Debug.Log("Meh");
            return;
        }

        Debug.Log("Good");

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
    }

    private void Update()
    {
        characterController?.Move(movementDirectionVector * Time.deltaTime * movementSpeed);
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


