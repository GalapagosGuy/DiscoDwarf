using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : IMusicListener
{
    public Animator playerAnimator;
    public ParticleSystem movementParticles;

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
        if (!MusicManager.Instance.CanDoAction)
            ComboCounter.Instance?.BreakCombo();

        if (!MusicManager.Instance.CanDoAction || !canChangeDirection)
            return;

        ComboCounter.Instance?.InputPressed();

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
                playerAnimator.gameObject.transform.localRotation = Quaternion.Euler(45.0f, 0, playerAnimator.gameObject.transform.localRotation.eulerAngles.z);
                break;
            case MovementDirection.Right:
                movementDirectionVector = Vector3.right;
                playerAnimator.gameObject.transform.localRotation = Quaternion.Euler(-45.0f, 180, playerAnimator.gameObject.transform.localRotation.eulerAngles.z);
                break;
        }

        canChangeDirection = false;

        currentMovementSpeed = movementSpeed;

        movementParticles?.Play();

        //playerAnimator?.SetTrigger("rideTrigger");
    }

    private void Update()
    {
        characterController?.Move(movementDirectionVector * Time.deltaTime * currentMovementSpeed);

        currentMovementSpeed -= Time.deltaTime * speedDrop;

        if (currentMovementSpeed < minimumSpeed)
            currentMovementSpeed = minimumSpeed;
    }

    public override void OnBeatStart()
    {
        canChangeDirection = true;
    }

    public override void OnBeatCenter()
    {

    }

    public override void OnBeatFinished()
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


