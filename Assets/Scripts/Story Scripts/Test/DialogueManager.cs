using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public Text dialougeText;
    public Animator animator;
    public PlayerController player;
    public Text[] Choices;

    private Queue<string> sentences;
    private bool haveControl = false;
    private bool giveControl = false;
    private bool goNext = false;
    private bool choices;
    private DialogueTrigger nextLink;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (haveControl) //If this object has control
        {
            if (Input.GetButtonDown("Interact"))
            {
                DisplayNextSentence();
            }
        }
        
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger nextLink)
    {
        this.nextLink = nextLink;
        sentences = new Queue<string>();
        player.canControl = false;
        haveControl = true;
        dialougeText.font = dialogue.font;
        animator.SetBool("IsOpen", true);

        //nameText.text = dialogue.name;
        //nameText.font = dialogue.font;
        
        sentences.Clear();
        
        foreach(string sentence in dialogue.sentences)
        {
            
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        
        if (sentences.Count <= 0)
        {
            EndDialogue();
        }
        if(sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialougeText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialougeText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        if (nextLink != null)
        {
            nextLink.conditionMet = true;
        }
        else
        {
            animator.SetBool("IsOpen", false);
            haveControl = false;
            player.canControl = true;
        }
    }

}
