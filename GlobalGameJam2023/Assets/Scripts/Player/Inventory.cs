using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public Text[] UITexts;

    public List<string> items;

    private void Start()
    {
        UITexts[0] = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        UITexts[1] = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
    }


}


