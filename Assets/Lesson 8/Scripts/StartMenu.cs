using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject StartMenuWindow;
    public GameObject LevelMenuWindow;
    public GameObject RecordsMenuWindow;
    
    void Start()
    {
        StartMenuWindow.SetActive(true);
        LevelMenuWindow.SetActive(false);
        RecordsMenuWindow.SetActive(false);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
