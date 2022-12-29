using UnityEngine;

public class LampScript : Interactable
{
    [SerializeField] private GameObject lightning;
    
    public bool active;
    private bool played = true;

    public override void Interact()
    {
        if (active)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
    public void TurnOn()
    {
        if(!played)
        {
            played = true;
            AudioSystem.PlaySetSoundAt("Lamp", AudioType.Furniture);
        }

        lightning.SetActive(false);
    }

    public void TurnOff()
    {
        if(played)
        {
            played = false;
            AudioSystem.PlaySetSoundAt("Lamp", AudioType.Furniture);
        }

        lightning.SetActive(true);
    }
}
