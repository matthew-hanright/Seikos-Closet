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
        SceneManager.LoadScene("BigDungeon");
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
}
