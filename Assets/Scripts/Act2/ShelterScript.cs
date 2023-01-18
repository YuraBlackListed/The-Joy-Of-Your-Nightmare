using UnityEngine;

public class ShelterScript : Interactable
{
    public bool PlayerIsInside { get; private set; }


    [SerializeField] private GameObject Player;

    [SerializeField] private Transform InPosition;
    [SerializeField] private Transform OutPosition;

    private bool canInteract = true;

    private float shelterCooldown = 1.5f; // Set it like get in/out animation lenght

    public override void Interact()
    {
        if (PlayerIsInside && canInteract)
        {
            GetOut();
        }
        else if (!PlayerIsInside && canInteract)
        {
            GetIn();
        }
    }
    private void GetIn()
    {
        canInteract = false;
        PlayerIsInside = true;

        //Do some thing like: PlayerState.Hidden = true;

        Player.transform.position = InPosition.position;
        Player.transform.rotation = InPosition.rotation;

        Invoke(nameof(ResetInteraction), shelterCooldown);
    }
    private void GetOut()
    {
        canInteract = false;
        PlayerIsInside = false;

        //Do some thing like: PlayerState.Hidden = false;

        Player.transform.position = OutPosition.position;
        Player.transform.rotation = OutPosition.rotation;

        Invoke(nameof(ResetInteraction), shelterCooldown);
    }
    private void ResetInteraction()
    {
        canInteract = true;
    }
}
