using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    [SerializeField]private bool active = true;
    
    private bool played = false;
    
    public List<GameObject> lights;

    [SerializeField] private BoxCollider collider;

    [SerializeField] private TimeCounter timecouter;

    public override void Interact()
    {
        active = !active;

        if (!active)
        {
            Close();
        }
    }

    private void Close()
    {
        if(!played)
        {
            collider.enabled = false;
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].SetActive(false);
            }
            played = true;
            int random = Random.Range(0, 3);

            AudioClip clip = AudioSystem.GetSound("Voiceover", AudioType.Enviroment);

            AudioSystem.StopSound("Voiceover", AudioType.Enviroment, clip);
            timecouter.StartCountdown();
        }
    }
}