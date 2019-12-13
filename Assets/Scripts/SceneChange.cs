using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string nextScene = null;
    public bool change = false;
    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
