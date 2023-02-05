using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    PlayerControls controls;
    InputAction menu;


    [SerializeField] private GameObject PauseUI;
    [SerializeField] private bool isPaused;

    public Button pauseStart;
    public Button GoverStart;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
       
    }
    private void Start()
    {
        if (GameManager.instance.currentScene != null)
        {
            if (GameManager.instance.currentScene.name != "Ending")
            {
                GameManager.instance.selectButt = GoverStart;
            }
            else
            {
                GameManager.instance.selectButt = pauseStart;
            }
        }
        
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

        if (isPaused && GameManager.instance.gameOverUI.activeInHierarchy == false)
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
        GameManager.instance.selectButt = pauseStart;
        GameManager.instance.ButtSelect();
        if (GameManager.instance.currentScene.name != "Ending")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = false;
        }
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PauseUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        if(GameManager.instance.currentScene.name != "Ending")
        {
            GameManager.instance.selectButt = GoverStart;
            GameManager.instance.ButtSelect();
            GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().enabled = true;
        }
        
    }
}
