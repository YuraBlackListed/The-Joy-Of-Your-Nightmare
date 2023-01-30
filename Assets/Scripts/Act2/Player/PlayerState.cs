using UnityEngine;

public enum PlayerStatus
{ 
    Normal,
    Hidden,
}
public class PlayerState : MonoBehaviour
{
    public static PlayerState instance;
    public PlayerStatus Status { get; private set; }
    public GameObject CurrentShelter { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Status = PlayerStatus.Normal;
    }
    public static void Hide(GameObject shelter)
    {
        instance.CurrentShelter = shelter;

        instance.Status = PlayerStatus.Hidden;
    }
    public static void SetNormal()
    {
        instance.CurrentShelter = null;

        instance.Status = PlayerStatus.Normal;
    }
}
