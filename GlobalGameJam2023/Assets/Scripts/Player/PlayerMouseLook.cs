using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 0.5f;
    [SerializeField] float sensitivityY = 0.5f;

    [HideInInspector] public float mouseX;
    [HideInInspector] public float mouseY;

    [SerializeField] Transform playerCamera;
    float cameraPitch = 0.0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        Vector2 mouseDelta = new Vector2(mouseX, mouseY);

        cameraPitch -= mouseDelta.y * sensitivityY;

        //clamps the vertical height to 90 degrees both ways
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up, mouseX * sensitivityX);

    }

    public void ReceiveInput (Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }
}
