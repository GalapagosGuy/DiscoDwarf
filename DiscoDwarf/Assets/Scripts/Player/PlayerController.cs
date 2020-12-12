using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        else if(Input.GetKeyDown(KeyCode.J))
        {
            if (interactableObject)
                interactableObject.Use(itemSlot);
        }

    }
    public void SetInteractableObject(InteractableObject obj)
    {
        interactableObject = obj;
    }
}
