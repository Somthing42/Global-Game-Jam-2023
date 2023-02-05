using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int questsCompleated = 0;
    //[HideInInspector]
    public GameObject player;
    //[HideInInspector]
    public GameObject gun;
   // [HideInInspector]
    public GameObject gameOverUI;
   // [HideInInspector]
    public Text healthText;
   // [HideInInspector]
    public Text ammoText;

    public float  currenthealth;

    public float currentammo;

    Color col;
    public float pickUpTimer1 = 3;
    public float pickUpTimer2 = 3;
  public  bool gameStarted;

    public Button selectButt;

    public Scene currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

    }

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
         
        if(SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "Ending")
        {
            //Debug.Log("Starting!");
            gameStarted = true;

        }
        currentScene = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (gameStarted)
        {

            MainGameSetup();
            gameStarted = false;
           
        }

        if (gameOverUI)
        {
            if (gameOverUI.activeInHierarchy)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {  
            
           gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
           
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
        gameStarted = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        gameStarted = true;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameStarted = true;
    }

    public void MainGameSetup()
    {

        if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "Ending")
        {

            Debug.Log(SceneManager.GetActiveScene().name);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<InputManager>().OnEnable();
            gun = GameObject.FindGameObjectWithTag("Gun");
            gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
            if(gameOverUI.activeInHierarchy == true)
            {
                gameOverUI.SetActive(false);
            }
               
           
            healthText = player.GetComponent<Inventory>().UITexts[0];
            ammoText = player.GetComponent<Inventory>().UITexts[1];
            selectButt.Select();

            if (SceneManager.GetActiveScene().name == "Main")
            {

                currenthealth = 200;
                currentammo = 50;

            }



            healthText.text = "Health: " + currenthealth;
            ammoText.text = "Ammo: " + currentammo;
           

        }
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            if (!gameOverUI)
            {
                gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
                gameOverUI.SetActive(false);
            }
           

        }
        gameStarted = false;

    }
    public void UpdateHealth(float cHealth)
    {
        healthText.text = "Health: " + cHealth;
        currenthealth = cHealth;
      //  healthUpdate.enabled = true;
//healthUpdate.text = "+ " + cHealth;
        
    }

    public void UpdateAmmo(float cAmmo)
    {
        ammoText.text = "Ammo: " + cAmmo;
        currentammo = cAmmo;
       // ammoUpdate.enabled = true;
       // ammoUpdate.text = "+ " + 5;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        player.GetComponent<InputManager>().OnDisable();
        //player.GetComponent<PlayerMovement>().enabled = false;
        gun.SetActive(false);
        player.GetComponent<BoxCollider>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        


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

    public void ButtSelect()
    {
        selectButt.Select();
    }
}


