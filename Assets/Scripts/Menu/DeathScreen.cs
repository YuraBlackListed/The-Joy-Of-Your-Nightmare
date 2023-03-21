using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private KillerInfo info;
    [SerializeField] private GameObject WindowScreen;
    [SerializeField] private GameObject DoorScreen;
    [SerializeField] private GameObject FearDeathScreen;

    [SerializeField] SceneFade fader;

    private void Start() 
    {
        if(info.killerID == 1)
        {
            WindowScreen.SetActive(true);
        }
        else if(info.killerID == 2)
        {
            DoorScreen.SetActive(true);
        }
        else
        {
            FearDeathScreen.SetActive(true);
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fader.ActivateFade(1);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            fader.ActivateFade(0);
        }
    }
}
