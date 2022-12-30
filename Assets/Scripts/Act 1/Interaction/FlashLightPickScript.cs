
public class FlashLightPickScript : Interactable
{
    public FlashLight flashlight;

    public override void Interact()
    {
        flashlight.picked = true;
        gameObject.SetActive(false);
    }
}
