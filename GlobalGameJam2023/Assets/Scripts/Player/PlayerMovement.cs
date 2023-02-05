using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 horizontalInput;

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11;
    [SerializeField][Range(0.0f,0.5f)] float moveSmoothTime = 0.3f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    [SerializeField] float gravity = 30.0f;
    Vector3 verticalVelocity = Vector3.zero;
   // [SerializeField]
   public LayerMask groundMask;
    bool isGrounded;

    [SerializeField] float jumpHeight = 8.0f;
    bool jump;

    private void Update()
    {
        //gets fall speed
        if (!controller.isGrounded)
        {
           
            verticalVelocity.y -= gravity * Time.deltaTime;
            controller.slopeLimit = 90.0f;
        }
        else
        {
            controller.slopeLimit = 45.0f;
        }

        //smooths out player movement so that the player doesnt immedietly stop when not putting in an input
        Vector2 targetDir = new Vector2(horizontalInput.x, horizontalInput.y);
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //gets input for movement
        Vector3 horizontalVelocity = (transform.right * currentDir.x + transform.forward * currentDir.y) * speed;
        //makes the player move
        controller.Move(horizontalVelocity * Time.deltaTime);
        
        if (jump && controller.isGrounded)
        {
               verticalVelocity.y = jumpHeight;

            jump = false;
        }
        
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _hInput)
    {
        //set the horizontal input to the input taken from the function
        horizontalInput = _hInput;
       // print(horizontalInput);
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}
