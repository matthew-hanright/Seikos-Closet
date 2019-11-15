using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicDialogue : MonoBehaviour
{
    public PlayerController player;
    public Text DialogeBox;
    public string[] text;
    private int i;
    private bool haveControl = false;
    private bool giveControl = false;
    private bool goNext = false;

    // Start is called before the first frame update
    void Start()
    {
        //DialogeBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (haveControl) //If this object has control
        {
            if (Input.GetAxis("Interact") < 0.2)
            {
                //If the interact button isn't being hit, the next hit will progress dialogue
                goNext = true;
            }
            if(goNext && Input.GetAxis("Interact") > 0.8)
            {
                //Progress dialogue
                goNext = false;
                i++;
                UpdateText();
            }
        }
        else if(giveControl)
        {
            //To return control to the player, wait for the interact button to be released,
            //to prevent immediately starting dialogue again
            if (Input.GetAxis("Interact") < 0.2)
            {
                player.canControl = true;
            }
        }
    }

   
    
    
    void UpdateText()
    {
        //If possible, display the requested dialogue
        if (i <= text.Length - 1)
        {
            DialogeBox.gameObject.SetActive(true);
            DialogeBox.text = this.text[i];
        }
        else
        {
            //Else, the dialogue is done, return control to the player
            DialogeBox.gameObject.SetActive(false);
            giveControl = true;
            haveControl = false;
        }
    }
    

    //This function is called first, and begins the dialogue
    public void ShowText(string[] text, int startIndex)
    {
        player.canControl = false;
        haveControl = true;
        this.text = text;
        i = startIndex;
        //If possible, display the first piece of dialogue
        if(startIndex <= text.Length - 1)
        {
            DialogeBox.gameObject.SetActive(true);
            DialogeBox.text = this.text[i];
            //Debug.Log(this.text[i]);
        }
        else
        {
            //Else, there is not dialogue present/at that index, return control
            DialogeBox.gameObject.SetActive(false);
            giveControl = true;
            haveControl = false;
        }
    }
    
}
