using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private KeyCode escapeKey = KeyCode.Escape;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject lookObj;
    [SerializeField] private OptionsScript settingObj;
    private void Awake() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if(!settingObj.active)
        {
            if (Input.GetKeyDown(escapeKey))
            {
                if (menu.activeSelf == true)
                {
                    Time.timeScale = 1f;

                    AudioListener.pause = false;

                    crosshair.SetActive(true);
                    menu.SetActive(false);
                    lookObj.SetActive(true);

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Time.timeScale = 0f;

                    AudioListener.pause = true;

                    crosshair.SetActive(false);
                    menu.SetActive(true);
                    lookObj.SetActive(false);

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
        else if (Input.GetKeyDown(escapeKey))
        {
            Time.timeScale = 0f;

            AudioListener.pause = true;

            crosshair.SetActive(false);
            menu.SetActive(true);
            settingObj.Click();
        }
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
    }
}
