using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MothersWords : MonoBehaviour
{
    public PlayerController player;
    public Animator anim = null;
    public string[] triggerCommand = null;
    public bool transfer = false;
    int i = 0;

    public void Update()
    {
        if (!player.lookingRight)
        {
            NextWord();
        }
        if(transfer)
        {
            SceneManager.LoadScene("Act1Scene2Part2");
        }
    }

    public void NextWord()
    {
        anim.SetBool(triggerCommand[i], true);
        i++;
    }

    IEnumerator PauseWord()
    {
      
        yield return new WaitForSeconds(3);
    }
}
