using UnityEngine;

public class FlashLightPickScript : MonoBehaviour
{
    public bool active;
    public FlashLight flashlight;
    void Update()
    {
        if (!active)
        {
            flashlight.picked = true;
            gameObject.SetActive(false);
        }
    }
}
