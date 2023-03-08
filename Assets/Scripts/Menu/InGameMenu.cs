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
                    crosshair.SetActive(true);
                    menu.SetActive(false);
                    lookObj.SetActive(true);

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
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
            crosshair.SetActive(false);
            menu.SetActive(true);
            settingObj.Click();
        }
    }
}
