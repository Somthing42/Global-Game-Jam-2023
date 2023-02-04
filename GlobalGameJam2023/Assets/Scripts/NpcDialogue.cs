using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    //the text objects that this npc has
    public GameObject[] npcLines;
    //lines when the player helps them
    public GameObject[] specialLines;

    public GameObject textBackground;
    // the current line that is active
    private int currentLine;
    //the previous line that was said.
    private int lastLine = -1;
    //the help that the player needs to do
    public string itemNeeded;

    public GameObject itemNeededObj;

    private bool npcHelped = false;

    private GameObject player;

    private bool doOnce = false;

    public AudioClip gregTalk;

    public AudioClip girlTalk;

    //edit this to make items optional
    public void playDialogue()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMouseLook>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        textBackground.SetActive(true);
        npcHelp();

        int dialogueLength;
        

       


        if (npcHelped)
        {
            dialogueLength = specialLines.Length;

            if (dialogueLength > currentLine)
            {
                //activates current line
                specialLines[currentLine].SetActive(true);
                //if current line is not the first set the prev
                if (currentLine > 0)
                {
                    specialLines[lastLine].SetActive(false);
                }

                currentLine += 1;
                lastLine += 1;
                if (gameObject.name == "Girl")
                {
                    SoundManager.Instance.Play(girlTalk);
                }
                else
                {
                    SoundManager.Instance.Play(gregTalk);
                }
            }
            else
            {
                specialLines[lastLine].SetActive(false);
                currentLine = 0;
                lastLine = -1;
               // player.GetComponent<PlayerController>().enabled = true;
                textBackground.SetActive(false);
            }
        }
        else
        {
            dialogueLength = npcLines.Length;

            if (dialogueLength > currentLine)
            {
                //activates current line
                npcLines[currentLine].SetActive(true);
                //if current line is not the first set the prev
                if (currentLine > 0)
                {
                    npcLines[lastLine].SetActive(false);
                }

                currentLine += 1;
                lastLine += 1;
                if (gameObject.name == "Girl")
                {
                    SoundManager.Instance.Play(girlTalk);
                }
                else
                {
                    SoundManager.Instance.Play(gregTalk);
                }
            }
            else
            {
                npcLines[lastLine].SetActive(false);
                currentLine = 0;
                lastLine = -1;
                player.GetComponent<PlayerMouseLook>().enabled = true;
                player.GetComponent<PlayerMovement>().enabled = true;
                textBackground.SetActive(false);
                if (!itemNeededObj.activeInHierarchy)
                {
                    itemNeededObj.SetActive(true);
                }
            }
        }
        //check which line the npc is on
        

    }

    public void npcHelp()
    {
      
            player = GameObject.FindGameObjectWithTag("Player");

            if(player.GetComponent<Inventory>().items.Contains(itemNeeded))
            {
               // Debug.Log("has orb");
                npcHelped = true;
               //add thing later that this npc has been helped in the game manager
               if (!doOnce)
               {
               // GameManager.instance.questsCompleated += 1;
                doOnce = true;
               }   
                

            }
    
    }
}
