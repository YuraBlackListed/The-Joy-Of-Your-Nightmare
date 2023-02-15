using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : Lockable
{
    [SerializeField] private List<string> soundNames;

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
            int randomSound = Random.Range(0, 3);
            played = true;

            AudioClip clip = AudioSystem.GetSound(soundNames[randomSound], AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Drawer", AudioType.Furniture, clip);

            int randomCondition = Random.Range(1, 5);
            animator.SetInteger("OpenIndex", randomCondition);
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
