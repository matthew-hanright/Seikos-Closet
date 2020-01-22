using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class DemoMenu : MonoBehaviour
{
    public GameObject loadObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beginGame()
    {
        SceneManager.LoadScene("Act1Scene1");
    }

    public void loadSave()
    {
        if (File.Exists("save"))
        {
            GameObject loader = Instantiate(loadObject as GameObject);
            //SceneManager.LoadScene("BigDungeon");
            //loader.GetComponent<Load>();
        }
    }

    public void close()
    {
        Application.Quit();
    }

    public void returnToMain()
    {
        SceneManager.LoadScene("DemoMenu");
    }

    public void controlsPage()
    {
        SceneManager.LoadScene("ControlsPage");
    }
}
