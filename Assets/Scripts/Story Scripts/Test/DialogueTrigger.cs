using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool currentStory = false;
    public bool conditionMet = false;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        //Debug.Log("Checkpoint 2");

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}


