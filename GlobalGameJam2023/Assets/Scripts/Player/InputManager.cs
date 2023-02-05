using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerMouseLook mouseLook;
    [SerializeField] Gun gun;
    [SerializeField] PlayerRaycast raycast;
    //the player controls that use the input system
    PlayerControls controls;
    //grabs the ground movement actions from the input system
    PlayerControls.GroundMovementActions groundMovement;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    Coroutine fireCoroutine; 

    private void Awake()
    {

        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;

        //gets the horizontal movement in the input system and puts it in the horizontal input
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        //gets the jump input
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();
        //gets camera input
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        groundMovement.Shoot.performed += _ => gun.Shoot();

        groundMovement.Interact.performed += _ => raycast.interactButtonPressed();
        //starts shooting when button is first pressed
        // groundMovement.Shoot.started += _ => StartFiring();
        //stops shooting when button is released
        // groundMovement.Shoot.canceled += _ => StopFiring();
    }

    private void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }

    /*void StartFiring()
    {
        fireCoroutine = StartCoroutine(gun.RapidFire());
    }

    void StopFiring()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
    }*/

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }
}
