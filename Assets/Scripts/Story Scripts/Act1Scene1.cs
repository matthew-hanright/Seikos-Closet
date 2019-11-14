using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Act1Scene1 : MonoBehaviour
{

    public GameObject[] characters;

    private void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
        characters[0].gameObject.SetActive(true);
    }
}
