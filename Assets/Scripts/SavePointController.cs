using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEditor;

public class SavePointController : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 lowPosition;
    private bool movingDown = true;
    private Vector3 bobSpeed = new Vector3(0.0f, 0.02f, 0.0f);
    private Vector3 rotateSpeed = new Vector3(0.0f, 0.0f, 1.0f);
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        lowPosition = new Vector3(origin.x, origin.y - 2f, origin.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed);
        if (movingDown)
        {
            if (transform.position.y > lowPosition.y)
            {
                transform.position -= bobSpeed;
            }
            else
            {
                movingDown = false;
                transform.position += bobSpeed;
            }
        }
        else
        {
            if (transform.position.y < origin.y)
            {
                transform.position += bobSpeed;
            }
            else
            {
                movingDown = true;
                transform.position -= bobSpeed;
            }
        }
    }

    public void Save()
    {
        PlayerController player = (PlayerController)FindObjectOfType(typeof(PlayerController));
        Save newSave = new Save();
        newSave.currentRoom = SceneManager.GetActiveScene().name;
        newSave.Create();
        string saveName = "save";
        StreamWriter saveFile;
        if (File.Exists(saveName))
        {
            saveFile = new StreamWriter(File.Open(saveName, FileMode.Create));
        }
        else
        {
            saveFile = new StreamWriter((FileStream)File.CreateText(saveName).BaseStream);
        }
        saveFile.WriteLine(newSave.currentRoom);
        foreach(KeyValuePair<string, bool> value in newSave.saveValues)
        {
            saveFile.WriteLine(value);
        }
        saveFile.Close();
    }
}
