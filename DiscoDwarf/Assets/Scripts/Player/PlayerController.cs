using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IMusicListener
{
    public PlayerMovement playerMovement;
    private InteractableObject interactableObject;
    private ItemSlot itemSlot;

    private void Awake()
    {
        itemSlot = GetComponent<ItemSlot>();
    }

    private void Update()
    {
        ProcessMovement();
    }

    private bool canInteractWithObject = false;

    private void ProcessMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerMovement?.Move(MovementDirection.Forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerMovement?.Move(MovementDirection.Backward);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerMovement?.Move(MovementDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerMovement?.Move(MovementDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (!MusicManager.Instance.CanDoAction)
                ComboCounter.Instance.BreakCombo();

            if (!MusicManager.Instance.CanDoAction || !canInteractWithObject)
                return;

            ComboCounter.Instance?.InputPressed();

            if (interactableObject)
                interactableObject.Use(itemSlot);

            canInteractWithObject = false;
        }

    }
    public void SetInteractableObject(InteractableObject obj)
    {
        interactableObject = obj;
    }

    public override void OnBeatStart()
    {
        canInteractWithObject = true;
    }

    public override void OnBeatCenter()
    {
        //hi
    }

    public override void OnBeatFinished()
    {
        canInteractWithObject = false;
    }
}
