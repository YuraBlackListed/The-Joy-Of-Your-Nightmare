using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : Interactable
{
    [SerializeField] private List<string> soundNames;

    public bool active;
    private bool played = true;

    [SerializeField] private Animator animator;

    public override void Interact()
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
            int random = Random.Range(0, 3);
            played = true;

            AudioClip clip = AudioSystem.GetSound(soundNames[random], AudioType.Enviroment);

            AudioSystem.PlaySoundOnce("Drawer", AudioType.Furniture, clip);

            animator.SetInteger("OpenIndex", random);
        }
    }

    public void Close()
    {
        if(played)
        {
            played = false;
            int random = Random.Range(0, 3);

            AudioClip clip = AudioSystem.GetSound(soundNames[random], AudioType.Enviroment);

            AudioSystem.PlaySoundOnce("Drawer", AudioType.Furniture, clip);

            animator.SetInteger("OpenIndex", 0);
        }
    }
}
