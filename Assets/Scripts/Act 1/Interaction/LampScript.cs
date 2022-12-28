using UnityEngine;

public class LampScript : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private AudioSource onSound;
    
    public bool active;
    private bool played = true;

    private void Update()
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
            onSound.Play();
        }
        lightning.SetActive(false);
    }

    public void TurnOff()
    {
        if(played)
        {
            played = false;
            onSound.Play();
        }
        lightning.SetActive(true);
    }
}
