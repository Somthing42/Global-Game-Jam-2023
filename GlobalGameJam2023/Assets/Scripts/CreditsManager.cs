using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public GameObject credits;

    public GameObject theEndText;

    public float creditsTimer;

    public float creditSpeed;

    public float creditsStop;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        creditsTimer -= Time.deltaTime;



        if (creditsTimer <= 0)
        {
            creditsStop -= Time.deltaTime;

           

            if (creditsStop >= 0)
            {
                credits.GetComponent<RectTransform>().Translate((Vector3.up * creditSpeed) * Time.deltaTime);
                theEndText.GetComponent<RectTransform>().Translate((Vector3.up * creditSpeed) * Time.deltaTime);
            }

            
        }

        

    }
}
