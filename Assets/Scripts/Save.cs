using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public string currentRoom;
    public List<KeyValuePair<string, bool>> saveValues = new List<KeyValuePair<string, bool>>();
    public void Create()
    {
        saveValues.Add(new KeyValuePair<string, bool>("firstDay", false));
        saveValues.Add(new KeyValuePair<string, bool>("hasKey", false));
    }
}
