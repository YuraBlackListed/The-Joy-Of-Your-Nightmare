
public class FlashLightPickScript : Interactable
{
    public bool active;
    public FlashLight flashlight;

    public override void Interact()
    {
        if (!active)
        {
            flashlight.picked = true;
            gameObject.SetActive(false);
        }
    }
}
