using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool currentStory = false;
    public bool conditionMet = false;
    public DialogueTrigger nextLink = null;
    public Animator anim = null;
    public string[] triggerCommand = null;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        Debug.Log("Checkpoint 2");
        conditionMet = false;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, nextLink);
        if(anim != null)
        {
            //StartCutsceneMovement();
        }
    }

    public void StartCutsceneMovement()
    {
        foreach(string command in triggerCommand)
        {
            anim.SetTrigger(command);
        }
    }
}


