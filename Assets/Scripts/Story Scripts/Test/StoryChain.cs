using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryChain : MonoBehaviour
{
    public DialogueTrigger story;
    public bool trigger;

    public void Update()
    {
        
        if (trigger)
        {
            trigger = false;
            story.conditionMet = true;
        }
    }
}
