using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    PlayerControls controls;
    InputAction menu;


    [SerializeField] private GameObject PauseUI;
    [SerializeField] private bool isPaused;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        menu = controls.PauseMenu.Pause;
        menu.Enable();

        menu.performed += Pause;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    public void Pause(InputAction.CallbackContext context) 
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }

    }


    public void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        PauseUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = false;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PauseUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = true;
    }
}
