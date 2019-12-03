using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDriver : MonoBehaviour
{
    public DialogueTrigger[] story;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        foreach(DialogueTrigger test in story)
        {
            
        }
    }

    void Update()
    {
        for(int i = 0;i < story.Length; i++)
        {
            if (story[i].currentStory && story[i].conditionMet)
            {
                Debug.Log("Checkpoint 1");
                story[i].TriggerDialogue();
                story[i].currentStory = false;
                if (i + 1 < story.Length)
                {
                    Debug.Log(story[i + 1]);
                    story[i + 1].currentStory = true;
                }
            }
        }
    }


}
