using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    private InteractableObject interactableObject;

    private CharacterController charController;
    private ItemSlot itemSlot;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        itemSlot = GetComponent<ItemSlot>();
    }

    void Update()
    {
        PlayerMovement();
        PlayerActions();
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis("Vertical");
        float horizInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizInput, 0.0f, vertInput);
        if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
        charController.Move(movement * movementSpeed * Time.deltaTime);
    }

    private void PlayerActions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(interactableObject)
                interactableObject.Use(itemSlot);
        }
    }

    public void SetInteractableObject(InteractableObject obj)
    {
        interactableObject = obj;
    }
}
