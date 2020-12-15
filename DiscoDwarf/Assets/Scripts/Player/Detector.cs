using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            //Debug.Log($"{other.name} is now in range to use");
            playerController.SetInteractableObject(other.GetComponent<InteractableObject>());
            if (other.GetComponent<DrinkMachine>())
                other.GetComponent<DrinkMachine>().TurnOnInfo(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            playerController.SetInteractableObject(null);
            if (other.GetComponent<DrinkMachine>())
                other.GetComponent<DrinkMachine>().TurnOnInfo(false);
        }
    }
}
