using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerScript : Lockable
{
    [SerializeField] private string soundName;

    [SerializeField]private bool active = true;
    private bool played = false;

    [SerializeField] private Animator animator;

    private void Update()
    {
        if (Inventory.ContainsItem(KeyName, ItemType.Key))
        {
            IsLocked = false;
        }
    }
    public override void Interact()
    {
        if (IsLocked) return;

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
            played = true;

            AudioClip clip = AudioSystem.GetSound(soundName, AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Drawer", AudioType.Furniture, clip);

            animator.SetBool("Open", true);
            print("open");
        }
    }

    private void Close()
    {
        if(played)
        {
            played = false;

            AudioClip clip = AudioSystem.GetSound(soundName, AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Drawer", AudioType.Furniture, clip);

            animator.SetBool("Open", false);
        }
    }
}
