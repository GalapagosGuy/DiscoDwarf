using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            //Debug.Log($"{other.name} is now in range to use");
            playerMove.SetInteractableObject(other.GetComponent<InteractableObject>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            playerMove.SetInteractableObject(null);
        }
    }
}
