using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool currentStory = false;
    public bool conditionMet = false;
    public DialogueTrigger nextLink = null;
    public AudioSource previousSound = null;
    public AudioSource sound = null;
    public Animator anim = null;
    public string[] triggerCommand = null;
    public Dialogue dialogue;

    public void Start()
    {
        /*if(anim != null)
        {
            anim.gameObject.GetComponent<Animator>().enabled = false;
        }*/
        
    }

    public void TriggerDialogue()
    {
        Debug.Log("Checkpoint 2");
        conditionMet = false;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, nextLink);
        if(anim != null)
        {
            //anim.gameObject.GetComponent<Animator>().enabled = true;
            StartCutsceneMovement();
        }
        if(sound != null)
        {
            if(previousSound != null)
            {
                previousSound.Stop();
            }
            sound.Play();
        }
    }

    public void StartCutsceneMovement()
    {
        
        foreach(string command in triggerCommand)
        {
            
            if(!anim.GetBool(command))
            {Debug.Log(anim.GetBool(command));
                anim.SetBool(command, true);
            }
            else if (anim.GetBool(command))
            {
                anim.SetBool(command, false);
            }

        }
        //anim.gameObject.GetComponent<Animator>().enabled = false;
    }


}


