using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDialogue : MonoBehaviour
{
    public PlayerController player;

    private string[] text;
    private int i;
    private bool haveControl = false;
    private bool giveControl = false;

    private bool goNext = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (haveControl)
        {
            if (Input.GetAxis("Interact") < 0.2)
            {
                goNext = true;
            }
            if(goNext && Input.GetAxis("Interact") > 0.8)
            {
                goNext = false;
                i++;
                UpdateText();
            }
        }
        else if(giveControl)
        {
            if (Input.GetAxis("Interact") < 0.2)
            {
                player.canControl = true;
            }
        }
    }

    void UpdateText()
    {
        if (i <= text.Length - 1)
        {
            Debug.Log(text[i]);
        }
        else
        {
            giveControl = true;
            haveControl = false;
        }
    }

    public void ShowText(string[] text, int startIndex)
    {
        player.canControl = false;
        haveControl = true;
        this.text = text;
        i = startIndex;
        if(startIndex <= text.Length - 1)
        {
            Debug.Log(this.text[i]);
        }
        else
        {
            giveControl = true;
            haveControl = false;
        }
    }
}
