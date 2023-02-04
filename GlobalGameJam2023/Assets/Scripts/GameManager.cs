using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int questsCompleated = 0;
   // [HideInInspector]
    public GameObject player;
    //[HideInInspector]
    public GameObject gun;
    public GameObject gameOverUI;
    //[HideInInspector]
    public Text healthText;
   // [HideInInspector]
    public Text ammoText;

   // public Text healthUpdate;

   // public Text ammoUpdate;

    Color col;
    public float pickUpTimer1 = 3;
    public float pickUpTimer2 = 3;
    bool gameStarted;

   

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //game manager is not destroyed when changing scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //destroys copies so that only one is in a scene at a time
            Destroy(gameObject);
        }
         
        if(SceneManager.GetActiveScene().name == "Main")
        {
            gameStarted = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {

            MainGameSetup();
           
        }

        if (gameOverUI)
        {
            if (gameOverUI.activeInHierarchy)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if (!player)
        {
            gameStarted = true;
        }



    }

    public void quitGame()
    {
        Application.Quit();
    }
    
    public void GameEnd()
    {
        SceneManager.LoadScene("Ending");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        gameStarted = true;
    }

    public void Level2()
    {
        SceneManager.LoadScene(2);
    }

    public void MainGameSetup()
    {
        
        if (SceneManager.GetActiveScene().name != "Menu")
        {
           // Debug.Log("working!");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player = GameObject.FindGameObjectWithTag("Player");
            gun = GameObject.FindGameObjectWithTag("Gun");
            gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
            gameOverUI.SetActive(false);
            healthText = player.GetComponent<Inventory>().UITexts[0];
            ammoText = player.GetComponent<Inventory>().UITexts[1];
           
            healthText.text = "Health: 200";

            ammoText.text = "Ammo: 50";
            gameStarted = false;

        }
    }


    public void UpdateHealth(float cHealth)
    {
        healthText.text = "Health: " + cHealth;
      //  healthUpdate.enabled = true;
//healthUpdate.text = "+ " + cHealth;
        
    }

    public void UpdateAmmo(float cAmmo)
    {
        ammoText.text = "Ammo: " + cAmmo;
       // ammoUpdate.enabled = true;
       // ammoUpdate.text = "+ " + 5;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        player.GetComponent<InputManager>().OnDisable();
        //player.GetComponent<PlayerMovement>().enabled = false;
        gun.SetActive(false);
        player.GetComponent<CapsuleCollider>().enabled = false;
        
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameStarted = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}


