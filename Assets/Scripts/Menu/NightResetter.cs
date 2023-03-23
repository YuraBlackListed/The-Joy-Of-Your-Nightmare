using UnityEngine;

public class NightResetter : MonoBehaviour
{
    public void ResetNight()
    {
        PlayerPrefs.SetInt("NightNumber", 1);
    }
}
