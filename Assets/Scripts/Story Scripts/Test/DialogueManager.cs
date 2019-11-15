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

    private Queue<string> sentences;
    private bool haveControl = false;
    private bool giveControl = false;
    private bool goNext = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (haveControl) //If this object has control
        {
            if (Input.GetAxis("Interact") < 0.2)
            {
                //If the interact button isn't being hit, the next hit will progress dialogue
                goNext = true;
            }
            if (goNext && Input.GetAxis("Interact") > 0.8)
            {
                //Progress dialogue
                goNext = false;
                DisplayNextSentence();
            }
        }
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Checkpoint 3");
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
        Debug.Log("Checkpoint 4");
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
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
        Debug.Log("Checkpoint 5");
        animator.SetBool("IsOpen", false);
        haveControl = false;
        player.canControl = true;
    }

}
