using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : Interactable
{
    [SerializeField] private List<string> soundNames;

    private bool active = true;
    private bool played = true;

    [SerializeField] private Animator animator;

    public override void Interact()
    {
        active = !active;

        if (active)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    private void Open()
    {
        if(!played)
        {
            int random = Random.Range(0, 3);
            played = true;

            AudioClip clip = AudioSystem.GetSound(soundNames[random], AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Drawer", AudioType.Furniture, clip);

            animator.SetInteger("OpenIndex", random);
        }
    }

    private void Close()
    {
        if(played)
        {
            played = false;
            int random = Random.Range(0, 3);

            AudioClip clip = AudioSystem.GetSound(soundNames[random], AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Drawer", AudioType.Furniture, clip);

            animator.SetInteger("OpenIndex", 0);
        }
    }
}
