using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    private PlayerController player;
    Save openSave;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StreamReader reader = new StreamReader("save");
        openSave = new Save();
        openSave.currentRoom = reader.ReadLine();
        while(!reader.EndOfStream)
        {
            string currentLine = reader.ReadLine();
            string valueName = currentLine.Split(',')[0];
            valueName = valueName.Substring(1, valueName.Length - 1);
            string valueBool = currentLine.Split(',')[1];
            valueBool = valueBool.Substring(0, valueBool.Length - 1);
            bool value = valueBool == "true";
            openSave.saveValues.Add(new KeyValuePair<string, bool>(valueName, value));
        }
        SceneManager.LoadScene(openSave.currentRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSaveFile(string saveName)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<PlayerController>();
        player.progressionValues = openSave.saveValues;
        Destroy(this.gameObject);
    }
}
