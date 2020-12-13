using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IMusicListener
{
    public PlayerMovement playerMovement;
    public ParticleSystem idleParticles;
    public Animator swaper;

    private InteractableObject interactableObject;
    private ItemSlot itemSlot;
    private RoboHand roboHand;
    private bool tutorial = true;
    [SerializeField]
    private GameObject startCanvas;

    private void Awake()
    {
        //Time.timeScale = 0;
        itemSlot = GetComponent<ItemSlot>();
        roboHand = GetComponent<RoboHand>();
    }

    private void Update()
    {
        ProcessMovement();
    }

    private bool canDoLocalAction = false;

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

        if (Input.GetKeyDown(KeyCode.J))
        {
            if(tutorial)
            {
                Time.timeScale = 1;
                startCanvas.SetActive(false);
                tutorial = false;
                
            }
            else
            {
                if (!MusicManager.Instance.CanDoAction)
                    ComboCounter.Instance.BreakCombo();

                if (!MusicManager.Instance.CanDoAction || !canDoLocalAction)
                    return;

                ComboCounter.Instance?.InputPressed();

                if (interactableObject)
                    interactableObject.Use(itemSlot);

                canDoLocalAction = false;

                idleParticles?.Play();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (!MusicManager.Instance.CanDoAction)
                ComboCounter.Instance.BreakCombo();

            if (!MusicManager.Instance.CanDoAction || !canDoLocalAction)
                return;

            ComboCounter.Instance?.InputPressed();

            if (roboHand)
                roboHand.SwapHand();

            canDoLocalAction = false;

            idleParticles?.Play();
            swaper?.SetTrigger("swapTrigger");
        }

    }
    public void SetInteractableObject(InteractableObject obj)
    {
        interactableObject = obj;
    }

    public override void OnBeatStart()
    {
        canDoLocalAction = true;
    }

    public override void OnBeatCenter()
    {
        //hi
    }

    public override void OnBeatFinished()
    {
        canDoLocalAction = false;
    }
}
