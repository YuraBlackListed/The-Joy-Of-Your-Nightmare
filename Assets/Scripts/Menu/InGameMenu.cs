using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private KeyCode escapeKey = KeyCode.Y;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private CameraLook lookScript;
    void Update()
    {
        if (Input.GetKeyDown(escapeKey))
        {
            if (menu.activeSelf == true)
            {
                crosshair.SetActive(true);
                menu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                lookScript.enabled = true;
            }
            else
            {
                crosshair.SetActive(false);
                menu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                lookScript.enabled = true;
            }
        }
    }
}
