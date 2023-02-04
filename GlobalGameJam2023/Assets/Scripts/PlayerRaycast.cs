using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    // length of the player's raycast
    [SerializeField] private int rayLength = 5;
    //the layer that the mask can interact with
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

   
    private GameObject raycastObj;
  
    //[SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;

    [SerializeField] private Image crosshair = null;

    [SerializeField] Image leftClickIcon;

    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    bool interacted;

    private void Update()
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        //excludes layers and checks layer value
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {

            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    //this needs to get the interactable assigner
                    raycastObj = hit.collider.gameObject;
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;
                //opens the door
                if (interacted)
                {
                    raycastObj.GetComponent<InteractableAssigner>().ItemFunction();
                    interacted = false;
                }
            }
        }

        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }

    }

    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.green;
            //leftClickIcon.enabled = true;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
            //leftClickIcon.enabled = false;
        }
    }

    public void interactButtonPressed()
    {
        Debug.Log("interactable!");
        interacted = true;
    }

}
