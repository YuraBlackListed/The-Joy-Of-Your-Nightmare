using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private KeyCode escapeKey = KeyCode.Escape;
    [SerializeField] private GameObject menu;
    void FixedUpdate()
    {
        if (Input.GetKeyDown(escapeKey))
        {
            if (menu.activeSelf == true)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }
}
