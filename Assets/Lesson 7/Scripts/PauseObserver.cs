using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseObserver : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject ExitDialog;
    // Update is called once per frame

    private void Start()
    {
        PauseMenu.SetActive(false);
        ExitDialog.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
