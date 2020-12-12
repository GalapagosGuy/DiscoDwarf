using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (MusicManager.Instance.CanDoAction)
                Debug.Log("Good!");
            else
                Debug.Log("Meh!");
        }
    }
}
