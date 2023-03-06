using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] GameObject optionsUI;
    [SerializeField] GameObject restOfUI;
    private bool active = false;
    public void Click()
    {
        active = !active;
        optionsUI.SetActive(active);
        restOfUI.SetActive(!active);
    }
}
