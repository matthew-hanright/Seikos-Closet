using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDriver : MonoBehaviour
{
    public DialogueTrigger[] story;
    // Start is called before the first frame update
    void Start()
    {
        foreach(DialogueTrigger test in story)
        {
            Debug.Log(test);
        }
    }

    void Update()
    {
        for(int i = 0; i < story.Length; i++)
        {
            if (story[i].currentStory && story[i].conditionMet)
            {
                story[i].TriggerDialogue();
                story[i].currentStory = false;
                if(i + 1 < story.Length)
                {
                story[i + 1].currentStory = true;
                }
                
            }
        }
    }


}
