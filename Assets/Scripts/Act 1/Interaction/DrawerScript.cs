using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    [SerializeField] private List<AudioSource> sounds;
    public bool active;
    private bool played = true;
    [SerializeField] private Animator animator;
    private void Update()
    {
        if (active) 
        {
            Close();
        }
        else 
        {
            Open();
        }
    }

    public void Open()
    {
        if(!played)
        {
            int random = Random.Range(1, 5);
            played = true;
            sounds[random].Play();
            animator.SetInteger("OpenIndex", random);
        }
    }

    public void Close()
    {
        if(played)
        {
            played = false;
            int random = Random.Range(1, 5);
            sounds[random].Play();
            animator.SetInteger("OpenIndex", 0);
        }
    }
}
